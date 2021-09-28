using CodeGenerator.Data;
using CodeGenerator.Stub.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Stub
{
	public class StubHeaderGenerator : StubCodeGenerator
	{
		/// <summary>
		/// Create template class.
		/// </summary>
		/// <param name="writeData">Write data.</param>
		/// <returns>Template class to create stub header file.</returns>
		protected override StubTemplate CreateTemplate(WriteData writeData)
		{
			var template = new StubHeaderTemplate()
			{
				ParentFunction = writeData.Test.Target,
				Config = writeData.CodeConfig
			};
			return template;
		}
	}
}
