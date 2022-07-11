using CodeGenerator.Data;
using CodeGenerator.TestDriver.Template;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSEngineer.Logger.Interface;
using CSEngineer.Logger;

namespace CodeGenerator.TestDriver.MinUnit
{
	public abstract class MinUnitCodeGenerator : ICodeGenerator, ILog
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
		/// Output DEBUG level log message.
		/// </summary>
		/// <param name="message">Log message.</param>
		public void DEBUG(string message)
		{
			Log.GetInstance().DEBUG(message);
		}

		/// <summary>
		/// Output ERROR level log message.
		/// </summary>
		/// <param name="message">Log message.</param>
		public void ERROR(string message)
		{
			Log.GetInstance().ERROR(message);
		}

		/// <summary>
		/// Output FATAL level log message.
		/// </summary>
		/// <param name="message">Log message.</param>
		public void FATAL(string message)
		{
			Log.GetInstance().FATAL(message);
		}

		/// <summary>
		/// Output INFO level log message.
		/// </summary>
		/// <param name="message">Log message.</param>
		public void INFO(string message)
		{
			Log.GetInstance().INFO(message);
		}

		/// <summary>
		/// Output TRACE level log message.
		/// </summary>
		/// <param name="message">Log message.</param>
		public void TRACE(string message)
		{
			Log.GetInstance().TRACE(message);
		}

		/// <summary>
		/// Output WARN level log message.
		/// </summary>
		/// <param name="message">Log message.</param>
		public void WARN(string message)
		{
			Log.GetInstance().WARN(message);
		}

		/// <summary>
		/// Abstract method to create template.
		/// </summary>
		/// <returns>StubTemplate class.</returns>
		protected abstract MinUnitTemplate CreateTemplate(WriteData writeData);
	}
}
