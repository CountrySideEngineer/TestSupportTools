using CodeGenerator.Data;
using CodeGenerator.TestDriver.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.TestDriver.GoogleTest
{
	public class GoogleTestHeaderCodeGenerator : GoogleTestCodeGenerator
	{
		/// <summary>
		/// Create template of test code.
		/// </summary>
		/// <param name="writeData">Test data.</param>
		/// <returns>Template for google test header file.</returns>
		protected override GoogleTestTemplate CreateTemplate(WriteData writeData)
		{
			var template = new GoogleTestHeaderTemplate()
			{
				TargetFunction = writeData.Test.Target
			};
			return template;
		}
	}
}
