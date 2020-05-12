using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using System.Reflection.Emit;
using RimWorld;
using Verse;

namespace Planets_Code.Patches.TileFinder
{
	using TileFinder = RimWorld.Planet.TileFinder;

	// Behold...
	[HarmonyPatch(typeof(TileFinder))]
	[HarmonyPatch(nameof(TileFinder.RandomSettlementTileFor))]
	static class TileFinder_RandomSettlementTileFor
	{
		#region Called by transpiler

		static bool UseFactionSprawl(int tile, Faction faction)
		{
			if (Controller.Settings.usingFactionControl)
			{
				return true;
			}

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
			return false;
		}

		static void AddFailureForFaction(Faction faction)
		{
			if (Controller.Settings.usingFactionControl)
			{
				return;
			}

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
				Controller.failureCount.Add(faction, 1);
			}
		}

		static IEnumerable<CodeInstruction> Transpiler_SetStateCounter(IEnumerable<CodeInstruction> instructions)
		{
			// Update state counter

			// Where stloc.2
			//	ldfld TileFinder_RandomSettlementTileFor::state
			//	ldloc 2
			//	call void TileFinder_RandomSettlementTileFor::SetCounter(int)

			DebugLogger.Message("Transpiler, set state counter.");

			var fi_State = AccessTools.Field(typeof(TileFinderState), nameof(TileFinderState.Global));
			var mi_SetCounter = AccessTools.Method(typeof(TileFinderState), nameof(TileFinderState.SetCounter));

			foreach (var instruction in instructions)
			{
				yield return instruction;

				if (instruction.opcode == OpCodes.Stloc_2)
				{
					yield return new CodeInstruction(OpCodes.Ldsfld, fi_State);
					yield return new CodeInstruction(OpCodes.Ldloc_2);
					yield return new CodeInstruction(OpCodes.Call, mi_SetCounter);
				}
			}
		}

		static IEnumerable<CodeInstruction> Transpiler_IncreaseLoopCounter(IEnumerable<CodeInstruction> instructions)
		{
			// Increase loop counter i from 500 to 2500

			// Where ldl.i4 500
			// operand -> 2500

			foreach (var instruction in instructions)
			{
				if (instruction.opcode == OpCodes.Ldc_I4 && instruction.OperandIs((int)500))
				{
					instruction.operand = (int)2500;
				}
				yield return instruction;
			}
		}

		static IEnumerable<CodeInstruction> Transpiler_GetFactionSprawlTile(IEnumerable<CodeInstruction> instructions, ILGenerator ilGenerator)
		{
			// Target: last ldloc.1
			// (ldloc.1)
			// ldarg.0
			// call bool UseFactionSprawl(int, Faction)
			// brfalse => END
			// ldloc.1

			// END = second ldloc.2 instruction

			var mi_UseFactionSprawl = AccessTools.Method(typeof(TileFinder_RandomSettlementTileFor), nameof(UseFactionSprawl));

			var targetInstruction = instructions.Where(x => x.opcode == OpCodes.Ldloc_1).Last();
			var labelInstruction = instructions.Where(x => x.opcode == OpCodes.Ldloc_2).Skip(1).First();

			var label = ilGenerator.DefineLabel();

			foreach (var instruction in instructions)
			{
				yield return instruction;

				if (instruction == targetInstruction)
				{
					yield return new CodeInstruction(OpCodes.Ldarg_0);
					yield return new CodeInstruction(OpCodes.Call, mi_UseFactionSprawl);
					yield return new CodeInstruction(OpCodes.Brfalse, label);

					yield return new CodeInstruction(OpCodes.Ldloc_1);
				}
				if (instruction == labelInstruction)
				{
					instruction.labels.Add(label);
				}
			}

		}

		static IEnumerable<CodeInstruction> Transpiler_AddFailureForFaction(IEnumerable<CodeInstruction> instructions)
		{
			// Target: Call void Verse.Log::Error(String, Boolean)
			// (call Log.Error)
			// ldarg.0 (faction)
			// call AddFailureForFaction(Faction)

			var mi_Log_Error = AccessTools.Method(typeof(Log), nameof(Log.Error));
			var mi_AddFailureForFaction = AccessTools.Method(typeof(TileFinder_RandomSettlementTileFor), nameof(AddFailureForFaction));

			foreach (var instruction in instructions)
			{
				yield return instruction;

				if (instruction.Calls(mi_Log_Error))
				{
					yield return new CodeInstruction(OpCodes.Ldarg_0);
					yield return new CodeInstruction(OpCodes.Call, mi_AddFailureForFaction);
				}
			}
		}

		#endregion

		[HarmonyPrefix]
		static void Prefix(Faction faction)
		{
			DebugLogger.Message("Prefix, RandomSettlementTileFor.");

			Controller.Settings.LogSettings();

			if (TileFinderState.Global != null)
			{
				throw new InvalidOperationException($"Global {nameof(TileFinderState)} is not null. Ensure that it is set to null when finished using it.");
			}
			TileFinderState.Global = new TileFinderState(faction);
		}

		[HarmonyPostfix]
		static void Postfix()
		{
			DebugLogger.Message("Postfix, RandomSettlementTileFor.");

			if (TileFinderState.Global == null)
			{
				throw new InvalidOperationException($"Global {nameof(TileFinderState)} is null. Ensure that it is only set to null when finished using it.");
			}
			TileFinderState.Global = null;
		}

		[HarmonyTranspiler]
		static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilGenerator)
		{
			DebugLogger.Message("Transpiler, RandomSettlementTileFor.");

			instructions = Transpiler_SetStateCounter(instructions);
			instructions = Transpiler_IncreaseLoopCounter(instructions);
			instructions = Transpiler_GetFactionSprawlTile(instructions, ilGenerator);
			instructions = Transpiler_AddFailureForFaction(instructions);
			return instructions;
		}
	}
}
