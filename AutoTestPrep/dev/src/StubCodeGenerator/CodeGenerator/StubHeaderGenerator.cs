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
	public class StubHeaderGenerator : StubCodeGenerator
	{
		/// <summary>
		/// Create template class.
		/// </summary>
		/// <param name="writeData">Write data.</param>
		/// <returns>Template class to create stub header file.</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="NullReferenceException"></exception>
		protected override StubTemplate CreateTemplate(WriteData writeData)
		{
			Debug.Assert(null != writeData.Test, $"{nameof(StubHeaderGenerator)}.{nameof(CreateTemplate)}.writeData.Test");
			Debug.Assert(null != writeData.Test.Target, $"{nameof(StubHeaderGenerator)}.{nameof(CreateTemplate)}.writeData.Test.Target");
			Debug.Assert(null != writeData.CodeConfig, $"{nameof(StubHeaderGenerator)}.{nameof(CreateTemplate)}.writeData.CodeConfig");

			var template = new StubHeaderTemplate()
			{
				ParentFunction = writeData.Test.Target,
				Config = writeData.CodeConfig
			};
			return template;
		}
	}
}
