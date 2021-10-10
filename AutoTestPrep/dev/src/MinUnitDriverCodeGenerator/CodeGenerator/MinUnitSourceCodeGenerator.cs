using CodeGenerator.Data;
using CodeGenerator.TestDriver.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.TestDriver.MinUnit
{
	public class MinUnitSourceCodeGenerator : MinUnitCodeGenerator
	{
		/// <summary>
		/// Create template for test driver source code using min_unit test framework.
		/// </summary>
		/// <param name="writeData">Test data.</param>
		/// <returns>Template for test driver source code using min_unit test framework.</returns>
		protected override MinUnitTemplate CreateTemplate(WriteData writeData)
		{
			MinUnitTemplate template = new MinUnitSourceTemplate()
			{
				TargetFunction = writeData.Test.Target,
				Test = writeData.Test,
				Config = writeData.CodeConfig
			};
			return template;
		}
	}
}
