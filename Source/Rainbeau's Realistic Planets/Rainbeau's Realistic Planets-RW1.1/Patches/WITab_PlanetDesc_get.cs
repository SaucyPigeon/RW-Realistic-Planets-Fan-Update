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
	public static class WITab_PlanetDesc_get
	{
		public static void Append(this StringBuilder stringBuilder, string key, string value)
		{
			stringBuilder.Append(key.Translate());
			stringBuilder.Append(": ");
			stringBuilder.Append(value.Translate());
			stringBuilder.Append("\n");
		}

		public static void Postfix(ref string __result)
		{
			var sb = new StringBuilder(__result);

			var worldTypeString = Planets_Strings.GetWorldTypeStringKey();
			sb.Append("Planets.OceanType", worldTypeString);

			var rainfallString = Planets_Strings.GetRainfallStringKey();
			sb.Append("PlanetRainfall", rainfallString);

			var temperatureString = Planets_Strings.GetTemperatureStringKey();
			sb.Append("PlanetTemperature", temperatureString);

			var axialTileString = Planets_Strings.GetAxialTiltStringKey();
			sb.Append("Planets.AxialTilt", axialTileString);

			var populationString = Planets_Strings.GetPopulationStringKey();
			sb.Append("PlanetPopulation", populationString);

			__result = sb.ToString();
		}
	}
}
