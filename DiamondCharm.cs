using CharmCore.Api;

namespace CharmDiamond
{
	/// <summary>Unbreakable: valuables you hold in your hands take no damage.</summary>
	internal sealed class DiamondCharm : Charm
	{
		internal const string PrefabName = "Valuable Wizard Diamond";

		internal DiamondCharm()
			: base("Diamond", "Diamond - Unbreakable", defaultWearPercent: 8f, defaultCooldownSeconds: null, PrefabName)
		{
			// Extra host-synced numbers for this power go here, for example:
			// Amount = AddSetting("Amount", 30f, 1f, 100f, "Description for the config file.");
		}
	}
}
