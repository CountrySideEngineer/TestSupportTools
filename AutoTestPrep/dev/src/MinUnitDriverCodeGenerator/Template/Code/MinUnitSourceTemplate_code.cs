using CodeGenerator.Data;
using System;
using System.Collections.Generic;
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
			return string.Empty;
		}

		/// <summary>
		/// Create code of the test case.
		/// </summary>
		/// <param name="function">Test target function.</param>
		/// <param name="test">Test data.</param>
		/// <returns>Test case code.</returns>
		public virtual string CreateTestCaseCode(Function function, Test test)
		{
			return string.Empty;
		}

		/// <summary>
		/// Create code to setup 
		/// </summary>
		/// <param name="function">Test target function.</param>
		/// <param name="test">Test data.</param>
		/// <returns>Code to run all test case method.</returns>
		public virtual string CreateCallTestCaseCode(Function function, Test test)
		{
			return string.Empty;
		}
	}
}
