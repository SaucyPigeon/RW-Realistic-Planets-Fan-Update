using RimWorld;
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

		private static bool ModLoaded(string name)
		{
			return ModsConfig.ActiveModsInLoadOrder.Any(mod => mod.Name.Contains(name));
		}

		static Planets_Initializer()
		{
			/* A useful table showing which mods have overridden biomes:

									| Grasslands	| Savanna	| Oasis
			-----------------------------------------------------------
			Nature's Pretty Sweet	| YES			| YES		| no
			Terra Project (Core)	| no			| YES		| no
			Advanced Biomes			| no			| YES		| no
			More Vanilla Biomes		| no*"			| no		| YES*


				* can be disabled via that mod's settings

				" this mod has its own grasslands biome which is like a wet
					savannah. Keep Realistic Planets' own grasslands biome
					enabled (assuming no other mods) and use a special
					BiomeWorker replacing More Vanilla Biomes' grasslands.

					If Nature's Pretty Sweet is enabled, this shouldn't affect
					anything; RP's grasslands will be replaced by NPS's own,
					and MVB's grasslands will have its own special behavior.

			*/
			if (ModLoaded("My Little Planet"))
			{
				Controller.Settings.usingMLP = true;
			}
			if (ModLoaded("Faction Control"))
			{
				Controller.Settings.usingFactionControl = true;
			}
			if (ModLoaded("Nature's Pretty Sweet"))
			{
				Controller.Settings.otherGrassland = true;
				Controller.Settings.otherSavanna = true;
			}
			if (ModLoaded("Terra Project (Core)"))
			{
				Controller.Settings.otherSavanna = true;
			}
			if (ModLoaded("Advanced Biomes"))
			{
				Controller.Settings.otherSavanna = true;
			}
			if (ModLoaded("More Vanilla Biomes"))
			{
				Controller.Settings.usingMoreVanillaBiomes = true;
				// Keep this setting in place. Oases may be used by other mods.
				Controller.Settings.otherOasis = true;
			}
			EditPlantDefs();
			EditAnimalDefs();
		}
	}
}
