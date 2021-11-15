using CodeGenerator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
	/// <summary>
	/// Interface to generate code base on <para>WriteData</para> object.
	/// </summary>
	public interface ICodeGenerator
	{
		/// <summary>
		/// Generate code
		/// </summary>
		/// <param name="data">Data for code.</param>
		/// <returns>Generated code.</returns>
		string Generate(WriteData data);
	}
}
