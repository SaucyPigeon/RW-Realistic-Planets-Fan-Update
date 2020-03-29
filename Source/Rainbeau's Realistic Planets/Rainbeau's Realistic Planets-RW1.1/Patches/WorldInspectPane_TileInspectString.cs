using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using RimWorld.Planet;
using HarmonyLib;
using Verse;

namespace Planets_Code.Patches
{
	[HarmonyPatch(typeof(WorldInspectPane))]
	[HarmonyPatch(MethodType.Getter)]
	[HarmonyPatch("TileInspectString")]
	public static class WorldInspectPane_TileInspectString
	{
		[HarmonyPostfix]
		public static void Postfix(ref string __result)
		{
			var sb = new StringBuilder(__result);
			sb.AppendLine();
			int selTileID = Find.WorldSelector.selectedTile;
			sb.Append(Zone_Growing.GrowingQuadrumsDescription(selTileID));
			__result = sb.ToString();
		}
	}
}
