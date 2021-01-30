using System;

namespace Planets_Code
{
	/// <summary>
	/// Represents data for another mod's method. Use this to "piggyback" off
	/// another mod's code for compatibility. See <see cref="ModFieldData"/>
	/// for accessing fields instead.
	/// </summary>
	public class ModMethodData
	{
		/// <summary>
		/// The package ID of the mod. Often in the format of Author.ModName.
		/// Refer to mod's about.xml.
		/// </summary>
		public string PackageId { get; }
		/// <summary>
		/// The target type's name. This includes the namespace.
		/// </summary>
		public string TypeName { get; }
		/// <summary>
		/// The name of the target method. 
		/// </summary>
		public string MethodName { get; }

		public ModMethodData(string packageId, string typeName, string methodName)
		{
			PackageId = packageId ?? throw new ArgumentNullException(nameof(packageId));
			TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
			MethodName = methodName ?? throw new ArgumentNullException(nameof(methodName));
		}
	}
}
