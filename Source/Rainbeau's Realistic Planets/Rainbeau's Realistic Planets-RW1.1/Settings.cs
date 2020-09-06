using UnityEngine;
using Verse;
using System;
using System.Collections.Generic;

namespace Planets_Code
{
	public class Settings : ModSettings
	{
		// Biome
		public bool otherGrassland = false;
		public bool otherSavanna = false;
		public bool otherOasis = false;

		// Generation
		public bool usingMLP = false;
		public bool usingFactionControl = false;
		public bool randomPlanet = false;

		// Factions
		public SettingsValue<bool> checkTemp = new SettingsValue<bool>(defaultValue: true, name: "checkTemp");
		public SettingsValue<float> factionGrouping = new SettingsValue<float>(defaultValue: 2.5f, name: "factionGrouping");

		// Descriptions
		public SettingsValue<bool> showStoneTypes = new SettingsValue<bool>(defaultValue: false, name: "showStoneTypes");
		public SettingsValue<bool> showGrowingPeriod = new SettingsValue<bool>(defaultValue: true, name: "showGrowingPeriod");
		public SettingsValue<bool> showDiseaseFrequency = new SettingsValue<bool>(defaultValue: false, name: "showDiseaseFrequency");
		public SettingsValue<bool> enableDebugLogging = new SettingsValue<bool>(defaultValue: false, name: "enableDebugLogging");


		public readonly List<SettingsValue<bool>> boolSettings = new List<SettingsValue<bool>>();
		public readonly List<SettingsValue<float>> floatSettings = new List<SettingsValue<float>>();

		public void DoWindowContents(Rect canvas)
		{
			Listing_Standard list = new Listing_Standard();
			list.ColumnWidth = canvas.width;
			list.Begin(canvas);
			list.Gap(24);
			// Faction settings
			if (Controller.Settings.usingFactionControl.Equals(true))
			{
				list.Label("Planets.SettingsDisabled".Translate());
			}
			else
			{
				list.CheckboxBool(checkTemp);
				list.Gap(24);
				factionGrouping.CurrentValue = list.Slider(factionGrouping.CurrentValue, 0, 3.99f);
				if (factionGrouping.CurrentValue < 1)
				{
					list.Label("Planets.FactionGrouping".Translate() + "  " + "Planets.FactionGroupingNone".Translate());
				}
				else if (factionGrouping.CurrentValue < 2)
				{
					list.Label("Planets.FactionGrouping".Translate() + "  " + "Planets.FactionGroupingLoose".Translate());
				}
				else if (factionGrouping.CurrentValue < 3)
				{
					list.Label("Planets.FactionGrouping".Translate() + "  " + "Planets.FactionGroupingTight".Translate());
				}
				else
				{
					list.Label("Planets.FactionGrouping".Translate() + "  " + "Planets.FactionGroupingVeryTight".Translate());
				}
			}
			// World inspect pane information
			const float wipInfoGap = 12f;

			list.Gap(wipInfoGap);
			list.CheckboxBool(this.showStoneTypes);

			list.Gap(wipInfoGap);
			list.CheckboxBool(this.showGrowingPeriod);

			list.Gap(wipInfoGap);
			list.CheckboxBool(this.showDiseaseFrequency);

			if (Prefs.DevMode)
			{
				list.Gap(wipInfoGap);
				list.CheckboxBool(this.enableDebugLogging);
			}

			list.Gap(24);
			if (list.ButtonText("Planets.ResetToDefault".Translate()))
			{
				foreach (var setting in boolSettings)
				{
					setting.ResetToDefault();
				}
				foreach (var setting in floatSettings)
				{
					setting.ResetToDefault();
				}
			}
			list.End();
		}

		public override void ExposeData()
		{
			base.ExposeData();
			foreach (var setting in boolSettings)
			{
				Planets_Scribe_Values.Look<bool>(setting);
			}
			foreach (var setting in floatSettings)
			{
				Planets_Scribe_Values.Look<float>(setting);
			}
		}

		public Settings() : base()
		{
			// Register settings

			this.boolSettings.Add(checkTemp);
			this.boolSettings.Add(showStoneTypes);
			this.boolSettings.Add(showGrowingPeriod);
			this.boolSettings.Add(showDiseaseFrequency);
			this.boolSettings.Add(enableDebugLogging);

			this.floatSettings.Add(factionGrouping);
		}
	}
}
