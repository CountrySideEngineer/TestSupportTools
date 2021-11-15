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
	public class GoogleTestHeaderCodeGenerator : GoogleTestCodeGenerator
	{
		/// <summary>
		/// Create template of test code.
		/// </summary>
		/// <param name="writeData">Test data.</param>
		/// <returns>Template for google test header file.</returns>
		/// <exception cref="NullReferenceException"></exception>
		protected override GoogleTestTemplate CreateTemplate(WriteData writeData)
		{
			Debug.Assert(null != writeData.Test, $"{nameof(GoogleTestHeaderCodeGenerator)}.{nameof(CreateTemplate)}.writeData.Test");
			Debug.Assert(null != writeData.Test.Target, $"{nameof(GoogleTestHeaderCodeGenerator)}.{nameof(CreateTemplate)}.writeData.Test.Target");

			var template = new GoogleTestHeaderTemplate()
			{
				TargetFunction = writeData.Test.Target
			};
			return template;
		}
	}
}
