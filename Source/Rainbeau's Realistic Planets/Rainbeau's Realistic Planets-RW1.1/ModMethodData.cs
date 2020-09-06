using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Verse;

namespace Planets_Code
{
	public class ModMethodData
	{
		public string PackageId { get; }
		public string TypeName { get; }
		public string MethodName { get; }

		private string FullMethodName
		{
			get
			{
				return String.Join("::", this.TypeName, this.MethodName);
			}
		}

		public bool ModIsLoaded()
		{
			return LoadedModManager.RunningMods.Any(x => x.PackageIdPlayerFacing == this.PackageId);
		}

		private MethodInfo GetMethod()
		{
			var mod = LoadedModManager.RunningMods.FirstOrDefault(x => x.PackageIdPlayerFacing == this.PackageId);

			if (mod == null)
			{
				throw new ArgumentException($"Tried to get method in mod that is not loaded. Target packageId={this.PackageId}.");
			}

			Debug.Assert(mod.assemblies.loadedAssemblies.Count > 0);

			foreach (var assembly in mod.assemblies.loadedAssemblies)
			{
				var dialog = assembly.GetType(this.TypeName);

				if (dialog != null)
				{
					return dialog.GetMethod(this.MethodName, BindingFlags.Public | BindingFlags.Static);
				}
			}

			Log.Warning($"Realistic Planets - Fan Update was unable to find {this.FullMethodName} in mod with packageId={this.PackageId}. Please ensure that both mods have been updated to their latest versions.");

			return null;
		}

		public MethodInfo GetMethodIfLoaded()
		{
			if (this.ModIsLoaded())
			{
				return this.GetMethod();
			}
			return null;
		}

		public ModMethodData(string packageId, string typeName, string methodName)
		{
			Guard.OnArgumentNull(packageId, nameof(packageId));
			Guard.OnArgumentNull(typeName, nameof(typeName));
			Guard.OnArgumentNull(methodName, nameof(methodName));

			PackageId = packageId;
			TypeName = typeName;
			MethodName = methodName;
		}
	}
}
