﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Tempaltes
{
	partial class TestDriverTemplate_Source_gtest_TestCase_CheckOutput
	{
		/// <summary>
		/// Test data.
		/// </summary>
		public Test Test { get; set; }

		public TestCase TestCase { get; set; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="test">Test data.</param>
		public TestDriverTemplate_Source_gtest_TestCase_CheckOutput(Test test)
		{
			this.Test = test;
		}

		public TestDriverTemplate_Source_gtest_TestCase_CheckOutput(TestCase testCase)
		{
			this.TestCase = testCase;
		}
	}
}
