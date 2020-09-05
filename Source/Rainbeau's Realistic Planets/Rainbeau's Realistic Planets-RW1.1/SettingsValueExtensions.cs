using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace Planets_Code
{
	public static class SettingsValueExtensions
	{
		public static void ResetToDefault<T>(this SettingsValue<T> value)
		{
			value.CurrentValue = value.DefaultValue;
		}

		public static void CheckboxBool(this Listing_Standard list, SettingsValue<bool> settingsValue)
		{
			string label = settingsValue.Label.Translate();
			string tooltip = settingsValue.Tooltip.Translate();
			
			list.CheckboxLabeled(label, ref settingsValue.CurrentValue, tooltip);
		}
	}
}
