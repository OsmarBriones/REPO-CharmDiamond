using HarmonyLib;

namespace CharmDiamond.Patches
{
	/// <summary>
	/// The game removes valuable value only through <c>PhysGrabObjectImpactDetector.Break</c> on the master (which then
	/// sends BreakRPC to everybody). Skipping it on the master prevents the loss on every machine. Host only.
	/// </summary>
	[HarmonyPatch(typeof(PhysGrabObjectImpactDetector), "Break")]
	internal static class PhysGrabObjectImpactDetector_Break_Patch
	{
		[HarmonyPrefix]
		private static bool Prefix(PhysGrabObjectImpactDetector __instance, bool _forceBreak)
		{
			if (!SemiFunc.IsMasterClientOrSingleplayer())
			{
				return true;
			}

			return !Unbreakable.ShouldBlock(__instance, _forceBreak);
		}
	}
}
