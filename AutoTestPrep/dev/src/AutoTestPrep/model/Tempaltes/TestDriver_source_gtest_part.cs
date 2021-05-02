using AutoTestPrep.Model.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Tempaltes
{
	partial class TestDriver_source_gtest
	{
		/// <summary>
		/// Test case datas
		/// </summary>
		public Test Test { get; set; }
		
		/// <summary>
		/// Options for test driver.
		/// </summary>
		public Options Options { get; set; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="test">Test data</param>
		/// <param name="options">Options of test.</param>
		public TestDriver_source_gtest(Test test, Options options)
		{
			this.Test = test;
			this.Options = options;
		}
	}
}
