﻿using RimWorld;
using System.Linq;
using Verse;
using HarmonyLib;
using RimWorld.Planet;
using UnityEngine;
using System;

namespace Planets_Code
{
	[StaticConstructorOnStartup]
	internal static class Planets_Initializer
	{
		private static void EditPlantDefs()
		{
			foreach (ThingDef current in DefDatabase<ThingDef>.AllDefsListForReading)
			{
				if (current.plant != null)
				{
					if (current.plant.wildBiomes != null)
					{
						for (int j = 0; j < current.plant.wildBiomes.Count; j++)
						{
							if (current.plant.wildBiomes[j].biome.defName == "Tundra")
							{
								PlantBiomeRecord newRecord = new PlantBiomeRecord();
								newRecord.biome = BiomeDef.Named("RRP_Permafrost");
								newRecord.commonality = current.plant.wildBiomes[j].commonality / 2;
								current.plant.wildBiomes.Add(newRecord);
							}
							if (current.plant.wildBiomes[j].biome.defName == "TemperateForest")
							{
								if (!current.defName.Contains("Tree"))
								{
									PlantBiomeRecord newRecord = new PlantBiomeRecord();
									newRecord.biome = BiomeDef.Named("RRP_Grassland");
									newRecord.commonality = current.plant.wildBiomes[j].commonality / 2;
									current.plant.wildBiomes.Add(newRecord);
								}
							}
							if (current.plant.wildBiomes[j].biome.defName == "BorealForest")
							{
								if (!current.defName.Contains("Tree"))
								{
									PlantBiomeRecord newRecord = new PlantBiomeRecord();
									newRecord.biome = BiomeDef.Named("RRP_Steppes");
									newRecord.commonality = current.plant.wildBiomes[j].commonality / 2;
									current.plant.wildBiomes.Add(newRecord);
								}
							}
							if (current.plant.wildBiomes[j].biome.defName == "AridShrubland")
							{
								if (!current.defName.Contains("Acacia"))
								{
									PlantBiomeRecord newRecord = new PlantBiomeRecord();
									newRecord.biome = BiomeDef.Named("RRP_Savanna");
									newRecord.commonality = current.plant.wildBiomes[j].commonality / 2;
									current.plant.wildBiomes.Add(newRecord);
								}
							}
							if (current.plant.wildBiomes[j].biome.defName == "Desert")
							{
								if (!current.defName.Contains("Acacia"))
								{
									PlantBiomeRecord newRecord = new PlantBiomeRecord();
									newRecord.biome = BiomeDef.Named("RRP_TemperateDesert");
									newRecord.commonality = current.plant.wildBiomes[j].commonality / 2;
									current.plant.wildBiomes.Add(newRecord);
									PlantBiomeRecord newRecord2 = new PlantBiomeRecord();
									newRecord2.biome = BiomeDef.Named("RRP_Oasis");
									newRecord2.commonality = current.plant.wildBiomes[j].commonality;
									current.plant.wildBiomes.Add(newRecord2);
								}
							}
						}
					}
				}
			}
		}

		private static void EditAnimalDefs()
		{
			foreach (PawnKindDef current in DefDatabase<PawnKindDef>.AllDefs)
			{
				if (current.RaceProps.wildBiomes != null && current.defName != "Cobra")
				{
					for (int j = 0; j < current.RaceProps.wildBiomes.Count; j++)
					{
						if (current.RaceProps.wildBiomes[j].biome.defName == "Tundra")
						{
							AnimalBiomeRecord newRecord = new AnimalBiomeRecord();
							newRecord.biome = BiomeDef.Named("RRP_Permafrost");
							newRecord.commonality = current.RaceProps.wildBiomes[j].commonality / 2;
							current.RaceProps.wildBiomes.Add(newRecord);
						}
						if (current.RaceProps.wildBiomes[j].biome.defName == "TemperateForest")
						{
							AnimalBiomeRecord newRecord = new AnimalBiomeRecord();
							newRecord.biome = BiomeDef.Named("RRP_Grassland");
							newRecord.commonality = current.RaceProps.wildBiomes[j].commonality / 2;
							current.RaceProps.wildBiomes.Add(newRecord);
						}
						if (current.RaceProps.wildBiomes[j].biome.defName == "BorealForest")
						{
							AnimalBiomeRecord newRecord = new AnimalBiomeRecord();
							newRecord.biome = BiomeDef.Named("RRP_Steppes");
							newRecord.commonality = current.RaceProps.wildBiomes[j].commonality / 2;
							current.RaceProps.wildBiomes.Add(newRecord);
						}
						if (current.RaceProps.wildBiomes[j].biome.defName == "AridShrubland")
						{
							AnimalBiomeRecord newRecord = new AnimalBiomeRecord();
							newRecord.biome = BiomeDef.Named("RRP_Savanna");
							newRecord.commonality = current.RaceProps.wildBiomes[j].commonality / 2;
							current.RaceProps.wildBiomes.Add(newRecord);
						}
						if (current.RaceProps.wildBiomes[j].biome.defName == "Desert")
						{
							AnimalBiomeRecord newRecord = new AnimalBiomeRecord();
							newRecord.biome = BiomeDef.Named("RRP_TemperateDesert");
							newRecord.commonality = current.RaceProps.wildBiomes[j].commonality / 2;
							current.RaceProps.wildBiomes.Add(newRecord);
							AnimalBiomeRecord newRecord2 = new AnimalBiomeRecord();
							newRecord2.biome = BiomeDef.Named("RRP_Oasis");
							newRecord2.commonality = current.RaceProps.wildBiomes[j].commonality;
							current.RaceProps.wildBiomes.Add(newRecord2);
						}
					}
				}
			}
		}

		private static void IncreasePlanetTabWinSize()
		{
			/*
			It is possible that WITab_Planet is constructed before field inits under
			RimWorld.Planet.WorldInspectPane::TileTabs
			*/

			//try
			//{
			//	var f = AccessTools.Field(typeof(WorldInspectPane), name: "TileTabs");
			//	var v = (WITab[])f.GetValue(null);

			//	var witab_planet = v[1];
			//	f = AccessTools.Field(typeof(WITab), name: "size");
			//	var v1 = (Vector2)f.GetValue(witab_planet);
			//	v1.y *= 2;
			//	f.SetValue(witab_planet, v1);
			//}
			//catch (Exception e)
			//{
			//	Log.Warning($"Realistic Planets - Fan Update has received an exception when trying to increase planet tab win size. Exception={e.ToString()}");
			//}

/*#if DEBUG
			Log.Warning("Increasing planet tab win size.");
#endif

			// Dummy construct to initialize static
			var _ = new WITab_Planet();

			var winSizeField = AccessTools.Field(typeof(WITab_Planet), "WinSize");
			var winSize = (Vector2)winSizeField.GetValue(null);
#if DEBUG
			Log.Warning($"WinSize (before): {winSize}");
#endif
			winSize.y *= 2;
			winSizeField.SetValue(null, winSize);

#if DEBUG
			Log.Warning($"WinSize (after): {winSize}");
			winSize = (Vector2)winSizeField.GetValue(null);
			Log.Warning($"WinSize (after, checked): {winSize}");
			#endif*/
		}

		static Planets_Initializer()
		{
#if DEBUG
			Log.Warning("Planets_Initializer running.");
#endif

			if (ModsConfig.ActiveModsInLoadOrder.Any(mod => mod.Name.Contains("My Little Planet")))
			{
				Controller.Settings.usingMLP = true;
			}
			if (ModsConfig.ActiveModsInLoadOrder.Any(mod => mod.Name.Contains("Faction Control")))
			{
#if DEBUG
				Log.Warning("Faction Control detected.");
#endif
				Controller.Settings.usingFactionControl = true;
			}
			if (ModsConfig.ActiveModsInLoadOrder.Any(mod => mod.Name.Contains("Nature's Pretty Sweet")))
			{
				Controller.Settings.otherGrassland = true;
				Controller.Settings.otherSavanna = true;
			}
			if (ModsConfig.ActiveModsInLoadOrder.Any(mod => mod.Name.Contains("Terra Project (Core)")))
			{
				Controller.Settings.otherSavanna = true;
			}
			if (ModsConfig.ActiveModsInLoadOrder.Any(mod => mod.Name.Contains("Advanced Biomes")))
			{
				Controller.Settings.otherSavanna = true;
			}
			if (ModsConfig.ActiveModsInLoadOrder.Any(mod => mod.Name.Contains("More Vanilla Biomes")))
			{
				Controller.Settings.otherGrassland = true;
				Controller.Settings.otherOasis = true;
			}
			EditPlantDefs();
			EditAnimalDefs();
			IncreasePlanetTabWinSize();
		}
	}
}
