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

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append("{");
			sb.Append("name=");
			sb.Append(this.Name);
			sb.Append(", ");
			sb.Append("currentValue=");
			sb.Append(this.CurrentValue.ToString());
			sb.Append(", ");
			sb.Append("defaultValue=");
			sb.Append(this.DefaultValue.ToString());
			sb.Append("}");
			return sb.ToString();
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
