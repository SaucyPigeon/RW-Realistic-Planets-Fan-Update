using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace Planets_Code
{
	public class SettingsValue<T>
	{
		public T CurrentValue;
		public T DefaultValue;
		public string Name;

		const string ModXmlNamespace = "Planets";

		public string Label
		{
			get
			{
				return String.Join(".", ModXmlNamespace, this.Name.CapitalizeFirst());
			}
		}

		public string Tooltip
		{
			get
			{
				return String.Join("", this.Label, "Tip");
			}
		}

		private SettingsValue(T currentValue, T defaultValue, string name)
		{
			CurrentValue = currentValue;
			DefaultValue = defaultValue;
			Name = name ?? throw new ArgumentNullException(nameof(name));
		}

		public SettingsValue(T defaultValue, string name)
			: this(defaultValue, defaultValue, name)
		{

		}
	}
}
