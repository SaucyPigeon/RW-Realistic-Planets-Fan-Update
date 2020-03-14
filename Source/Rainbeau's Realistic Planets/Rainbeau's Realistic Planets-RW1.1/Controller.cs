using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Verse;

namespace Planets_Code
{
	public class Controller : Mod {
		public static Dictionary<Faction, int> factionCenters = new Dictionary<Faction, int>();
		public static Dictionary<Faction, int> failureCount = new Dictionary<Faction, int>();
		public static double maxFactionSprawl = 0;
		public static double minFactionSeparation = 0;
		public static Settings Settings;
		public static MethodInfo FactionControlSettingsMI = null;

		public override string SettingsCategory() { return "Planets.ModName".Translate(); }
		public override void DoSettingsWindowContents(Rect canvas) { Settings.DoWindowContents(canvas); }

		public Controller(ModContentPack content) : base(content) {
			const bool Debug = false;
			const string Id = "net.rainbeau.rimworld.mod.realisticplanets";

			if (Debug)
			{
				Log.Warning($"{Id} is in debug mode. Contact the mod author if you see this.");
				Harmony.DEBUG = Debug;
			}

			var harmony = new Harmony(Id);
			harmony.PatchAll( Assembly.GetExecutingAssembly() );
			Settings = GetSettings<Settings>();
			LongEventHandler.QueueLongEvent(new Action(Init), "LibraryStartup", false, null);
		}
		
		private void Init()
		{
			// Get FactionControl's settings button for use on the Create World page
			foreach (ModContentPack pack in LoadedModManager.RunningMods)
			{
				if (pack.PackageId == "factioncontrol.kv.rw")
				{
					const string typeName = "FactionControl.Patch_Page_CreateWorldParams_DoWindowContents";
					const string methodName = "OpenSettingsWindow";

					foreach (Assembly assembly in pack.assemblies.loadedAssemblies)
					{
						var dialog = assembly.GetType(typeName);
						if (dialog != null)
						{
							FactionControlSettingsMI = dialog.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
							break;
						}
					}
					if (FactionControlSettingsMI == null)
					{
						Log.Warning($"Realistic Planets - Fan Update was unable to find {typeName}::{methodName} in mod Faction Control. Please ensure that both mods have been updated to their latest versions. Otherwise, report this issue to both mod authors.");
					}
					break;
				}
			}
		}
	}
}
