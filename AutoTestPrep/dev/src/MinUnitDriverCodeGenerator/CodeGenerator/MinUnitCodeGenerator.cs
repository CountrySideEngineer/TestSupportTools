using CodeGenerator.Data;
using CodeGenerator.TestDriver.Template;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.TestDriver.MinUnit
{
	public abstract class MinUnitCodeGenerator : ICodeGenerator
	{
		/// <summary>
		/// Generate test driver code using min_unit test framework.
		/// </summary>
		/// <param name="data">Data for code.</param>
		/// <returns>Generated test driver code.</returns>
		public string Generate(WriteData data)
		{
			Debug.Assert(null != data, $"{nameof(MinUnitCodeGenerator)}.{nameof(Generate)}");

			try
			{
				var template = this.CreateTemplate(data);
				return template.TransformText();
			}
			catch (Exception ex)
			when ((ex is ArgumentNullException) || (ex is NullReferenceException))
			{
				Debug.WriteLine(ex.StackTrace);
				throw;
			}
		}

		/// <summary>
		/// Abstract method to create template.
		/// </summary>
		/// <returns>StubTemplate class.</returns>
		protected abstract MinUnitTemplate CreateTemplate(WriteData writeData);
	}
}
