using CodeGenerator.Data;
using CodeGenerator.TestDriver.Template;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.TestDriver.GoogleTest
{
	public abstract class GoogleTestCodeGenerator : ICodeGenerator
	{
		/// <summary>
		/// Generate stub code.
		/// </summary>
		/// <param name="data">Data for code.</param>
		/// <returns>Generated stub code.</returns>
		/// <exception cref="NullReferenceException">One of object refered in a template is NULL.</exception>
		public string Generate(WriteData data)
		{
			try
			{
				var template = this.CreateTemplate(data);
				return template.TransformText();
			}
			catch (NullReferenceException ex)
			{
				Debug.WriteLine(ex.Message);
				throw;
			}

		}

		/// <summary>
		/// Abstract method to create template.
		/// </summary>
		/// <returns>StubTemplate class.</returns>
		protected abstract GoogleTestTemplate CreateTemplate(WriteData writeData);
	}
}
