using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planets_Code
{
	class Planets_Log
	{
		const string logNamespace = "Realistic Planets - Fan Update";

		public static void Message(string value)
		{
			Guard.OnArgumentNull(value, nameof(value));
			if (Controller.Settings == null)
			{
				throw new InvalidOperationException($"Cannot call this method without settings initialized.");
			}

			if (Controller.Settings.enableDebugLogging.CurrentValue.Equals(true))
			{
				var sb = new StringBuilder();
				sb.Append("[");
				sb.Append(logNamespace);
				sb.Append("]");
				sb.Append(" ");
				sb.Append(value);
				Verse.Log.Message(sb.ToString());
			}
		}

		private Planets_Log()
		{
		}
	}
}
