using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Data;
using TestParser.Target;

namespace CodeGenerator.TestDriver.Template
{
	public partial class MinUnitSourceCallTestCaseTemplate
	{
		/// <summary>
		/// Test target function data.
		/// </summary>
		public Function TargetFunction { get; set; }

		/// <summary>
		/// Test data/
		/// </summary>
		public Test Test { get; set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		protected MinUnitSourceCallTestCaseTemplate()
		{
			this.TargetFunction = new Function();
			this.Test = new Test();
		}

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="function">Test target function data.</param>
		/// <param name="test">Test data.</param>
		public MinUnitSourceCallTestCaseTemplate(Function function, Test test)
		{
			this.TargetFunction = function;
			this.Test = test;
		}
	}
}
