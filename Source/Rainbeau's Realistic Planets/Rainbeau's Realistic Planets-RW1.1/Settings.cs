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
		public bool checkTemp = true;
		public float factionGrouping = 2.5f;

		// Added descriptions
		public bool showGrowingPeriod = true;
		

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
				list.CheckboxLabeled( "Planets.CheckTemp".Translate(), ref checkTemp, "Planets.CheckTempTip".Translate() );
				list.Gap(24);
				factionGrouping = list.Slider(factionGrouping, 0, 3.99f);
				if (factionGrouping < 1) {
					list.Label("Planets.FactionGrouping".Translate()+"  "+"Planets.FactionGroupingNone".Translate());
				}
				else if (factionGrouping < 2 ) {
					list.Label("Planets.FactionGrouping".Translate()+"  "+"Planets.FactionGroupingLoose".Translate());
				}
				else if (factionGrouping < 3) {
					list.Label("Planets.FactionGrouping".Translate()+"  "+"Planets.FactionGroupingTight".Translate());
				}
				else {
					list.Label("Planets.FactionGrouping".Translate()+"  "+"Planets.FactionGroupingVeryTight".Translate());
				}
			}
			// Show growing period in world inspect pane
			list.Gap(24);
			list.CheckboxLabeled("Planets.ShowGrowingPeriod".Translate(), ref showGrowingPeriod, "Planets.ShowGrowingPeriodTip".Translate());
			list.End();
		}

		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look(ref checkTemp, "checkTemp", true);
			Scribe_Values.Look(ref factionGrouping, "factionGrouping", 2.5f);
			Scribe_Values.Look(ref showGrowingPeriod, "showGrowingPeriod", true);
		}
	}
	
}
