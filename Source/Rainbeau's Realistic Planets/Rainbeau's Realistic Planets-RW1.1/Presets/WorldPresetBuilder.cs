using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld.Planet;

namespace Planets_Code.Presets
{
	public class WorldPresetBuilder
	{
		public string name;
		public WorldType? worldType;
		public AxialTilt? axialTilt;
		public RainfallModifier? rainfallModifier;
		public OverallTemperature? temperature;
		public OverallPopulation? population;

		public WorldPresetBuilder Name(string value)
		{
			this.name = value;
			return this;
		}

		public WorldPresetBuilder WorldType(WorldType value)
		{
			this.worldType = value;
			return this;
		}

		public WorldPresetBuilder AxialTilt(AxialTilt value)
		{
			this.axialTilt = value;
			return this;
		}

		public WorldPresetBuilder RainfallModifier(RainfallModifier value)
		{
			this.rainfallModifier = value;
			return this;
		}

		public WorldPresetBuilder Temperature(OverallTemperature value)
		{
			this.temperature = value;
			return this;
		}

		public WorldPresetBuilder Population(OverallPopulation value)
		{
			this.population = value;
			return this;
		}

		public WorldPreset Build()
		{
			Guard.OnArgumentNull(name, nameof(name));
			Guard.OnArgumentNull(worldType, nameof(worldType));
			Guard.OnArgumentNull(axialTilt, nameof(axialTilt));
			Guard.OnArgumentNull(rainfallModifier, nameof(rainfallModifier));
			Guard.OnArgumentNull(temperature, nameof(temperature));
			Guard.OnArgumentNull(population, nameof(population));

			return new WorldPreset(name, worldType.Value, axialTilt.Value, rainfallModifier.Value, temperature.Value, population.Value);
		}

		public WorldPresetBuilder()
		{

		}
	}
}
