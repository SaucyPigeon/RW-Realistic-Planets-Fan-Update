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
		[Conditional("DEBUG")]
		public static void Message(string text, bool ignoreStopLoggingLimit = false)
		{
			Log.Message(text, ignoreStopLoggingLimit);
		}

		[Conditional("DEBUG")]
		public static void Warning(string text, bool ignoreStopLoggingLimit = false)
		{
			Log.Warning(text, ignoreStopLoggingLimit);
		}

		[Conditional("DEBUG")]
		public static void Error(string text, bool ignoreStopLoggingLimit = false)
		{
			Log.Error(text, ignoreStopLoggingLimit);
		}

		DebugLogger()
		{

		}
	}
}
