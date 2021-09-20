using AutoTestPrep.Model.InputInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Tempaltes.Stub
{
	public partial class TestStubTemplate_Source
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		protected TestStubTemplate_Source() : base() { }

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="function">Parent function information.</param>
		/// <param name="subFunction">Sub functoin information.</param>
		/// <param name="testDataInfo"></param>
		public TestStubTemplate_Source(Function function, Function subFunction, TestDataInfo testDataInfo)
			: base(function, subFunction, testDataInfo) { }

		/// <summary>
		/// Create code to declare buffer to store the count the stub is called.
		/// </summary>
		/// <returns>Code to declare buffer to stre the count the stub is called.</returns>
		protected override string CreateFunctionCalledCountBufferDecalre()
		{
			string calledCountBufferName = string.Empty;
			calledCountBufferName = base.CreateFunctionCalledCountBufferDecalre();
			calledCountBufferName = $"{calledCountBufferName};";
			return calledCountBufferName;
		}
	}
}
