using CodeGenerator.Data;
using CodeGenerator.Stub.Template;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Stub
{
	public class StubSourceGenerator : StubCodeGenerator
	{
		/// <summary>
		/// Stub header file name.
		/// </summary>
		public string StubHeaderFileName { get; set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public StubSourceGenerator() : base()
		{
			StubHeaderFileName = string.Empty;
		}

		/// <summary>
		/// Create template class.
		/// </summary>
		/// <param name="writeData">Write data.</param>
		/// <returns>Template class object to create stub source code.</returns>
		protected override StubTemplate CreateTemplate(WriteData writeData)
		{
			Debug.Assert(null != writeData.Test, $"{nameof(StubSourceGenerator)}.{nameof(CreateTemplate)}.writeData.Test");
			Debug.Assert(null != writeData.Test.Target, $"{nameof(StubSourceGenerator)}.{nameof(CreateTemplate)}.writeData.Test.Target");
			Debug.Assert(null != writeData.CodeConfig, $"{nameof(StubSourceGenerator)}.{nameof(CreateTemplate)}.writeData.CodeConfig");

			INFO("Generate stub source code template.");

			var template = new StubSourceTemplate()
			{
				ParentFunction = writeData.Test.Target,
				Config = writeData.CodeConfig
			};
			return template;
		}
	}
}
