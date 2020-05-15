using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using RimWorld;
using Verse;

namespace Planets_Code
{
	class DebugLogger
	{
		private const bool ignoreStopLoggingLimit = true;

		[Conditional("DEBUG")]
		public static void Message(string text)
		{
			Log.Message(text, ignoreStopLoggingLimit);
		}

		[Conditional("DEBUG")]
		public static void Warning(string text)
		{
			Log.Warning(text, ignoreStopLoggingLimit);
		}

		[Conditional("DEBUG")]
		public static void Error(string text)
		{
			Log.Error(text, ignoreStopLoggingLimit);
		}

		DebugLogger()
		{
		}
	}
}
