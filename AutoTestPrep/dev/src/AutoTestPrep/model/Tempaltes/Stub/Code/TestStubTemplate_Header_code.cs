using AutoTestPrep.Model.InputInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Tempaltes.Stub
{
	public partial class TestStubTemplate_Header
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		protected TestStubTemplate_Header() : base() { }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="function">Function information data.</param>
		public TestStubTemplate_Header(Function function, TestDataInfo testDataInfo)
			: base(function, testDataInfo)
		{ }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="function">Function information data (Parent function data).</param>
		/// <param name="subFunction">Sub function information.</param>
		/// <param name="testDataInfo">TestDataInformation</param>
		public TestStubTemplate_Header(Function function, Function subFunction, TestDataInfo testDataInfo)
			:base(function, subFunction, testDataInfo)
		{ }
	}
}
