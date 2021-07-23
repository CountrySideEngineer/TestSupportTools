using AutoTestPrep.Model.InputInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Tempaltes.Driver.gtest
{
	public partial class TestDriverTemplate_gtest_Base
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		protected TestDriverTemplate_gtest_Base()
		{
			this.Test = new Test();
			this.TestDataInfo = new TestDataInfo();
		}

		/// <summary>
		/// Constructor witha argument.
		/// </summary>
		/// <param name="test">Test data.</param>
		/// <param name="testDataInfo">Test informations</param>
		public TestDriverTemplate_gtest_Base(Test test, TestDataInfo testDataInfo)
		{
			this.Test = test;
			this.TestDataInfo = testDataInfo;
		}
	}
}
