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
	/// <summary>
	/// Provides extension methods for the <see cref="ModMethodData"/> class.
	/// </summary>
	public static class ModMethodDataExtensions
	{
		/// <summary>
		/// Returns the full name of the method according to the intermediate
		/// language. In the format "Namespace.Type::Method".
		/// </summary>
		/// <param name="modMethodData">The target mod method.</param>
		/// <returns>The full name of the method.</returns>
		public static string FullMethodName(this ModMethodData modMethodData)
		{
			return String.Join("::", modMethodData.TypeName, modMethodData.MethodName);
		}

		/// <summary>
		/// Returns whether the mod is currently loaded by the game.
		/// </summary>
		/// <param name="modMethodData">The target mod method.</param>
		/// <returns>True if loaded, false otherwise.</returns>
		public static bool ModIsLoaded(this ModMethodData modMethodData)
		{
			return LoadedModManager.RunningMods.Any(x => x.PackageIdPlayerFacing == modMethodData.PackageId);
		}

		/// <summary>
		/// Returns the <see cref="MethodInfo"/> of the target mod method. Use
		/// this to invoke the target method.
		/// </summary>
		/// <param name="modMethodData">The target mod method.</param>
		/// <returns>The target method's MethodInfo.</returns>
		public static MethodInfo GetMethod(this ModMethodData modMethodData)
		{
			if (modMethodData == null)
				throw new ArgumentNullException(nameof(modMethodData));

			var mod = LoadedModManager.RunningMods.FirstOrDefault(x => x.PackageIdPlayerFacing == modMethodData.PackageId);

			if (mod == null)
			{
				throw new ArgumentException($"Tried to get method in mod that is not loaded. Target packageId={modMethodData.PackageId}.");
			}

			Debug.Assert(mod.assemblies.loadedAssemblies.Count > 0);

			foreach (var assembly in mod.assemblies.loadedAssemblies)
			{
				var dialog = assembly.GetType(modMethodData.TypeName);

				if (dialog != null)
				{
					return dialog.GetMethod(modMethodData.MethodName, BindingFlags.Public | BindingFlags.Static);
				}
			}

			Log.Warning($"Realistic Planets - Fan Update was unable to find {FullMethodName(modMethodData)} in mod with packageId={modMethodData.PackageId}. Please ensure that both mods have been updated to their latest versions.");

			return null;
		}

		/// <summary>
		/// Returns the MethodInfo of the target method as long as it is loaded.
		/// </summary>
		/// <param name="modMethodData">The target mod method.</param>
		/// <returns>The target MethodInfo if it is loaded; null otherwise.</returns>
		public static MethodInfo GetMethodIfLoaded(this ModMethodData modMethodData)
		{
			if (ModIsLoaded(modMethodData))
			{
				return GetMethod(modMethodData);
			}
			return null;
		}
	}
}
