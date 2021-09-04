using AutoTestPrep.Model.InputInfos;
using AutoTestPrep.Model.Tempaltes.Driver.min_unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Tempaltes.Driver.mid_unit
{
	public partial class TestDriverTemplate_mid_unit_main_Source : TestDriverTemplate_min_unit_main_Source
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		protected TestDriverTemplate_mid_unit_main_Source() : base() { }

		/// <summary>
		/// Constructor with argument about test.
		/// </summary>
		/// <param name="test">Test data</param>
		/// <param name="testDataInfo">Test data information.</param>
		public TestDriverTemplate_mid_unit_main_Source(Test test, TestDataInfo testDataInfo)
			: base(test, testDataInfo)
		{ }

		/// <summary>
		/// Constructor with argument about test.
		/// </summary>
		/// <param name="tests">Collection of test.</param>
		/// <param name="testDataInfo">Test data information.</param>
		public TestDriverTemplate_mid_unit_main_Source(IEnumerable<Test> tests, TestDataInfo testDataInfo)
			: base(tests, testDataInfo) 
		{ }
	}
}
