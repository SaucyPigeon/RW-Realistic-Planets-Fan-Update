using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace Planets_Code
{
	public static class Planets_Scribe_Values
	{
		public static void Look<T>(this SettingsValue<T> settingsValue)
		{
			Guard.OnArgumentNull(settingsValue, nameof(settingsValue));

			Scribe_Values.Look(ref settingsValue.CurrentValue, settingsValue.Name, settingsValue.DefaultValue);
		}
	}
}
