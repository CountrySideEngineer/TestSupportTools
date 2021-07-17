using AutoTestPrep.Model.InputInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Tempaltes.Stub
{
	public partial class TestStubTemplate_Base
	{
		/// <summary>
		/// Function information.
		/// </summary>
		//public Function Function { get; set; }

		/// <summary>
		/// Sub function information
		/// </summary>
		//public Function SubFunction { get; set; }

		/// <summary>
		/// Test data information.
		/// </summary>
		//public TestDataInfo TestDataInfo { get; set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		protected TestStubTemplate_Base()
		{
			this.ParentFunction = new Function();
			this.TestDataInfo = new TestDataInfo();
		}

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="function">Function information.</param>
		/// <param name="testDataInfo">Test data information.</param>
		public TestStubTemplate_Base(Function function, TestDataInfo testDataInfo)
		{
			this.ParentFunction = function;
			this.TestDataInfo = testDataInfo;
		}

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="function">Funtion information (Parent function information).</param>
		/// <param name="subFunction">Sub function information.</param>
		/// <param name="testDataInfo">Test data information.</param>
		public TestStubTemplate_Base(Function function, Function subFunction, TestDataInfo testDataInfo)
		{
			this.ParentFunction = function;
			this.SubFunction = subFunction;
			this.TestDataInfo = testDataInfo;
		}

		/// <summary>
		/// Create code declaring variable to set the count the function called.
		/// </summary>
		/// <param name="function">Function information</param>
		/// <returns>Code to declare variable to set the count function called.</returns>
		protected virtual string CreateFunctionCalledCountBufferDecalre()
		{
			string calledCountBufferName = ("int " + this.SubFunction.Name);
			calledCountBufferName += "_called_count";
			return calledCountBufferName;
		}
	}
}
