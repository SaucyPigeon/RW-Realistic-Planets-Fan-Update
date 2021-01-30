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
	/// Provides extension methods for the <see cref="ModFieldData"/> class.
	/// </summary>
	public static class ModFieldDataExtensions
	{
		/// <summary>
		/// Returns the full name of the field according to the intermediate
		/// language. In the format "Namespace.Type::Field".
		/// </summary>
		/// <param name="modFieldData">The target mod field.</param>
		/// <returns>The full name of the field.</returns>
		public static string FullFieldName(this ModFieldData modFieldData)
		{
			return String.Join("::", modFieldData.TypeName, modFieldData.FieldName);
		}

		/// <summary>
		/// Returns whether the mod is currently loaded by the game.
		/// </summary>
		/// <param name="modFieldData">The target mod field.</param>
		/// <returns>True if loaded, false otherwise.</returns>
		public static bool ModIsLoaded(this ModFieldData modFieldData)
		{
			return LoadedModManager.RunningMods.Any(x => x.PackageIdPlayerFacing == modFieldData.PackageId);
		}

		/// <summary>
		/// Returns the <see cref="FieldInfo"/> of the target mod field. Use
		/// this to use the target field.
		/// </summary>
		/// <param name="modFieldData">The target mod field.</param>
		/// <returns>The target field's FieldInfo.</returns>
		public static FieldInfo GetField(this ModFieldData modFieldData)
		{
			if (modFieldData == null)
				throw new ArgumentNullException(nameof(modFieldData));

			var mod = LoadedModManager.RunningMods.FirstOrDefault(x => x.PackageIdPlayerFacing == modFieldData.PackageId);

			if (mod == null)
			{
				throw new ArgumentException($"Tried to get field in mod that is not loaded. Target packageId={modFieldData.PackageId}.");
			}

			Debug.Assert(mod.assemblies.loadedAssemblies.Count > 0);

			foreach (var assembly in mod.assemblies.loadedAssemblies)
			{
				var dialog = assembly.GetType(modFieldData.TypeName);

				if (dialog != null)
				{
					return dialog.GetField(modFieldData.FieldName, BindingFlags.Public | BindingFlags.Static);
				}
			}

			Log.Warning($"Realistic Planets - Fan Update was unable to find {FullFieldName(modFieldData)} in mod with packageId={modFieldData.PackageId}. Please ensure that both mods have been updated to their latest versions.");

			return null;
		}

		/// <summary>
		/// Returns the FieldInfo of the target field as long as it is loaded.
		/// </summary>
		/// <param name="modFieldData">The target mod field.</param>
		/// <returns>The target FieldInfo if it is loaded; null otherwise.</returns>
		public static FieldInfo GetFieldIfLoaded(this ModFieldData modFieldData)
		{
			if (ModIsLoaded(modFieldData))
			{
				return GetField(modFieldData);
			}
			return null;
		}
	}
}
