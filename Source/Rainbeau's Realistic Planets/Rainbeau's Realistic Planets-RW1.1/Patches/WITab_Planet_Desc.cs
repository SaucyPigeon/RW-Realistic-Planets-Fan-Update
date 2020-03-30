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
#if DEBUG
		public static void Postfix(ref string __result, WITab_Planet __instance)
#else
		public static void Postfix(ref string __result)
#endif
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

#if DEBUG
			// Check value of static field.
			// For some reason it is not applying in initializer...

			try
			{
				var field = AccessTools.Field(typeof(WITab_Planet), "WinSize");
				var value = field.GetValue(null);
			Log.Warning($"Value of winSize (static): {value}");
				field = AccessTools.Field(typeof(WITab_Planet), "size");
				value = field.GetValue(__instance);
				Log.Warning($"Value of size (instance): {value}");
			}
			catch (Exception e)
			{
				Log.Warning("Failed to get RimWorld.Planet.WITab_Planet::WinSize");
			}
#endif
		}
	}
}
