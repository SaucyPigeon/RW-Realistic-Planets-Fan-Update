using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planets_Code
{
	public class SettingsValue<T> : IEquatable<T>
	{
		public T CurrentValue;
		public T DefaultValue;
		public string Name;

		public void Reset()
		{
			this.CurrentValue = DefaultValue;
		}

		public static SettingsValue<T> Create(string name, T value)
		{
			return new SettingsValue<T>(value, name);
		}

		private SettingsValue(T currentValue, T defaultValue, string name)
		{
			DebugLogger.Message($"Creating setting value: name={name}, value={currentValue}");

			CurrentValue = currentValue;
			DefaultValue = defaultValue;
			Name = name ?? throw new ArgumentNullException(nameof(name));
		}

		public SettingsValue(T defaultValue, string name)
			: this(defaultValue, defaultValue, name)
		{

		}

		public static implicit operator T(SettingsValue<T> value) => value.CurrentValue;

		public override string ToString()
		{
			return String.Concat("Setting: name=", Name, ", current=", CurrentValue.ToString(), ", default=", DefaultValue.ToString());
		}

		public bool Equals(T other)
		{
			return CurrentValue.Equals(other);
		}
	}
}
