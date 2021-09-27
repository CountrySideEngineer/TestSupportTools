using CodeGenerator;
using CodeGenerator.Data;
using CodeGenerator.Stub.Template;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Stub
{
	public abstract class StubCodeWriter : ICodeGenerator
	{
		/// <summary>
		/// Generate stub code.
		/// </summary>
		/// <param name="data">Data for code.</param>
		/// <returns>Generated stub code.</returns>
		public string Generate(WriteData data)
		{
			var template = this.CreateTemplate(data);
			return template.TransformText();
		}

		/// <summary>
		/// Abstract method to create template.
		/// </summary>
		/// <returns>StubTemplate class.</returns>
		protected abstract StubTemplate CreateTemplate(WriteData writeData);
	}
}
