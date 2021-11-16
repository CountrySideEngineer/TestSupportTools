using CodeGenerator;
using CodeGenerator.Data;
using CodeGenerator.Stub.Template;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Stub
{
	public abstract class StubCodeGenerator : ICodeGenerator
	{
		/// <summary>
		/// Generate stub code.
		/// </summary>
		/// <param name="data">Data for code.</param>
		/// <returns>Generated stub code.</returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		public string Generate(WriteData data)
		{
			Debug.Assert(null != data, $"{nameof(StubCodeGenerator)}.{nameof(Generate)}.data");

			try
			{
				var template = this.CreateTemplate(data);
				return template.TransformText();
			}
			catch (Exception ex)
			when ((ex is ArgumentException) || (ex is ArgumentNullException))
			{
				Debug.WriteLine(ex.StackTrace);

				throw;
			}
		}

		/// <summary>
		/// Abstract method to create template.
		/// </summary>
		/// <returns>StubTemplate class.</returns>
		protected abstract StubTemplate CreateTemplate(WriteData writeData);
	}
}
