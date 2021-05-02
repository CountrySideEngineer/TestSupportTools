using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Tempaltes
{
	partial class TestDriverTemplate_Header_gtest
	{
		/// <summary>
		/// Test case data.
		/// </summary>
		public Test Test { get; set; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="test">Test data</param>
		public TestDriverTemplate_Header_gtest(Test test)
		{
			this.Test = test;
		}
	}
}
