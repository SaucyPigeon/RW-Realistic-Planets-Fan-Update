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
		int id;

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
				"id=",
				id,
				", value=",
				(float)this,
				")"
				);
		}

		public void ExposeData()
		{
			Scribe_Values.Look(ref id, "id", 0, true);
		}

		public int CompareTo(HillinessModifier other)
		{
			return id.CompareTo(other.id);
		}

		public static implicit operator int(HillinessModifier value) => value.id;
		public static implicit operator HillinessModifier(int value) => new HillinessModifier(value, recordValue: false);

		public static explicit operator float(HillinessModifier value)
		{
			return 1.0f + (value.id * 0.1f);
		}

		HillinessModifier(int id, bool recordValue = true)
		{
			this.id = id;
			
			if (recordValue)
			{
				if (Values == null)
				{
					Values = new List<int>();
				}
				Values.Add(id);
			}
		}
	}
}
