using CharmCore.Api;
using Pockets.Api;
using UnityEngine;

namespace CharmDiamond
{
	/// <summary>
	/// Unbreakable (host only): a valuable held in the hands of a player who carries the Diamond in a pocket takes no
	/// damage. The Harmony prefix on <c>PhysGrabObjectImpactDetector.Break</c> asks <see cref="ShouldBlock"/>; each
	/// prevented hit wears the Diamond (rate-limited per held valuable by <see cref="WearThrottle"/>).
	/// Not blocked on purpose: forced breaks (game scripts, and CharmCore's own wear, which is forced and always on a
	/// pocketed valuable), pocketed valuables, valuables nobody holds, and valuables that are already dead.
	/// </summary>
	internal static class Unbreakable
	{
		/// <summary>True when this physics hit must be cancelled. Call on the host only.</summary>
		internal static bool ShouldBlock(PhysGrabObjectImpactDetector detector, bool forceBreak)
		{
			DiamondCharm? charm = CharmDiamondPlugin.Charm;
			if (charm == null || forceBreak || detector == null || !detector.isValuable)
			{
				return false;
			}

			PhysGrabObject? grabObject = detector.GetComponent<PhysGrabObject>();
			ValuableObject? valuable = detector.GetComponent<ValuableObject>();
			if (grabObject == null || valuable == null || grabObject.dead || grabObject.playerGrabbing.Count == 0)
			{
				return false;
			}

			bool held = false;
			ValuableObject? diamond = null;
			CharmsApi.ForEachCarrier(charm, (avatar, carried) =>
			{
				if (held || avatar.physGrabber == null || !grabObject.playerGrabbing.Contains(avatar.physGrabber))
				{
					return;
				}

				held = true;
				diamond = carried;
			});

			if (!held || PocketsApi.IsPocketed(valuable))
			{
				return false;
			}

			if (diamond != null && charm.Throttle.TryCharge(valuable.GetInstanceID(), Time.time, charm.WearCooldownSeconds.Value))
			{
				charm.WearValuable(diamond, charm.WearPercent);
				if (charm.DebugMode)
				{
					charm.LogDebug($"Unbreakable: prevented a hit on {valuable.name} (value {valuable.dollarValueCurrent:0}); wore the Diamond {charm.WearPercent:0.#}%");
				}
			}
			else
			{
				if (charm.DebugMode)
				{
					charm.LogDebug($"Unbreakable: prevented a hit on {valuable.name} (no extra wear, inside the wear cooldown)");
				}
			}

			return true;
		}
	}
}
