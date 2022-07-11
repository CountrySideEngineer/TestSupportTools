using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Target;
using CodeGenerator.Data;
using System.Diagnostics;
using CSEngineer.Logger;

namespace CodeGenerator.Stub.Template
{
	public partial class StubHeaderTemplate
	{
		/// <summary>
		/// Function data the (stub) function will call.
		/// </summary>
		public Function ParentFunction { get; set; }

		/// <summary>
		/// Base function of the stub.
		/// </summary>
		public Function TargetFunction { get; set; }

		/// <summary>
		/// Configuration about stub.
		/// </summary>
		public CodeConfiguration Config { get; set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public StubHeaderTemplate()
		{
			this.ParentFunction = new Function();
			this.TargetFunction = new Function();
			this.Config = new CodeConfiguration();
		}

		/// <summary>
		/// Create body of stub header code,
		/// </summary>
		/// <param name="parent">Parent function.</param>
		/// <param name="target">Target function.</param>
		/// <returns>Code of body of stub header file.</returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="NullReferenceException"></exception>
		protected string CreateStubBody(Function parent, Function target)
		{
			try
			{
				var template = new StubHeaderBodyTemplate()
				{
					ParentFunction = parent,
					TargetFunction = target
				};
				var code = template.TransformText();
				return code;
			}
			catch (Exception ex)
			when ((ex is ArgumentException) || (ex is ArgumentNullException) || (ex is NullReferenceException))
			{
				string message = "An error occurred while creating stub function header code.";
				Log.GetInstance().ERROR(message);

				throw ex;
			}
		}
	}
}
