using UnityEngine;
using Verse;

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
		public SettingsValue<bool> showGrowingPeriod = new SettingsValue<bool>(defaultValue: true, name: "showGrowingPeriod");		

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
			// Show growing period in world inspect pane
			list.Gap(24);
			list.CheckboxLabeled("Planets.ShowGrowingPeriod".Translate(), ref showGrowingPeriod.CurrentValue, "Planets.ShowGrowingPeriodTip".Translate());

			if (list.ButtonText("Planets.ResetToDefault"))
			{
				this.checkTemp.ResetToDefault();
				this.factionGrouping.ResetToDefault();
				this.showGrowingPeriod.ResetToDefault();
			}
			list.End();
		}

		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look(ref checkTemp.CurrentValue, checkTemp.Name, checkTemp.DefaultValue);
			Scribe_Values.Look(ref factionGrouping.CurrentValue, factionGrouping.Name, factionGrouping.DefaultValue);
			Scribe_Values.Look(ref showGrowingPeriod.CurrentValue, showGrowingPeriod.Name, showGrowingPeriod.DefaultValue);
		}
	}
}
