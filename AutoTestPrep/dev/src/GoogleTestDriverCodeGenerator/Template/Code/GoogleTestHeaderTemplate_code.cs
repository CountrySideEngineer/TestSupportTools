using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Target;

namespace CodeGenerator.TestDriver.Template
{
	public partial class GoogleTestHeaderTemplate
	{
		/// <summary>
		/// Target function data.
		/// </summary>
		public Function TargetFunction { get; set; }

		/// <summary>
		/// Default function.
		/// </summary>
		public GoogleTestHeaderTemplate()
		{
			TargetFunction = new Function();
		}

	}
}
