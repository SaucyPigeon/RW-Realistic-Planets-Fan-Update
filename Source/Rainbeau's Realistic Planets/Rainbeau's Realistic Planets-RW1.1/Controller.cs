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
			var data = new ModMethodData(
				packageId: "factioncontrol.kv.rw",
				typeName: "FactionControl.Patch_Page_CreateWorldParams_DoWindowContents",
				methodName: "OpenSettingsWindow");

			if (data.ModIsLoaded())
			{
				FactionControlSettingsMI = data.GetMethod();
			}
		}
	}
}
