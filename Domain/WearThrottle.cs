using System.Collections.Generic;

namespace CharmDiamond.Domain
{
	/// <summary>
	/// Game-free rule that keeps one scrape from being billed many times: after a wear charge for a protected object,
	/// further prevented hits on the same object are free until <c>cooldownSeconds</c> have passed.
	/// Time is passed in (seconds) so the rule is unit tested without Unity.
	/// </summary>
	internal sealed class WearThrottle
	{
		private readonly Dictionary<int, float> lastCharge = new Dictionary<int, float>();

		/// <summary>
		/// True when a prevented hit on <paramref name="objectId"/> at <paramref name="now"/> should wear the charm's valuable.
		/// Records the charge when it returns true. A cooldown of 0 or less charges every hit.
		/// </summary>
		internal bool TryCharge(int objectId, float now, float cooldownSeconds)
		{
			if (cooldownSeconds > 0f && lastCharge.TryGetValue(objectId, out float last) && now >= last && now - last < cooldownSeconds)
			{
				return false;
			}

			lastCharge[objectId] = now;
			return true;
		}

		/// <summary>Forgets every record.</summary>
		internal void Clear()
		{
			lastCharge.Clear();
		}
	}
}
