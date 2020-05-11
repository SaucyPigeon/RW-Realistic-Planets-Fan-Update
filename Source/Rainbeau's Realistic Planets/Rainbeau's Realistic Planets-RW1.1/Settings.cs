using UnityEngine;
using Verse;
using System.Diagnostics;

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
		public SettingsValue<bool> checkTemp = SettingsValue<bool>.Create("checkTemp", true);
		public SettingsValue<float> factionGrouping = SettingsValue<float>.Create("factionGrouping", 2.5f);

		// Descriptions
		public SettingsValue<bool> showStoneTypes = SettingsValue<bool>.Create("showStoneTypes", false);
		public SettingsValue<bool> showGrowingPeriod = SettingsValue<bool>.Create("showGrowingPeriod", true);
		public SettingsValue<bool> showDiseaseFrequency = SettingsValue<bool>.Create("showDiseaseFrequency", false);
		
		[Conditional("DEBUG")]
		internal void LogSettings()
		{
			DebugLogger.Message("Current settings:");
			DebugLogger.Message(checkTemp.ToString());
			DebugLogger.Message(factionGrouping.ToString());
			DebugLogger.Message(showStoneTypes.ToString());
			DebugLogger.Message(showGrowingPeriod.ToString());
			DebugLogger.Message(showDiseaseFrequency.ToString());
		}

		public void DoWindowContents(Rect canvas)
		{
			Listing_Standard list = new Listing_Standard();
			list.ColumnWidth = canvas.width;
			list.Begin(canvas);
			list.Gap(24);
			// Faction settings
			if (Controller.Settings.usingFactionControl.Equals(true)) {
				list.Label("Planets.SettingsDisabled".Translate());
			}
			else {
				list.CheckboxLabeled( "Planets.CheckTemp".Translate(), ref checkTemp.CurrentValue, "Planets.CheckTempTip".Translate() );
				list.Gap(24);
				factionGrouping.CurrentValue = list.Slider(factionGrouping.CurrentValue, 0, 3.99f);
				if (factionGrouping.CurrentValue < 1) {
					list.Label("Planets.FactionGrouping".Translate()+"  "+"Planets.FactionGroupingNone".Translate());
				}
				else if (factionGrouping.CurrentValue < 2 ) {
					list.Label("Planets.FactionGrouping".Translate()+"  "+"Planets.FactionGroupingLoose".Translate());
				}
				else if (factionGrouping.CurrentValue < 3) {
					list.Label("Planets.FactionGrouping".Translate()+"  "+"Planets.FactionGroupingTight".Translate());
				}
				else {
					list.Label("Planets.FactionGrouping".Translate()+"  "+"Planets.FactionGroupingVeryTight".Translate());
				}
			}
			// World inspect pane information
			const float wipInfoGap = 12f;

			list.Gap(wipInfoGap);
			list.CheckboxLabeled("Planets.ShowStoneTypes".Translate(), ref showStoneTypes.CurrentValue, "Planets.ShowStoneTypesTip".Translate());

			list.Gap(wipInfoGap);
			list.CheckboxLabeled("Planets.ShowGrowingPeriod".Translate(), ref showGrowingPeriod.CurrentValue, "Planets.ShowGrowingPeriodTip".Translate());

			list.Gap(wipInfoGap);
			list.CheckboxLabeled("Planets.ShowDiseaseFrequency".Translate(), ref showDiseaseFrequency.CurrentValue, "Planets.ShowDiseaseFrequencyTip".Translate());

			list.Gap(24);
			if (list.ButtonText("Planets.ResetToDefault".Translate()))
			{
				this.checkTemp.Reset();
				this.factionGrouping.Reset();
				this.showStoneTypes.Reset();
				this.showGrowingPeriod.Reset();
				this.showDiseaseFrequency.Reset();
			}
			list.End();
		}

		public override void ExposeData()
		{
			base.ExposeData();
			Planets_Scribe_Values.Look(checkTemp);
			Planets_Scribe_Values.Look(factionGrouping);
			Planets_Scribe_Values.Look(showStoneTypes);
			Planets_Scribe_Values.Look(showGrowingPeriod);
			Planets_Scribe_Values.Look(showDiseaseFrequency);
		}
	}
}
