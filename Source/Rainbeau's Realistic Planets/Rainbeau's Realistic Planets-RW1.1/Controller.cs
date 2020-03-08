﻿using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
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
		public override string SettingsCategory() { return "Planets.ModName".Translate(); }
		public override void DoSettingsWindowContents(Rect canvas) { Settings.DoWindowContents(canvas); }

		public Controller(ModContentPack content) : base(content) {
			const bool Debug = false;
			const string Id = "net.rainbeau.rimworld.mod.realisticplanets";

			Log.Warning($"Realistic Planets - Fan Update will be using Harmony as an external dependency when it next updates. To avoid wiping your mod list, please download and use Harmony now.");

			if (Debug)
			{
				Log.Warning($"{Id} is in debug mode. Contact the mod author if you see this.");
				Harmony.DEBUG = Debug;
			}

			var harmony = new Harmony(Id);
			harmony.PatchAll( Assembly.GetExecutingAssembly() );
			Settings = GetSettings<Settings>();
		}
	}
	
}
