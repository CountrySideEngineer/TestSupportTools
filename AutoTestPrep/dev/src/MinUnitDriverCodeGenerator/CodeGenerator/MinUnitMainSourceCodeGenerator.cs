using CodeGenerator.Data;
using CodeGenerator.TestDriver.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Target;

namespace CodeGenerator.TestDriver.MinUnit.CodeGenerator
{
	public class MinUnitMainSourceCodeGenerator : MinUnitCodeGenerator
	{
		/// <summary>
		/// Create template for "main" function of test.
		/// </summary>
		/// <param name="writeData">Test data.</param>
		/// <returns>Template for test driver main function code using min_unit test framework.</returns>
		protected override MinUnitTemplate CreateTemplate(WriteData writeData)
		{
			var template = new MinUnitSourceMainTemplate()
			{
				TargetFunction = writeData.Test.Target
			};
			return template;
		}
	}
}
