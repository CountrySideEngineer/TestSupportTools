using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Target;
using CodeGenerator.Data;
using System.Diagnostics;

namespace CodeGenerator.Stub.Template
{
	public partial class StubSourceTemplate
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
		public StubSourceTemplate()
		{
			this.ParentFunction = new Function();
			this.TargetFunction = new Function();
			this.Config = new CodeConfiguration();
		}

		/// <summary>
		/// Create stub source body.
		/// </summary>
		/// <param name="target">Target function.</param>
		/// <returns>Stub source body code.</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		protected string CreateStubBody(Function target)
		{
			try
			{
				var template = new StubSourceBodyTemplate()
				{
					ParentFunction = this.ParentFunction,
					TargetFunction = target,
					Config = this.Config
				};
				var code = template.TransformText();
				return code;
			}
			catch (Exception ex)
			when ((ex is ArgumentException) || (ex is ArgumentNullException))
			{
				Debug.WriteLine(ex.StackTrace);

				throw;
			}
		}
	}
}
