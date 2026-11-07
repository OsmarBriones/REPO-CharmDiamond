using BepInEx;
using HarmonyLib;
using CharmCore.Api;
using UnityEngine;

namespace CharmDiamond
{
	[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
	[BepInDependency("com.osmar.Pockets", BepInDependency.DependencyFlags.HardDependency)]
	[BepInDependency("com.osmar.CharmCore", BepInDependency.DependencyFlags.HardDependency)]
	public class CharmDiamondPlugin : BaseUnityPlugin
	{
		public const string PluginGuid = "com.osmar.CharmDiamond";
		public const string PluginName = "CharmDiamond";
		public const string PluginVersion = "1.0.0";

		internal static new BepInEx.Logging.ManualLogSource Logger { get; private set; } = null!;

		/// <summary>The registered charm, for the Harmony patch.</summary>
		internal static DiamondCharm? Charm { get; private set; }

		private void Awake()
		{
			Logger = base.Logger;

			// Prevent the plugin from being deleted
			this.gameObject.transform.parent = null;
			this.gameObject.hideFlags = HideFlags.HideAndDontSave;

			// Registering binds Enabled, WearPercent, CooldownSeconds and the Debug section (DebugMode) in this mod's config file.
			Charm = new DiamondCharm();
			CharmsApi.Register(Charm, Config);
			new Harmony(PluginGuid).PatchAll();

			Logger.LogInfo($"{PluginName} {PluginVersion} loaded!");
		}
	}
}
