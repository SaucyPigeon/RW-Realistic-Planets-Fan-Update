using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld.Planet;
using RimWorld;
using Verse;
using UnityEngine;

namespace Planets_Code.Patches
{
	[HarmonyPatch(typeof(WITab_Planet))]
	[HarmonyPatch("Desc", MethodType.Getter)]
	public static class WITab_Planet_Desc
	{
		/*
		Had to move increase of planet tab win size to HarmonyPrepare().
		This is because the static constructor of Planets_Initializer is called after
		the constructor of WITab_Planet, which is called in WorldInspectPane.
		*/
		[HarmonyPrepare]
		public static void Prepare()
		{
			var winSizeField = AccessTools.Field(typeof(WITab_Planet), "WinSize");
			var winSizeValue = (Vector2)winSizeField.GetValue(obj: null);

			winSizeValue.y *= 2;

			winSizeField.SetValue(obj: null, winSizeValue);
		}

		[HarmonyPostfix]
		public static void Postfix(ref string __result)
		{
			var sb = new StringBuilder(__result);
			{
				var preset = Planets_GameComponent.worldPreset;
				var worldType = Planets_Strings.GetWorldType();
				var rainfall = Planets_Strings.GetRainfall();
				var temperature = Planets_Strings.GetTemperature();
				var axialTilt = Planets_Strings.GetAxialTilt();
				var population = Planets_Strings.GetPopulation();

				sb.Append("Planets.WorldPresets", preset);
				sb.Append("Planets.OceanType", worldType);
				sb.Append("PlanetRainfall", rainfall);
				sb.Append("PlanetTemperature", temperature);
				sb.Append("Planets.AxialTilt", axialTilt);
				sb.Append("PlanetPopulation", population);
			}
			__result = sb.ToString();
		}
	}
}
