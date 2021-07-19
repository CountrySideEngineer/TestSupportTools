using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Tempaltes
{
	partial class TestDriverTemplate_Source_gtest_TestCase
	{
		/// <summary>
		/// Test data
		/// </summary>
		public Test Test { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="test"></param>
		public TestDriverTemplate_Source_gtest_TestCase(Test test)
		{
			this.Test = test;
		}

		/// <summary>
		/// Declare test variables.
		/// </summary>
		public string TestDeclare
		{
			get
			{
				var template = new TestDriverTemplate_Source_gtest_TestCase_DeclareInput(this.Test);
				var content = template.TransformText();
				return content;
			}
		}

		public string TestInput(TestCase testCase)
		{
			var template = new TestDriverTemplate_Source_gtest_TestCase_SetupInput(testCase);
			var content = template.TransformText();
			return content;
		}

		public string FunctionCall
		{
			get
			{
				var template = new TestDriverTemplate_Source_FunctionCall(Test);
				var content = template.TransformText();
				return content;
			}
		}

		public string CheckOutput(TestCase testCase)
		{
			var template = new TestDriverTemplate_Source_gtest_TestCase_CheckOutput(testCase);
			var content = template.TransformText();
			return content;
		}
	}
}
