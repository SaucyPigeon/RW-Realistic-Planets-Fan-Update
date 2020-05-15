using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
using UnityEngine;

namespace Planets_Code
{
	class Planets_Widgets
	{
		public class SliderParams
		{
			public bool MiddleAlignment = false;
			public string Label = null;
			public string LeftAlignedLabel = null;
			public string RightAlignedLabel = null;
		}

		public static int HorizontalSlider(Rect rect, int value, int leftValue, int rightValue, SliderParams sliderParams = default)
		{
			return Mathf.RoundToInt(Widgets.HorizontalSlider(
				rect,
				value,
				leftValue,
				rightValue,
				sliderParams.MiddleAlignment,
				sliderParams.Label,
				sliderParams.LeftAlignedLabel,
				sliderParams.RightAlignedLabel,
				roundTo: 1.0f
				));
		}
	}
}
