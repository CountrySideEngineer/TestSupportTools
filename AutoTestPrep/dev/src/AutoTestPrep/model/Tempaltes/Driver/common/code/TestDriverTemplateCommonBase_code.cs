using AutoTestPrep.Model.InputInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Tempaltes.Driver.common
{
	public partial class TestDriverTemplateCommonBase
	{
		/// <summary>
		/// Test data for test.
		/// </summary>
		protected Test Test;

		/// <summary>
		/// Target function information to be tested.
		/// </summary>
		protected Function TargetFunction
		{
			get
			{
				return this.Test.Target;
			}
		}

		/// <summary>
		/// Collection of function the test target function will call while test.
		/// </summary>
		protected IEnumerable<Function> SubFunctions
		{
			get
			{
				return this.Test.Target.SubFunctions;
			}
		}

		/// <summary>
		/// Test data information.
		/// </summary>
		protected TestDataInfo TestDataInfo;


		/// <summary>
		/// Default constructor.
		/// </summary>
		protected TestDriverTemplateCommonBase()
		{
			this.Test = new Test();
			this.TestDataInfo = new TestDataInfo();
		}

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="test">Test data.</param>
		/// <param name="testDataInfo">Test data information.</param>
		public TestDriverTemplateCommonBase(Test test, TestDataInfo testDataInfo)
		{
			this.Test = test;
			this.TestDataInfo = testDataInfo;
		}
	}
}
