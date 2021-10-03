using CodeGenerator.Stub.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Target;

namespace CodeGenerator.TestDriver.Template
{
	public partial class GoogleTestSourceSetUpTemplate
	{
		/// <summary>
		/// Test target function data.
		/// </summary>
		public Function TargetFunction { get; set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		protected GoogleTestSourceSetUpTemplate()
		{
			this.TargetFunction = new Function();
		}

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="targetFunction">Test target function data.</param>
		public GoogleTestSourceSetUpTemplate(Function targetFunction)
		{
			this.TargetFunction = targetFunction;
		}

		/// <summary>
		/// Create name of method to initialize stub.
		/// </summary>
		/// <param name="stubFunction">Stub function data.</param>
		/// <returns>Method name to initialize stub parameter.</returns>
		public virtual string CreateStubInitializeMethodName(Function stubFunction)
		{
			var template = new StubTemplate();
			string stubInitializeCode = template.CreateInitializeFunctionName(stubFunction);
			return stubInitializeCode;
		}


	}
}
