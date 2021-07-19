using AutoTestPrep.Model.InputInfos;
using AutoTestPrep.Model.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Tempaltes
{
	partial class CFunctionStubTemplate_Header
	{
		/// <summary>
		/// Functoin information.
		/// </summary>
		public Function Function { get; set; }

		/// <summary>
		/// Options for test stub.
		/// </summary>
		public Options Options { get; set; }

		/// <summary>
		/// Test data information for test stub.
		/// </summary>
		public TestDataInfo TestDataInfo { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="function">Function data.</param>
		public CFunctionStubTemplate_Header(Function function)
		{
			this.Function = function;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="function">Function data.</param>
		public CFunctionStubTemplate_Header(Function function, Options options)
		{
			this.Function = function;
			this.Options = options;
		}

		public CFunctionStubTemplate_Header(Function function, TestDataInfo tsetDataInfo)
		{
			this.Function = function;
			this.TestDataInfo = TestDataInfo;
		}
	}
}
