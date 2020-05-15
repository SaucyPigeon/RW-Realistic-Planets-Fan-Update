using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using RimWorld.Planet;
using Verse;
using HarmonyLib;
using System.Reflection.Emit;

namespace Planets_Code.Patches
{
	[HarmonyPatch(typeof(WorldGenStep_Terrain))]
	[HarmonyPatch("SetupHillinessNoise")]
	[HarmonyPatch(new Type[] { })]
	public static class WorldGenStep_Terrain_SetupHillinessNoise
	{
		private static float AffectHillinessFrequency(float frequency)
		{
			return frequency * (float)Planets_GameComponent.hillinessModifier;
		}

		[HarmonyTranspiler]
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			var mi_freqMultiplier = AccessTools.PropertyGetter(typeof(WorldGenStep_Terrain), "FreqMultiplier");
			var mi_AffectHillinessFrequency = AccessTools.Method(typeof(WorldGenStep_Terrain_SetupHillinessNoise), nameof(AffectHillinessFrequency));

			if (mi_freqMultiplier == null)
				throw new InvalidOperationException($"Could not find property getter for FreqMultiplier");
			if (mi_AffectHillinessFrequency == null)
				throw new InvalidOperationException($"Could not find method info for {nameof(WorldGenStep_Terrain_SetupHillinessNoise)}::{nameof(AffectHillinessFrequency)}");

			foreach (var instruction in instructions)
			{
				yield return instruction;

				if (instruction.Calls(mi_freqMultiplier))
				{
					yield return new CodeInstruction(OpCodes.Call, mi_AffectHillinessFrequency);
				}
			}
		}
	}
}
