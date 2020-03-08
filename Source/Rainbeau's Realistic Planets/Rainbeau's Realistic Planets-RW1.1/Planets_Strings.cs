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
		// Values to display:
		// Planet name*
		// Seed*
		// Coverage*
		// 
		// World preset
		// Sea level
		// Rainfall
		// Temperature
		// Axial tilt
		// Population
		//
		// *already in vanilla

		// {KEY}: {VALUE}

		public static string GetWorldType()
		{
			switch (Planets_GameComponent.worldType)
			{
				case WorldType.Waterworld:
				case WorldType.Islands:
				case WorldType.Earthlike:
				case WorldType.Vanilla:
				case WorldType.Dry:
				case WorldType.VeryDry:
				case WorldType.Barren:
				default:
					break;
			}
			throw new ArgumentOutOfRangeException(nameof(Planets_GameComponent.worldType));
		}

		public static string GetRainfall()
		{
			switch (Find.World.info.overallRainfall)
			{
				case OverallRainfall.AlmostNone:
				case OverallRainfall.Little:
				case OverallRainfall.LittleBitLess:
				case OverallRainfall.Normal:
				case OverallRainfall.LittleBitMore:
				case OverallRainfall.High:
				case OverallRainfall.VeryHigh:
				default:
					break;
			}
			throw new ArgumentOutOfRangeException(nameof(Find.World.info.overallRainfall));
		}

		public static string GetTemperature()
		{
			switch (Find.World.info.overallTemperature)
			{
				case OverallTemperature.VeryCold:
				case OverallTemperature.Cold:
				case OverallTemperature.LittleBitColder:
				case OverallTemperature.Normal:
				case OverallTemperature.LittleBitWarmer:
				case OverallTemperature.Hot:
				case OverallTemperature.VeryHot:
				default:
					break;
			}
			throw new ArgumentOutOfRangeException(nameof(Find.World.info.overallTemperature));
		}

		public static string GetAxialTilt()
		{
			switch (Planets_GameComponent.axialTilt)
			{
				case AxialTilt.VeryLow:
				case AxialTilt.Low:
				case AxialTilt.Normal:
				case AxialTilt.High:
				case AxialTilt.VeryHigh:
				default:
					break;
			}
			throw new ArgumentOutOfRangeException(nameof(Planets_GameComponent.axialTilt));
		}

		public static string GetPopulation()
		{
			switch (Find.World.info.overallPopulation)
			{
				case OverallPopulation.AlmostNone:
				case OverallPopulation.Little:
				case OverallPopulation.LittleBitLess:
				case OverallPopulation.Normal:
				case OverallPopulation.LittleBitMore:
				case OverallPopulation.High:
				case OverallPopulation.VeryHigh:
				default:
					break;
			}
			throw new ArgumentOutOfRangeException(nameof(Find.World.info.overallPopulation));
		}
	}
}
