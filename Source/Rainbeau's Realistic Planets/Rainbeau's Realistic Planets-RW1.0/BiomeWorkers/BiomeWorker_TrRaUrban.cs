﻿using RimWorld.Planet;
using Verse;

namespace Planets_Code
{
	public class BiomeWorker_TrRaUrban : BiomeWorker_RRP_TropicalRainforest
	{
		public BiomeWorker_TrRaUrban() { }
		public override float GetScore(Tile tile, int tileID)
		{
			float single;
			float single1 = 0.0008f;
			float score = base.GetScore(tile, tileID);
			single = ((score <= 0f ? false : Rand.Value <= single1) ? score + 0.1f : score - 1f);
			return single;
		}
	}

}
