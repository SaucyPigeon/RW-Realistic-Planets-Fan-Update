using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using System.Reflection;
using System.Reflection.Emit;
using Verse;
using RimWorld;
using RimWorld.Planet;

namespace Planets_Code.Patches.TileFinder
{
	using TileFinder = RimWorld.Planet.TileFinder;

	[HarmonyPatch]
	static class TileFinder_RandomSettlementTileFor_DisplayClass
	{
		#region Called by transpiler

		static bool ExcludedByTemperature(Tile tile)
		{
			return TileFinderState.Global.ExcludedByTemperature(tile);
		}

		#endregion

		[HarmonyTargetMethod]
		static MethodBase TargetMethod()
		{
			var type = typeof(TileFinder).GetNestedType("<>c__DisplayClass1_0", AccessTools.all);
			var method = type.GetMethods(AccessTools.all).Where(x => !x.IsConstructor).First();

			return method;
		}

		[HarmonyTranspiler]
		static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilGenerator)
		{
			// Target: last ldloc.0 instruction
			// Add:
			// [copy labels]
			// load arg tile
			// call bool ExcludedByTemperature(tile)
			// brfalse: block end
			// load float 0
			// br: last instruction
			// label: block end
			// (ldloc.0)
			// (add last instruction label)

			var targetInstruction = instructions.Where(x => x.opcode == OpCodes.Ldloc_0).Last();
			var lastInstruction = instructions.Last();

			var mi_ExcludedByTemperature = AccessTools.Method(typeof(TileFinder_RandomSettlementTileFor_DisplayClass), nameof(ExcludedByTemperature));

			var methodEnd = ilGenerator.DefineLabel();

			foreach (var instruction in instructions)
			{
				if (instruction == targetInstruction)
				{
					var blockEnd = ilGenerator.DefineLabel();

					yield return new CodeInstruction(OpCodes.Ldloc_0) { labels = instruction.labels.ListFullCopy() };
					instruction.labels.Clear();
					yield return new CodeInstruction(OpCodes.Call, mi_ExcludedByTemperature);
					yield return new CodeInstruction(OpCodes.Brfalse, blockEnd);
					yield return new CodeInstruction(OpCodes.Ldc_R4, 0f);
					yield return new CodeInstruction(OpCodes.Br, methodEnd);
					yield return new CodeInstruction(OpCodes.Nop) { labels = new List<Label>() { blockEnd } };
				}
				if (instruction == lastInstruction)
				{
					instruction.labels.Add(methodEnd);
				}
				yield return instruction;
			}
		}
	}
}