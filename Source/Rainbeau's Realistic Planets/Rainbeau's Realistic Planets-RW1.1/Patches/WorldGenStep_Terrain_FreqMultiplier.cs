using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using RimWorld.Planet;
using Verse;
using HarmonyLib;

namespace Planets_Code.Patches
{
#if false
	[HarmonyPatch(typeof(WorldGenStep_Terrain))]
	[HarmonyPatch(MethodType.Getter)]
	[HarmonyPatch("FreqMultiplier")]
	public static class WorldGenStep_Terrain_FreqMultiplier
	{
		[HarmonyPostfix]
		public static void Postfix(ref float __result)
		{
			DebugLogger.Message($"WorldGenStep_Terrain::FreqMultiplier postfix");
			DebugLogger.Message($"Value of __result (before): {__result}");
			DebugLogger.Message($"Hilliness modifier: {Planets_GameComponent.hillinessModifier}");

			__result = __result * Planets_GameComponent.hillinessModifier;

			DebugLogger.Message($"Value of __result (after): {__result}");
		}
	}
#endif
}
