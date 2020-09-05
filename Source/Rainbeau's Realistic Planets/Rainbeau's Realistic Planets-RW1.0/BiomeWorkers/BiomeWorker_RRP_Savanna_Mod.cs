﻿using RimWorld;
using RimWorld.Planet;

namespace Planets_Code
{
	// Used for Terra Core, Advanced Biomes and Nature's Pretty Sweet

	public class BiomeWorker_RRP_Savanna_Mod : BiomeWorker
	{
		public BiomeWorker_RRP_Savanna_Mod() { }
		public override float GetScore(Tile tile, int tileID)
		{
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
			if (tile.rainfall < 600f)
			{
				return 0f;
			}
			float tempAdjust = TempCheck.SeasonalTempChange(tileID);
			if (tile.rainfall >= 2000f && (tile.temperature - tempAdjust <= 42f))
			{
				return 0f;
			}
			return 22.5f + (tile.temperature - 20f) * 2.2f + (tile.rainfall - 600f) / 100f;
		}
	}

}
