using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld.Planet;
using RimWorld;
using Verse;

namespace Planets_Code.Patches
{
	[HarmonyPatch(typeof(WITab_Planet))]
	[HarmonyPatch("Desc", MethodType.Getter)]
	public static class WITab_Planet_Desc
	{
		private static void Append(this StringBuilder stringBuilder, string key, string value)
		{
			stringBuilder.Append(key.Translate());
			stringBuilder.Append(": ");
			stringBuilder.Append(value.Translate());
			stringBuilder.Append("\n");
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

				sb.Append("Planets.Preset", preset);
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
