using CodeGenerator.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Data;
using TestParser.Target;

namespace CodeGenerator.TestDriver.Template
{
	public partial class MinUnitSourceTemplate
	{
		/// <summary>
		/// Test target function data.
		/// </summary>
		public Function TargetFunction { get; set; }

		/// <summary>
		/// Test data/
		/// </summary>
		public Test Test { get; set; }

		/// <summary>
		/// Code configuration.
		/// </summary>
		public CodeConfiguration Config { get; set; }

		/// <summary>
		/// Create code to setup test.
		/// </summary>
		/// <param name="function">Test target function.</param>
		/// <returns>Setup test code.</returns>
		public virtual string CreateSetUpCode(Function function)
		{
			var template = new MinUnitSourceSetUpTemplate(function);

			try
			{
				var setupCode = template.TransformText();
				return setupCode;
			}
			catch (NullReferenceException ex)
			{
				Debug.WriteLine(ex.StackTrace);

				throw;
			}
		}

		/// <summary>
		/// Create code of the test case.
		/// </summary>
		/// <param name="function">Test target function.</param>
		/// <param name="test">Test data.</param>
		/// <returns>Test case code.</returns>
		public virtual string CreateTestCaseCode(Function function, Test test)
		{
			string testCaseCode = string.Empty;
			foreach (var testCase in test.TestCases)
			{
				testCaseCode += this.CreateTestCaseCode(function, testCase);
			}
			return testCaseCode;
		}

		/// <summary>
		/// Create code of the test case code.
		/// </summary>
		/// <param name="function">Test target function.</param>
		/// <param name="testCase">Test case.</param>
		/// <returns></returns>
		public virtual string CreateTestCaseCode(Function function, TestCase testCase)
		{
			var template = new MinUnitSourceTestCaseTemplate(function, testCase);
			var testCode = template.TransformText();
			return testCode;
		}

		/// <summary>
		/// Create code to setup 
		/// </summary>
		/// <param name="function">Test target function.</param>
		/// <param name="test">Test data.</param>
		/// <returns>Code to run all test case method.</returns>
		public virtual string CreateCallTestCaseCode(Function function, Test test)
		{
			var template = new MinUnitSourceCallTestCaseTemplate(function, test);
			var testCode = template.TransformText();
			return testCode;
		}
	}
}
