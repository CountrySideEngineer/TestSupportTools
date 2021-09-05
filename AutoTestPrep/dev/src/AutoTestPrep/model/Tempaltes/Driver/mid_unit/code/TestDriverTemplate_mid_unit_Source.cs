using AutoTestPrep.Model.InputInfos;
using AutoTestPrep.Model.Tempaltes.Driver.min_unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Tempaltes.Driver.mid_unit
{
	public partial class TestDriverTemplate_mid_unit_Source : TestDriverTemplate_min_unit_Source
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		protected TestDriverTemplate_mid_unit_Source() : base() { }

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="test"></param>
		/// <param name="testDataInfo"></param>
		public TestDriverTemplate_mid_unit_Source(Test test, TestDataInfo testDataInfo) 
			: base(test, testDataInfo) 
		{ }

		///	<summary>
		///	Create code to check output value whether it equals to expect.
		///	</summary>
		///	<return>Code to check output value whether it equals to expect.</return>
		/// <remarks>Declare as an abstract method in base class.</remarks>
		protected override string CreateCodeToCheckOutputAndExpect(TestData expect)
		{
			string nameOfExpect = string.Empty;
			if (("return").Equals(expect.Name))
			{
				nameOfExpect = "returnValue";
			}
			else
			{
				nameOfExpect = $"{expect.Value}";
			}

			string codeToCheckExpect = string.Empty;
			codeToCheckExpect = $"mu_assert(\"{nameOfExpect}\", {nameOfExpect}, {expect.Value})";
			return codeToCheckExpect;
		}

		/// <summary>
		/// Create code to call method to run test using min_unit test framework method.
		/// </summary>
		/// <param name="testCase">Test case data to test.</param>
		/// <returns>Code to call method to run test.</returns>
		protected override string CreateCodeToCallTestRunMethod(TestCase testCase)
		{
			string codeToCallTestRunMethod = string.Empty;
			codeToCallTestRunMethod = $"mu_run_test(" +
				$"\"{this.CreateTestCaseMethodName(testCase)}\", " +
				$"{this.CreateTestCaseMethodName(testCase)})";
			return codeToCallTestRunMethod;
		}
	}
}
