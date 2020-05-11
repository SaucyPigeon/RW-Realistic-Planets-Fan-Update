using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using RimWorld.Planet;
using Verse;
using System.Diagnostics;

namespace Planets_Code.Patches.TileFinder
{
	class TileFinderState
	{
		int? i;

		float minTemp;
		float maxTemp;

		internal static TileFinderState Global;

		[Conditional("DEBUG")]
		internal void Log()
		{
			DebugLogger.Message("Logging tileFinderState");
			DebugLogger.Message(i.ToString());
			DebugLogger.Message(minTemp.ToString());
			DebugLogger.Message(maxTemp.ToString());

		}

		public bool ExcludedByTemperature(Tile tile)
		{
			this.Log();

			if (Controller.Settings.usingFactionControl)
			{
				return false;
			}
			if (!Controller.Settings.checkTemp)
			{
				return false;
			}

			if (!i.HasValue)
			{
				throw new InvalidOperationException("Counter must be set before calling this method.");
			}

			if (i < 1000)
			{
				if (tile.temperature < (minTemp - 45) || tile.temperature > (maxTemp + 45))
				{
					return true;
				}
				if (tile.temperature < (minTemp - 36) || tile.temperature > (maxTemp + 36))
				{
					if (Rand.Value > 0.1f)
					{
						return true;
					}
				}
				if (tile.temperature < (minTemp - 28) || tile.temperature > (maxTemp + 28))
				{
					if (Rand.Value > 0.2f)
					{
						return true;
					}
				}
				if (tile.temperature < (minTemp - 21) || tile.temperature > (maxTemp + 21))
				{
					if (Rand.Value > 0.3f)
					{
						return true;
					}
				}
				if (tile.temperature < (minTemp - 15) || tile.temperature > (maxTemp + 15))
				{
					if (Rand.Value > 0.4f)
					{
						return true;
					}
				}
				if (tile.temperature < (minTemp - 10) || tile.temperature > (maxTemp + 10))
				{
					if (Rand.Value > 0.5f)
					{
						return true;
					}
				}
				if (tile.temperature < (minTemp - 6) || tile.temperature > (maxTemp + 6))
				{
					if (Rand.Value > 0.6f)
					{
						return true;
					}
				}
				if (tile.temperature < (minTemp - 3) || tile.temperature > (maxTemp + 3))
				{
					if (Rand.Value > 0.7f)
					{
						return true;
					}
				}
				if (tile.temperature < (minTemp - 1) || tile.temperature > (maxTemp + 1))
				{
					if (Rand.Value > 0.8f)
					{
						return true;
					}
				}
				if (tile.temperature < minTemp || tile.temperature > maxTemp)
				{
					if (Rand.Value > 0.9f)
					{
						return true;
					}
				}
			}
			else if (i < 1500)
			{
				if (tile.temperature < (minTemp - 45) || tile.temperature > (maxTemp + 45))
				{
					if (Rand.Value > 0.2f)
					{
						return true;
					}
				}
				if (tile.temperature < (minTemp - 36) || tile.temperature > (maxTemp + 36))
				{
					if (Rand.Value > 0.4f)
					{
						return true;
					}
				}
				if (tile.temperature < (minTemp - 28) || tile.temperature > (maxTemp + 28))
				{
					if (Rand.Value > 0.6f)
					{
						return true;
					}
				}
				if (tile.temperature < (minTemp - 21) || tile.temperature > (maxTemp + 21))
				{
					if (Rand.Value > 0.8f)
					{
						return true;
					}
				}
			}
			return false;
		}

		public void SetCounter(int value)
		{
			DebugLogger.Message($"Setting counter value to {value}");

			i = value;
		}

		internal TileFinderState(Faction faction)
		{
			minTemp = -500f;
			maxTemp = +500f;

			if (faction != null)
			{
				if (!faction.IsPlayer)
				{
					if (faction.leader != null)
					{
						minTemp = faction.leader.def.GetStatValueAbstract(StatDefOf.ComfyTemperatureMin, null);
						maxTemp = faction.leader.def.GetStatValueAbstract(StatDefOf.ComfyTemperatureMax, null);
					}
				}
			}
		}
	}
}
