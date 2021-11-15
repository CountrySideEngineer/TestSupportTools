using CodeGenerator.Data;
using CodeGenerator.Stub.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Stub
{
	public class StubSourceGenerator : StubCodeGenerator
	{
		/// <summary>
		/// Create template class.
		/// </summary>
		/// <param name="writeData">Write data.</param>
		/// <returns>Template class object to create stub source code.</returns>
		protected override StubTemplate CreateTemplate(WriteData writeData)
		{
			var template = new StubSourceTemplate()
			{
				ParentFunction = writeData.Test.Target,
				Config = writeData.CodeConfig
			};
			return template;
		}
	}
}
