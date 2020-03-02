using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld.Planet;
using Verse;

namespace Planets_Code
{
	public static class Planets_Strings
	{
		public static string GetTemperatureStringKey(OverallTemperature temperature)
		{
			switch (temperature)
			{
				case OverallTemperature.VeryCold:
					return "Planets.Temperature_VeryCold";

				case OverallTemperature.Cold:
					return "Planets.Temperature_Cold";

				case OverallTemperature.LittleBitColder:
					return "Planets.Temperature_LittleBitColder";

				case OverallTemperature.Normal:
					return "Planets.Temperature_Normal";

				case OverallTemperature.LittleBitWarmer:
					return "Planets.Temperature_LittleBitWarmer";

				case OverallTemperature.Hot:
					return "Planets.Temperature_Hot";

				case OverallTemperature.VeryHot:
					return "Planets.Temperature_VeryHot";
			
				default:
					break;
			}
			throw new ArgumentOutOfRangeException(nameof(temperature));
		}

		public static string GetTemperatureStringKey()
		{
			return GetTemperatureStringKey(Find.World.info.overallTemperature);
		}

		public static string GetWorldTypeStringKey(WorldType worldType)
		{
			switch (worldType)
			{
				case WorldType.Waterworld:
					return "Planets.OceanType_Waterworld";

				case WorldType.Islands:
					return "Planets.OceanType_Islands";

				case WorldType.Earthlike:
					return "Planets.OceanType_Earthlike";

				case WorldType.Vanilla:
					return "Planets.OceanType_Vanilla";

				case WorldType.Dry:
					return "Planets.OceanType_Dry";

				case WorldType.VeryDry:
					return "Planets.OceanType_VeryDry";

				case WorldType.Barren:
					return "Planets.OceanType_Barren";

				default:
					break;
			}
			throw new ArgumentOutOfRangeException(nameof(worldType));
		}

		public static string GetWorldTypeStringKey()
		{
			return GetWorldTypeStringKey(Planets_GameComponent.worldType);
		}

		public static string GetAxialTiltStringKey(AxialTilt axialTilt)
		{
			switch (axialTilt)
			{
				case AxialTilt.VeryLow:
					return "Planets.AxialTilt_VeryLow";

				case AxialTilt.Low:
					return "Planets.AxialTilt_Low";

				case AxialTilt.Normal:
					return "Planets.AxialTilt_Normal";

				case AxialTilt.High:
					return "Planets.AxialTilt_High";

				case AxialTilt.VeryHigh:
					return "Planets.AxialTilt_VeryHigh";

				default:
					break;
			}
			throw new ArgumentOutOfRangeException(nameof(axialTilt));
		}

		public static string GetAxialTiltStringKey()
		{
			return GetAxialTiltStringKey(Planets_GameComponent.axialTilt);
		}

		public static string GetPopulationStringKey(OverallPopulation population)
		{
			switch (population)
			{
				case OverallPopulation.AlmostNone:
					return "Planets.Population_AlmostNone";

				case OverallPopulation.Little:
					return "Planets.Population_Little";

				case OverallPopulation.LittleBitLess:
					return "Planets.Population_LittleBitLess";

				case OverallPopulation.Normal:
					return "Planets.Population_Normal";

				case OverallPopulation.LittleBitMore:
					return "Planets.Population_LittleBitMore";

				case OverallPopulation.High:
					return "Planets.Population_High";

				case OverallPopulation.VeryHigh:
					return "Planets.Population_VeryHigh";

				default:
					break;
			}
			throw new ArgumentOutOfRangeException(nameof(population));
		}

		public static string GetPopulationStringKey()
		{
			return GetPopulationStringKey(Find.World.info.overallPopulation);
		}

		public static string GetRainfallStringKey(OverallRainfall rainfall)
		{
			switch (rainfall)
			{
				case OverallRainfall.AlmostNone:
					return "Planets.Rainfall_AlmostNone";

				case OverallRainfall.Little:
					return "Planets.Rainfall_Little";

				case OverallRainfall.LittleBitLess:
					return "Planets.Rainfall_LittleBitLess";

				case OverallRainfall.Normal:
					return "Planets.Rainfall_Normal";

				case OverallRainfall.LittleBitMore:
					return "Planets.Rainfall_LittleBitMore";

				case OverallRainfall.High:
					return "Planets.Rainfall_High";

				case OverallRainfall.VeryHigh:
					return "Planets.Rainfall_VeryHigh";

				default:
					break;
			}
			throw new ArgumentOutOfRangeException(nameof(rainfall));
		}

		public static string GetRainfallStringKey()
		{
			return GetRainfallStringKey(Find.World.info.overallRainfall);
		}
	}
}
