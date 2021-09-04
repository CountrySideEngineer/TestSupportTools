using AutoTestPrep.Model.InputInfos;
using AutoTestPrep.Model.Tempaltes.Driver.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Tempaltes.Driver.min_unit
{
	public partial class TestDriverTemplate_min_unit_Base : TestDriverTemplateCommonBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		protected TestDriverTemplate_min_unit_Base() : base() { }

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="test">Test data.</param>
		/// <param name="testDataInfo">Test data information.</param>
		public TestDriverTemplate_min_unit_Base(Test test, TestDataInfo testDataInfo)
			: base(test, testDataInfo)
		{
		}
	}
}
