using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoTestPrep.Model;

namespace AutoTestPrep.Model.Tempaltes
{
	partial class CFunctionStubTemplate
	{
		/// <summary>
		/// Parameter object to 
		/// </summary>
		protected Function Function { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="function">Target function data.</param>
		public CFunctionStubTemplate(Function function)
		{
			this.Function = function;
		}
	}
}
