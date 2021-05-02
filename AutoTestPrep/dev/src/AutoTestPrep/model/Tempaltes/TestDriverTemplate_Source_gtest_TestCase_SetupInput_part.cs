using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Tempaltes
{
	partial class TestDriverTemplate_Source_gtest_TestCase_SetupInput
	{
		/// <summary>
		/// Test data.
		/// </summary>
		public Test Test { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="test">Test data.</param>
		public TestDriverTemplate_Source_gtest_TestCase_SetupInput(Test test)
		{
			this.Test = test;
		}
	}
}
