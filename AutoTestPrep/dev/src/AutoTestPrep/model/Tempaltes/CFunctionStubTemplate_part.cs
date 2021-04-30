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
		protected Parameter parameter { get; set; }
		public CFunctionStubTemplate(Parameter parameter)
		{
			this.parameter = parameter;
		}
	}
}
