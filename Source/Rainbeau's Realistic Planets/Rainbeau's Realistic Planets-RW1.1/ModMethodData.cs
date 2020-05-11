using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using System.Reflection;
using System.Diagnostics;

namespace Planets_Code
{
	public class ModMethodData
	{
		public string PackageId { get; }
		public string TypeName { get; }
		public string MethodName { get; }

		public string FullMethodName()
		{
			return String.Concat(TypeName, "::", MethodName);
		}

		public bool IsLoaded()
		{
			return LoadedModManager.RunningMods.Any(x => x.PackageIdPlayerFacing == this.PackageId);
		}

		public MethodInfo GetMethod()
		{
			var mod = LoadedModManager.RunningMods.FirstOrDefault(x => x.PackageIdPlayerFacing == this.PackageId);

			if (mod == null)
			{
				throw new ArgumentException($"Tried to get method in mod that is not loaded. Target packageId={this.PackageId}.");
			}

			Debug.Assert(mod.assemblies.loadedAssemblies.Count > 0);

			foreach (var assembly in mod.assemblies.loadedAssemblies)
			{
				var dialog = assembly.GetType(TypeName);

				if (dialog != null)
				{
					return dialog.GetMethod(MethodName, BindingFlags.Public | BindingFlags.Static);
				}
			}

			Log.Warning($"Realistic Planets - Fan Update was unable to find {FullMethodName()} in mod with packageId={PackageId}. Please ensure that both mods have been updated to their latest versions.");

			return null;
		}

		public MethodInfo GetMethodIfLoaded()
		{
			if (IsLoaded())
			{
				return GetMethod();
			}
			return null;
		}

		public ModMethodData(string packageId, string typeName, string methodName)
		{
			PackageId = packageId ?? throw new ArgumentNullException(nameof(packageId));
			TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
			MethodName = methodName ?? throw new ArgumentNullException(nameof(methodName));
		}
	}
}
