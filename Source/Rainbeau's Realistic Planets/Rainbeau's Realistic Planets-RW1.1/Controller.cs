using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Verse;
using System.Diagnostics;

namespace Planets_Code
{
	public class Controller : Mod
	{
		public static Dictionary<Faction, int> factionCenters = new Dictionary<Faction, int>();
		public static Dictionary<Faction, int> failureCount = new Dictionary<Faction, int>();
		public static double maxFactionSprawl = 0;
		public static double minFactionSeparation = 0;
		public static Settings Settings;

		// Targetting other mods
		public static MethodInfo FactionControlSettingsMI = null;
		public static MethodInfo RimWarSettingsMI = null;
		public static FieldInfo MoreVanillaBiomesGrasslandsSettingFI = null;
		public static FieldInfo MoreVanillaBiomesOasisSettingFI = null;

		public const bool Debug = false;

		public override string SettingsCategory()
		{
			return "Planets.ModName".Translate();
		}

		public override void DoSettingsWindowContents(Rect canvas)
		{
			Settings.DoWindowContents(canvas);
		}

		public Controller(ModContentPack content) : base(content)
		{ 
			const string Id = "net.saucypigeon.rimworld.mod.realisticplanets";

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
			// Faction Control's button for CreateWorld page.

			var fcData = new ModMethodData(
				packageId: "factioncontrol.kv.rw",
				typeName: "FactionControl.Patch_Page_CreateWorldParams_DoWindowContents",
				methodName: "OpenSettingsWindow");

			FactionControlSettingsMI = fcData.GetMethodIfLoaded();

			if (Settings.usingFactionControl && FactionControlSettingsMI == null)
			{
				throw new MissingMethodException("Realistic Planets was unable to find necessary Faction Control method info.");
			}


			// Rim War's button for CreateWorld page.

			var rwData = new ModMethodData(
				packageId: "Torann.RimWar",
				typeName: "RimWar.Harmony.RimWarMod+Patch_Page_CreateWorldParams_DoWindowContents",
				methodName: "OpenSettingsWindow");

			RimWarSettingsMI = rwData.GetMethodIfLoaded();

			if (Settings.usingRimWar && RimWarSettingsMI == null)
			{
				throw new MissingMethodException("Realistic Planets was unable to find necessary Rim War method info.");
			}


			// More Vanilla Biomes - get access to their settings so we can
			// emulate their enable/disable feature.

			var mbvGrasslandsSetting = new ModFieldData(
				packageId: "zylle.MoreVanillaBiomes",
				typeName: "VanillaBiomes.BiomeSettings",
				fieldName: "spawnGrasslands"
				);

			var mbvOasisSetting = new ModFieldData(
				packageId: "zylle.MoreVanillaBiomes",
				typeName: "VanillaBiomes.BiomeSettings",
				fieldName: "spawnDesertOasis"
				);

			MoreVanillaBiomesGrasslandsSettingFI = mbvGrasslandsSetting.GetFieldIfLoaded();
			MoreVanillaBiomesOasisSettingFI = mbvOasisSetting.GetFieldIfLoaded();

			if (Settings.usingMoreVanillaBiomes && (MoreVanillaBiomesGrasslandsSettingFI == null || MoreVanillaBiomesOasisSettingFI == null))
			{
				throw new MissingFieldException("Realistic Planets was unable to find necessary More Vanilla Biomes field info.");
			}
		}
	}
}
