using CharmCore.Api;
using CharmDiamond.Domain;

namespace CharmDiamond
{
	/// <summary>Unbreakable: valuables you hold in your hands take no damage.</summary>
	internal sealed class DiamondCharm : Charm
	{
		internal const string PrefabName = "Valuable Wizard Diamond";

		internal DiamondCharm()
			: base("Diamond", "Diamond - Unbreakable", defaultWearPercent: 8f, defaultCooldownSeconds: null, PrefabName)
		{
			WearCooldownSeconds = AddSetting("WearCooldownSeconds", 0.5f, 0f, 10f, "Minimum time, in seconds, between wear charges for the same held valuable. Hits are always prevented; hits inside this window cost nothing extra, so one scrape is not billed many times. 0 charges every prevented hit.");
		}

		/// <summary>Per-object wear records of the current level; cleared when the level ends.</summary>
		internal WearThrottle Throttle { get; } = new WearThrottle();

		protected override void OnLevelEnded()
		{
			Throttle.Clear();
		}

		/// <summary>Minimum seconds between wear charges for one held valuable (0 to 10).</summary>
		internal CharmSetting WearCooldownSeconds { get; }
	}
}
