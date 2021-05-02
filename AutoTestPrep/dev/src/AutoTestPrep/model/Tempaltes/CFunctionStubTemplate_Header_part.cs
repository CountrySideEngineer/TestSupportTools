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
		/// Constructor
		/// </summary>
		/// <param name="function">Function data.</param>
		public CFunctionStubTemplate_Header(Function function)
		{
			this.Function = function;
		}
	}
}
