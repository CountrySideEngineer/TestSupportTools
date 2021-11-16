using CodeGenerator.Data;
using CodeGenerator.TestDriver.Template;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.TestDriver.GoogleTest
{
	public class GoogleTestSourceCodeGenerator : GoogleTestCodeGenerator
	{
		/// <summary>
		/// Create template of test code.
		/// </summary>
		/// <param name="writeData">Test data.</param>
		/// <returns>Template for google test driver source file.</returns>
		protected override GoogleTestTemplate CreateTemplate(WriteData writeData)
		{
			Debug.Assert(null != writeData.Test, $"{nameof(GoogleTestSourceCodeGenerator)}.{nameof(CreateTemplate)}.writeData.Test");
			Debug.Assert(null != writeData.Test.Target, $"{nameof(GoogleTestSourceCodeGenerator)}.{nameof(CreateTemplate)}.writeData.Test.Target");
			Debug.Assert(null != writeData.CodeConfig, $"{nameof(GoogleTestSourceCodeGenerator)}.{nameof(CreateTemplate)}.writeData.CodeConfig");

			var template = new GoogleTestSourceTemplate()
			{
				TargetFunction = writeData.Test.Target,
				Test = writeData.Test,
				Config = writeData.CodeConfig
			};
			return template;
		}
	}
}
