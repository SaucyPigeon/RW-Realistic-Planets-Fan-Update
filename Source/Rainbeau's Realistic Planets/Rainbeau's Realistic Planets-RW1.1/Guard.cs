using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planets_Code
{
	class Guard
	{
		public static void OnArgumentNull(object value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
		}

		public static void OnArgumentNull<T>(T? value, string name)
			where T : struct
		{
			if (!value.HasValue)
			{
				throw new ArgumentException("Nullable object must have a value.", name);
			}
		}

		private Guard()
		{

		}
	}
}
