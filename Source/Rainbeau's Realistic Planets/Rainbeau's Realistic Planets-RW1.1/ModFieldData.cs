using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planets_Code
{
	/// <summary>
	/// Represents data for another mod's field. Use this to "piggyback" off 
	/// another mod's code for compatibility. See <see cref="ModMethodData"/>
	/// for accessing methods instead.
	/// </summary>
	public class ModFieldData
	{
		/// <summary>
		/// The package ID of the mod. Often in the format of Author.ModName.
		/// Refer to mod's about.xml.
		/// </summary>
		public string PackageId { get; }
		/// <summary>
		/// The target type's name. This includes the namespace. Note that nested types use "+" rather than ".".
		/// </summary>
		public string TypeName { get; }
		/// <summary>
		/// The name of the target field. 
		/// </summary>
		public string FieldName { get; }

		public ModFieldData(string packageId, string typeName, string fieldName)
		{
			PackageId = packageId ?? throw new ArgumentNullException(nameof(packageId));
			TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
			FieldName = fieldName ?? throw new ArgumentNullException(nameof(fieldName));
		}
	}
}
