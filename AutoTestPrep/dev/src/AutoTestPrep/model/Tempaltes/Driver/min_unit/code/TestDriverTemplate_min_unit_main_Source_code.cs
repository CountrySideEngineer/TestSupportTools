using AutoTestPrep.Model.InputInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Tempaltes.Driver.min_unit
{
	public partial class TestDriverTemplate_min_unit_main_Source : TestDriverTemplate_min_unit_Source
	{
		protected IEnumerable<Test> Tests;

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="test">Test data.</param>
		/// <param name="testDataInfo">Test data information.</param>
		public TestDriverTemplate_min_unit_main_Source(Test test, TestDataInfo testDataInfo)
			: base(test, testDataInfo)
		{ }

		public TestDriverTemplate_min_unit_main_Source(IEnumerable<Test> tests, TestDataInfo testDataInfo)
			: base(tests.ElementAt(0), testDataInfo)
		{
			this.Tests = tests;
		}

		protected virtual string CreateRunTestMethodEntryPointName(Function targetFunction)
		{
			string runTestMethodEntryPoint = string.Empty;
			runTestMethodEntryPoint = $"run_{targetFunction.Name}_test";

			return runTestMethodEntryPoint;
		}
	}
}
