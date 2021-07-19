using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoTestPrep.Model;
using AutoTestPrep.Model.InputInfos;
using AutoTestPrep.Model.Option;

namespace AutoTestPrep.Model.Tempaltes
{
	partial class CFunctionStubTemplate
	{
		/// <summary>
		/// Parameter object to 
		/// </summary>
		public Function Function { get; set; }

		/// <summary>
		/// Options for test stub.
		/// </summary>
		public Options Options { get; set; }

		/// <summary>
		/// Test data information for test stub.
		/// </summary>
		public TestDataInfo TestDataInfo;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="function">Target function data.</param>
		public CFunctionStubTemplate(Function function)
		{
			this.Function = function;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="function">Function data.</param>
		/// <param name="testDataInfo">Test information data for test stub data.</param>
		public CFunctionStubTemplate(Function function, TestDataInfo testDataInfo)
		{
			this.Function = function;
			this.TestDataInfo = testDataInfo;
		}
	}
}
