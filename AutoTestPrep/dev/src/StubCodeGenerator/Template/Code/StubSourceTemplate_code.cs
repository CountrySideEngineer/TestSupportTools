using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Target;
using CodeGenerator.Data;

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

		protected string CreateStubBody(Function target)
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
	}
}
