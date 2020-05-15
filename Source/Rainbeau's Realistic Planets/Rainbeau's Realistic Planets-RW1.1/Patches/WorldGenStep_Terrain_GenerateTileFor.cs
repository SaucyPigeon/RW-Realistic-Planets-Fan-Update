using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
using HarmonyLib;
using RimWorld.Planet;
using System.Diagnostics;
using System.Globalization;

namespace Planets_Code.Patches
{
#if DEBUG
	public class HillinessRecorder
	{
		readonly IDictionary<Hilliness, int> counts;

		public void Record(Hilliness hilliness)
		{
			if (counts.ContainsKey(hilliness))
			{
				counts[hilliness]++;
			}
			else
			{
				counts[hilliness] = 1;
			}
		}

		[Conditional("DEBUG")]
		public void Log()
		{
			DebugLogger.Message($"Seed: {Find.World.info.seedString}");
			DebugLogger.Message("Reporting counts of hilliness generated:");
			foreach (var pair in counts)
			{
				DebugLogger.Message($"{pair.Key}: {pair.Value}");
			}

			DebugLogger.Message("Reporting hilliness generated as percentage of total:");
			var total = counts.Values.Sum();
			
			foreach (var pair in counts)
			{
				float percentageValue = pair.Value / (float)total;
				DebugLogger.Message($"{pair.Key}: {percentageValue.ToString("P", CultureInfo.InvariantCulture)}");
			}

		}

		public HillinessRecorder()
		{
			counts = new Dictionary<Hilliness, int>();
		}
	}

	[HarmonyPatch(typeof(WorldGenStep_Terrain))]
	[HarmonyPatch("GenerateTileFor")]
	[HarmonyPatch(new[] { typeof(int) })]
	public static class WorldGenStep_Terrain_GenerateTileFor
	{
		static readonly HillinessRecorder recorder = new HillinessRecorder();

		[HarmonyPostfix]
		private static void Postfix(Tile __result)
		{
			var hilliness = __result.hilliness;
			recorder.Record(hilliness);
		}

		public static void ReportHilliness()
		{
			recorder.Log();
		}
	}

	[HarmonyPatch(typeof(WorldGenStep_Terrain))]
	[HarmonyPatch("GenerateGridIntoWorld")]
	public static class WorldGenStep_Terrain_GenerateGridIntoWorld
	{
		[HarmonyPostfix]
		private static void Postfix()
		{
			WorldGenStep_Terrain_GenerateTileFor.ReportHilliness();
		}
	}
#endif
}
