using AutoTestPrep.Model.InputInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Tempaltes.Driver.gtest
{
	public partial class TestDriverTemplate_gtest_Header : TestDriverTemplate_gtest_Base
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		protected TestDriverTemplate_gtest_Header() : base() { }

		/// <summary>
		/// Constructor witha argument.
		/// </summary>
		/// <param name="test">Test test data.</param>
		/// <param name="testDataInfo">Test informations</param>
		public TestDriverTemplate_gtest_Header(Test test, TestDataInfo testDataInfo)
			: base(test, testDataInfo)
		{ }
	}
}
