using RimWorld;
using RimWorld.Planet;
using Verse;
using System;

namespace Planets_Code
{
	/// <summary>
	/// The special BiomeWorker for More Vanilla Biome's grasslands. Note that
	/// the biome's name is intended to be changed by MVB.
	/// </summary>
	public class BiomeWorker_MVB_StormyGrasslands : BiomeWorker
	{
		public BiomeWorker_MVB_StormyGrasslands()
		{

		}

		public override float GetScore(Tile tile, int tileID)
		{
			if (Controller.Settings.usingMoreVanillaBiomes)
			{
				if (!(bool)Controller.MoreVanillaBiomesGrasslandsSettingFI.GetValue(null))
				{
					return -100f;
				}
			}
			if (tile.WaterCovered)
			{
				return -100f;
			}
			if (tile.temperature <= 18f)
			{
				return 0f;
			}
			if (tile.temperature > 0f && (tile.rainfall / tile.temperature < 28f))
			{
				return 0f;
			}
			if (tile.rainfall < 1200f)
			{
				return 0f;
			}
			float tempAdjust = TempCheck.SeasonalTempChange(tileID);
			if (tile.rainfall >= 2000f && (tile.temperature - tempAdjust <= 42f))
			{
				return 0f;
			}
			return 22.5f + (tile.temperature - 20f) * 2.2f + (tile.rainfall - 1200f) / 50f;
		}
	}
}