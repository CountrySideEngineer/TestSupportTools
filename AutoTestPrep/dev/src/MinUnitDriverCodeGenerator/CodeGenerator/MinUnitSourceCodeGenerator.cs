using CodeGenerator.Data;
using CodeGenerator.TestDriver.Template;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.TestDriver.MinUnit
{
	public class MinUnitSourceCodeGenerator : MinUnitCodeGenerator
	{
		/// <summary>
		/// Stub header file the test driver file should include.
		/// </summary>
		public string StubHeaderFileName { get; set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public MinUnitSourceCodeGenerator() : base()
		{
			StubHeaderFileName = string.Empty;
		}

		/// <summary>
		/// Create template for test driver source code using min_unit test framework.
		/// </summary>
		/// <param name="writeData">Test data.</param>
		/// <returns>Template for test driver source code using min_unit test framework.</returns>
		protected override MinUnitTemplate CreateTemplate(WriteData writeData)
		{
			Debug.Assert(null != writeData.Test, $"{nameof(MinUnitMainSourceCodeGenerator)}.{nameof(CreateTemplate)}.{nameof(writeData)}");
			Debug.Assert(null != writeData.Test.Target, $"{nameof(MinUnitMainSourceCodeGenerator)}.{nameof(CreateTemplate)}.{nameof(writeData)}.{nameof(writeData.Test)}");

			INFO("Generate test driver code template of min_unit test framework.");

			MinUnitTemplate template = new MinUnitSourceTemplate()
			{
				TargetFunction = writeData.Test.Target,
				Test = writeData.Test,
				Config = writeData.CodeConfig,
				StubHeaderFileName = StubHeaderFileName,
			};
			return template;
		}
	}
}
