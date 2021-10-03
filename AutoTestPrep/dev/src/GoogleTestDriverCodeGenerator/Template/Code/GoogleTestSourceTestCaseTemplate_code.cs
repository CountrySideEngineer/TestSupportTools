using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Data;
using TestParser.Target;

namespace CodeGenerator.TestDriver.Template
{
	public partial class GoogleTestSourceTestCaseTemplate
	{
		/// <summary>
		/// Target test function data.
		/// </summary>
		public Function TargetFunction { get; set; }

		/// <summary>
		/// Test case number.
		/// </summary>
		public int TestCaseNumber { get; set; }

		/// <summary>
		/// Test case data for a test.
		/// </summary>
		public TestCase TestCase { get; set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		protected GoogleTestSourceTestCaseTemplate()
		{
			this.TestCaseNumber = 0;
			this.TargetFunction = new Function();
		}

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="testCaseNumber">Test case number.</param>
		/// <param name="targetFunction">Target test function data.</param>
		public GoogleTestSourceTestCaseTemplate(int testCaseNumber, Function targetFunction)
		{
			this.TestCaseNumber = TestCaseNumber;
			this.TargetFunction = targetFunction;
		}

		/// <summary>
		/// Create code to setup test input data.
		/// </summary>
		/// <param name="testData"></param>
		/// <returns></returns>
		protected virtual string CreateInput(TestData testData)
		{
			string inputCode = string.Empty;

			inputCode = $"{testData.Name} = {testData.Value}";
			return inputCode;
		}

		/// <summary>
		/// Create code to call target function.
		/// </summary>
		/// <param name="targetFunctoin">Target functoin data.</param>
		/// <returns>Code to call target function.</returns>
		protected virtual string CreateTargetFunctionCall(Function targetFunctoin)
		{
			string targetFunctionCall = string.Empty;

			if (!("void".Equals(targetFunctoin.DataType.ToLower())))
			{
				targetFunctionCall = $"{targetFunctoin.ActualDataType()} returnValue = ";
			}
			targetFunctionCall += $"{targetFunctoin.Name}(";
			bool isTop = true;
			foreach (var argument in targetFunctoin.Arguments)
			{
				if (!isTop)
				{
					targetFunctionCall += ", ";
				}
				targetFunctionCall += $"{argument.Name}";
				isTop = false;
			}
			targetFunctionCall += ")";

			return targetFunctionCall;
		}
	}
}
