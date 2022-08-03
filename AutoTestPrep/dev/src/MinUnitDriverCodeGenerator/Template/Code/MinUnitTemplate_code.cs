using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Data;
using TestParser.Target;

namespace CodeGenerator.TestDriver.Template
{
	public partial class MinUnitTemplate
	{
		/// <summary>
		/// Create method name to run all test case.
		/// </summary>
		/// <param name="function">Test target function data.</param>
		/// <returns>Method name to run all test case.</returns>
		public virtual string CreateRunAllTestName(Function function)
		{
			string testName = string.Empty;
			testName = $"{function.Name}_run_all";
			return testName;
		}

		/// <summary>
		/// Create test case method name to run a test.
		/// </summary>
		/// <param name="function">Test target function data.</param>
		/// <param name="testCase">Test case data.</param>
		/// <returns>Test case method name.</returns>
		public virtual string CreateTestCaseMethodName(Function function, TestCase testCase)
		{
			string testName = string.Empty;
			testName = $"{function.Name}_utest_{testCase.Id}";
			return testName;
		}

		/// <summary>
		/// Create test setup method name.
		/// </summary>
		/// <param name="function">Test target function data.</param>
		/// <returns>Stub initialize method name.</returns>
		public virtual string CreateStubInitMethodName(Function function)
		{
			string methodName = string.Empty;
			methodName = $"{function.Name}_utest_SetUp";
			return methodName;
		}

		protected virtual string DeclareArgumentVariable(Parameter argument)
		{
			string code = string.Empty;
			if (0 == argument.PointerNum)
			{
				code = argument.DataType;
			}
			else if (1 == argument.PointerNum)
			{
				code = argument.DataType;
			}
			else if (2 == argument.PointerNum)
			{
				code = $"{argument.PointerNum}*";
			}
			else
			{
				throw new ArgumentException();
			}
			return code;
		}
	}
}
