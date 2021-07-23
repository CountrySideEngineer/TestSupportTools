using AutoTestPrep.Model.InputInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Tempaltes.Driver.gtest
{
	public partial class TestDriverTemplate_gtest_Source
	{
		/// <summary>
		/// Default constructro.
		/// </summary>
		protected TestDriverTemplate_gtest_Source() :base() { }

		/// <summary>
		/// Construtor with argument.
		/// </summary>
		/// <param name="test">Test data </param>
		/// <param name="testDataInfo"></param>
		public TestDriverTemplate_gtest_Source(Test test, TestDataInfo testDataInfo)
			: base (test, testDataInfo) { }
	}
}
