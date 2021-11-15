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
	public partial class GoogleTestSourceTemplate
	{
		/// <summary>
		/// Test target function data.
		/// </summary>
		public Function TargetFunction { get; set; }

		/// <summary>
		/// Test data.
		/// </summary>
		public Test Test { get; set; }

		/// <summary>
		/// Test configuration.
		/// </summary>
		public CodeConfiguration Config { get; set; }

		/// <summary>
		/// Create code to initialize stub variables by "SetUp" method.
		/// </summary>
		/// <param name="targetFunction">Test target function data.</param>
		/// <returns>SetUp method code.</returns>
		/// <exception cref="NullReferenceException">Target function or sub functions are NULL.</exception>
		public virtual string CreateSetUpCode(Function targetFunction)
		{
			var template = new GoogleTestSourceSetUpTemplate(targetFunction);
			try
			{
				var setupCode = template.TransformText();
				return setupCode;
			}
			catch (NullReferenceException ex)
			{
				Debug.WriteLine(ex.Message);

				throw;
			}
		}

		/// <summary>
		/// Create code for unit tests.
		/// </summary>
		/// <param name="targetFunction">Test target fucntion data.</param>
		/// <param name="test">Test data</param>
		/// <returns>Code for unit tests.</returns>
		public virtual string CreateTestCaseCode(Function targetFunction, Test test)
		{
			string testCaseCode = string.Empty;
			foreach (var testCase in test.TestCases)
			{
				testCaseCode += this.CreateTestCaseCode(targetFunction, testCase);
			}
			return testCaseCode;
		}

		/// <summary>
		/// Create code for a unit test case
		/// </summary>
		/// <param name="targetFunction">Test target function.</param>
		/// <param name="testCase">Test case data.</param>
		/// <returns>Code for a unit test.</returns>
		public virtual string CreateTestCaseCode(Function targetFunction, TestCase testCase)
		{
			var template = new GoogleTestSourceTestCaseTemplate(targetFunction, testCase);
			var testCaseCode = template.TransformText();
			return testCaseCode;
		}
	}
}
