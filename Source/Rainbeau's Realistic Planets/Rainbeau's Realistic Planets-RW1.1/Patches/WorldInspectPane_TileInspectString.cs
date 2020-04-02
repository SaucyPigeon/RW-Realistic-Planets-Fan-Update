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
	/*
	Adds the following to the world inspect pane:
	- Growing quadrums
	*/
	[HarmonyPatch(typeof(WorldInspectPane))]
	[HarmonyPatch("TileInspectString", MethodType.Getter)]
	public static class WorldInspectPane_TileInspectString
	{
		[HarmonyPostfix]
		public static void Postfix(ref string __result)
		{
			var sb = new StringBuilder(__result);
			{
				// Growing quadrums description
				if (Controller.Settings.showGrowingPeriod.CurrentValue)
				{
					sb.AppendLine();
					int selTileID = Find.WorldSelector.selectedTile;
					sb.Append("OutdoorGrowingPeriod".Translate(), Zone_Growing.GrowingQuadrumsDescription(selTileID), translate: false);
				}
			}
			__result = sb.ToString();
		}
	}
}
