using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace Planets_Code
{
	public sealed class HillinessModifier : IExposable, IComparable<HillinessModifier>
	{
		int value;

		public string Label
		{
			get
			{
				return "Planets.Hilliness_" + GetLabelSuffix();
			}
		}

		public static readonly HillinessModifier VeryFlat		= new HillinessModifier(-3);
		public static readonly HillinessModifier Flat			= new HillinessModifier(-2);
		public static readonly HillinessModifier SlightlyFlat	= new HillinessModifier(-1);
		public static readonly HillinessModifier Normal			= new HillinessModifier(0);
		public static readonly HillinessModifier SlightlyHilly	= new HillinessModifier(+1);
		public static readonly HillinessModifier Hilly			= new HillinessModifier(+2);
		public static readonly HillinessModifier VeryHilly		= new HillinessModifier(+3);

		public static IList<int> Values { get; private set; }

		public override string ToString()
		{
			return String.Concat(
				"HillinessModifier(",
				"value=",
				value,
				", as float=",
				(float)this,
				")"
				);
		}

		public void ExposeData()
		{
			Scribe_Values.Look(ref value, "value", 0, true);
		}

		public int CompareTo(HillinessModifier other)
		{
			return value.CompareTo(other.value);
		}

		private string GetLabelSuffix()
		{
			switch (value)
			{
				case -3:
					return "VeryLow";
				case -2:
					return "Low";
				case -1:
					return "SlightlyLow";
				case 0:
					return "Normal";
				case +1:
					return "SlightlyHigh";
				case +2:
					return "High";
				case +3:
					return "VeryHigh";
				default:
					throw new ArgumentOutOfRangeException(nameof(value), $"Value of {nameof(value)} ({value}) does not have a defined label suffix.");
			}
		}

		public static implicit operator int(HillinessModifier value) => value.value;
		public static implicit operator HillinessModifier(int value) => new HillinessModifier(value, recordValue: false);

		public static explicit operator float(HillinessModifier value)
		{
			return 1.0f + (value.value * 0.1f);
		}

		HillinessModifier(int value, bool recordValue = true)
		{
			this.value = value;
			
			if (recordValue)
			{
				if (Values == null)
				{
					Values = new List<int>();
				}
				Values.Add(value);
			}
		}
	}
}
