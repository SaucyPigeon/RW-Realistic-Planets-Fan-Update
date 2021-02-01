using RimWorld;
using RimWorld.Planet;

namespace Planets_Code
{
	/// <summary>
	/// The BiomeWorker for modded grassland biomes. Although this mod adds its
	/// own version, it disables that one in favour of those from other mods.
	/// Currently only Nature's Pretty Sweet is affected. 
	/// </summary>
	public class BiomeWorker_RRP_Grassland_Mod : BiomeWorker
	{
		public BiomeWorker_RRP_Grassland_Mod()
		{

		}

		public override float GetScore(Tile tile, int tileID)
		{
			// This mimics More Vanilla Biome's enable/disable function.
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
			if (tile.temperature < -10f)
			{
				return 0f;
			}
			if (tile.rainfall < 170f)
			{
				return 0f;
			}
			float tempAdjust = TempCheck.SeasonalTempChange(tileID);
			if (tile.temperature + tempAdjust < 6f)
			{
				return 0f;
			}
			if (tile.rainfall >= 600f && (tile.temperature + tempAdjust >= 12f))
			{
				return 0f;
			}
			if (tile.temperature > 0f && (tile.rainfall / tile.temperature < 28f))
			{
				return 0f;
			}
			if (tile.temperature < 0f && (tile.rainfall / -tile.temperature < 50f))
			{
				return 0f;
			}
			if (tile.temperature < 0f)
			{
				return -tile.temperature + 0.0003f;
			}
			return tile.temperature + 0.0003f;
		}
	}
}
