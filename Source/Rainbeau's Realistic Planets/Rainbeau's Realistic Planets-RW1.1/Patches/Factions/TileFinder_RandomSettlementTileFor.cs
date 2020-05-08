using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Planets_Code.Factions
{
	internal class RandomSettlementTileFinder
	{
		readonly Faction faction;
		readonly bool mustBeAutoChoosable;
		readonly Predicate<int> extraValidator;

		readonly FloatRange temperatureRange;

		float minTemp => temperatureRange.min;
		float maxTemp => temperatureRange.max;

		bool tileFindAttempt = false;
		bool tileFound = false;

		private static FloatRange CalculateTemperatureRange(Faction faction)
		{
			float minTemp = -500f;
			float maxTemp = +500f;

			if (faction != null)
			{
				if (!faction.IsPlayer)
				{
					if (faction.leader != null)
					{
						minTemp = faction.leader.def.GetStatValueAbstract(StatDefOf.ComfyTemperatureMin, null);
						maxTemp = faction.leader.def.GetStatValueAbstract(StatDefOf.ComfyTemperatureMax, null);
					}
				}
			}
			return new FloatRange(minTemp, maxTemp);
		}
		
		private bool InTemperatureRange(Tile tile, int i)
		{
			/*
			FloatRange temperatureRange = GetTemperatureRange(faction)

			InTemperatureRange(tile, i, temperatureRange.min, temperatureRange.max)
			*/

			if (Controller.Settings.checkTemp.CurrentValue.Equals(true))
			{
				if (i < 1000)
				{
					if (tile.temperature < (minTemp - 45) || tile.temperature > (maxTemp + 45))
					{
						return false;
					}
					if (tile.temperature < (minTemp - 36) || tile.temperature > (maxTemp + 36))
					{
						if (Rand.Value > 0.1f)
						{
							return false;
						}
					}
					if (tile.temperature < (minTemp - 28) || tile.temperature > (maxTemp + 28))
					{
						if (Rand.Value > 0.2f)
						{
							return false;
						}
					}
					if (tile.temperature < (minTemp - 21) || tile.temperature > (maxTemp + 21))
					{
						if (Rand.Value > 0.3f)
						{
							return false;
						}
					}
					if (tile.temperature < (minTemp - 15) || tile.temperature > (maxTemp + 15))
					{
						if (Rand.Value > 0.4f)
						{
							return false;
						}
					}
					if (tile.temperature < (minTemp - 10) || tile.temperature > (maxTemp + 10))
					{
						if (Rand.Value > 0.5f)
						{
							return false;
						}
					}
					if (tile.temperature < (minTemp - 6) || tile.temperature > (maxTemp + 6))
					{
						if (Rand.Value > 0.6f)
						{
							return false;
						}
					}
					if (tile.temperature < (minTemp - 3) || tile.temperature > (maxTemp + 3))
					{
						if (Rand.Value > 0.7f)
						{
							return false;
						}
					}
					if (tile.temperature < (minTemp - 1) || tile.temperature > (maxTemp + 1))
					{
						if (Rand.Value > 0.8f)
						{
							return false;
						}
					}
					if (tile.temperature < minTemp || tile.temperature > maxTemp)
					{
						if (Rand.Value > 0.9f)
						{
							return false;
						}
					}
				}
				else if (i < 1500)
				{
					if (tile.temperature < (minTemp - 45) || tile.temperature > (maxTemp + 45))
					{
						if (Rand.Value > 0.2f)
						{
							return false;
						}
					}
					if (tile.temperature < (minTemp - 36) || tile.temperature > (maxTemp + 36))
					{
						if (Rand.Value > 0.4f)
						{
							return false;
						}
					}
					if (tile.temperature < (minTemp - 28) || tile.temperature > (maxTemp + 28))
					{
						if (Rand.Value > 0.6f)
						{
							return false;
						}
					}
					if (tile.temperature < (minTemp - 21) || tile.temperature > (maxTemp + 21))
					{
						if (Rand.Value > 0.8f)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		private Func<int, float> TemperatureWeightSelector(int i)
		{
			return (int x) =>
			{
				#region Shared with source
				Tile tile = Find.WorldGrid[x];
				if (!tile.biome.canBuildBase || !tile.biome.implemented || tile.hilliness == Hilliness.Impassable)
				{
					return 0f;
				}
				if (mustBeAutoChoosable && !tile.biome.canAutoChoose)
				{
					return 0f;
				}
				if (extraValidator != null && !extraValidator(x))
				{
					return 0f;
				}
				#endregion

				#region Temperature weighting
				if (!InTemperatureRange(tile, i))
				{
					return 0f;
				}
				#endregion
				return tile.biome.settlementSelectionWeight;
			};
		}

		private bool TryGetRandomTile(int i, out int tile)
		{
			return (from _ in Enumerable.Range(0, 100)
			select Rand.Range(0, Find.WorldGrid.TilesCount)).TryRandomElementByWeight<int>(TemperatureWeightSelector(i), out tile);
		}

		private bool IsValidTile(int tile)
		{
			if (TileFinder.IsValidTileForNewSettlement(tile, null))
			{
				if (faction == null || faction.def.hidden.Equals(true) || faction.def.isPlayer.Equals(true))
				{
					return true;
				}
				else if (Controller.factionCenters.ContainsKey(faction))
				{
					float test = Find.WorldGrid.ApproxDistanceInTiles(Controller.factionCenters[faction], tile);
					if (faction.def.defName == "Pirate" || faction.def.defName == "TribalRaiders")
					{
						if (test < (Controller.maxFactionSprawl * 3))
						{
							return true;
						}
					}
					else
					{
						if (test < Controller.maxFactionSprawl)
						{
							return true;
						}
					}
				}
				else
				{
					bool locationOK = true;
					foreach (KeyValuePair<Faction, int> factionCenter in Controller.factionCenters)
					{
						float test = Find.WorldGrid.ApproxDistanceInTiles(factionCenter.Value, tile);
						if (test < Controller.minFactionSeparation)
						{
							locationOK = false;
						}
					}
					if (locationOK.Equals(true))
					{
						Controller.factionCenters.Add(faction, tile);
						return true;
					}
				}
			}
			return false;
		}

		public int FindTile()
		{
			tileFindAttempt = true;
			for (int i = 0; i < 2500; i++)
			{
				if (TryGetRandomTile(i, out int tile) && IsValidTile(tile))
				{
					tileFound = true;
					return tile;
				}
			}
			return -1;
		}

		public bool TileFound()
		{
			if (!tileFindAttempt)
			{
				throw new InvalidOperationException($"Invalid state: attempting to test whether tile find operation was successful even though it has not been called.");
			}
			return tileFound;
		}

		public RandomSettlementTileFinder(Faction faction, bool mustBeAutoChoosable, Predicate<int> extraValidator)
		{
			this.faction = faction;
			this.mustBeAutoChoosable = mustBeAutoChoosable;
			this.extraValidator = extraValidator;

			this.temperatureRange = CalculateTemperatureRange(faction);
		}
	}


	[HarmonyPatch(typeof(TileFinder), "RandomSettlementTileFor", null)]
	public static class TileFinder_RandomSettlementTileFor
	{
		[HarmonyPriority(Priority.High)]
		public static bool Prefix(Faction faction, ref int __result, bool mustBeAutoChoosable = false, Predicate<int> extraValidator = null)
		{
			if (Controller.Settings.usingFactionControl.Equals(true))
			{
				return true;
			}

			var tileFinder = new RandomSettlementTileFinder(faction, mustBeAutoChoosable, extraValidator);

			__result = tileFinder.FindTile();

			if (tileFinder.TileFound())
			{
				return false;
			}

			Log.Warning(string.Concat("Failed to find faction base tile for ", faction));
			if (Controller.failureCount.ContainsKey(faction))
			{
				Controller.failureCount[faction]++;
				if (Controller.failureCount[faction] == 10)
				{
					Controller.failureCount.Remove(faction);
					if (Controller.factionCenters.ContainsKey(faction))
					{
						Controller.factionCenters.Remove(faction);
						Log.Warning("  Relocating faction center.");
					}
				}
			}
			else
			{
				Log.Warning("  Retrying.");
				Controller.failureCount.Add(faction,1);
			}
			__result = 0;
			return false;
		}
	}
	
}
