using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGenerator.Data;
using TestParser.Data;
using TestParser.Target;

namespace CodeGenerator.TestDriver.Template
{
	public partial class MinUnitSourceSetUpTemplate
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
		/// Code configuration.
		/// </summary>
		public CodeConfiguration Config { get; set; }

		/// <summary>
		/// Create method name to initialize stub.
		/// </summary>
		/// <param name="function">Stub function data.</param>
		/// <returns>Initialize method name.</returns>
		public virtual string CreateStubInitializeMethodName(Function function)
		{
			string methodName = string.Empty;
			methodName = $"{function.Name}_init";
			return methodName;
		}
	}
}
