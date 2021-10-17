using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Target;
using CodeGenerator.Data;

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

		protected string CreateStubBody(Function parent, Function target)
		{
			var template = new StubHeaderBodyTemplate()
			{
				ParentFunction = parent,
				TargetFunction = target
			};
			var code = template.TransformText();
			return code;
		}
	}
}
