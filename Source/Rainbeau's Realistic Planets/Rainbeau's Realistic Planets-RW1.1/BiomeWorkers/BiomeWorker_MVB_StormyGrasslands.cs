using RimWorld;
using RimWorld.Planet;
using Verse;

namespace Planets_Code.BiomeWorkers
{
	/// <summary>
	/// The special BiomeWorker for More Vanilla Biome's grasslands. Note that
	/// its name is intended to be changed by MVB.
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

			throw new System.Exception("This BiomeWorker is not fully implemented.");
			return 100f;
		}
	}
}
