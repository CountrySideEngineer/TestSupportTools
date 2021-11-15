using CodeGenerator.Data;
using CodeGenerator.TestDriver.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.TestDriver.MinUnit
{
	public abstract class MinUnitCodeGenerator : ICodeGenerator
	{
		/// <summary>
		/// Generate test driver code using min_unit test framework.
		/// </summary>
		/// <param name="data">Data for code.</param>
		/// <returns>Generated test driver code.</returns>
		public string Generate(WriteData data)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Abstract method to create template.
		/// </summary>
		/// <returns>StubTemplate class.</returns>
		protected abstract MinUnitTemplate CreateTemplate(WriteData writeData);
	}
}
