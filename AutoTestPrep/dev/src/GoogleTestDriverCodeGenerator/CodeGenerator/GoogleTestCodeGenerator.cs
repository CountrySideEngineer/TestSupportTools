using CodeGenerator.Data;
using CodeGenerator.TestDriver.Template;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSEngineer.Logger;
using CSEngineer.Logger.Interface;

namespace CodeGenerator.TestDriver.GoogleTest
{
	public abstract class GoogleTestCodeGenerator : ICodeGenerator, ILog
	{
		/// <summary>
		/// Generate stub code.
		/// </summary>
		/// <param name="data">Data for code.</param>
		/// <returns>Generated stub code.</returns>
		/// <exception cref="NullReferenceException">One of object refered in a template is NULL.</exception>
		public string Generate(WriteData data)
		{
			Debug.Assert(null != data, $"{nameof(GoogleTestCodeGenerator)}.{nameof(Generate)}");

			try
			{
				INFO("Start generating test driver codes using google test framework.");

				var template = this.CreateTemplate(data);

				INFO("    Generate test driver codes of function using template.");
				INFO($"        {data.Test.Target.ToString()}");

				return template.TransformText();
			}
			catch (Exception ex)
			when ((ex is ArgumentNullException) || (ex is NullReferenceException))
			{
				DEBUG($"ex.StackTrace");
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
		protected abstract GoogleTestTemplate CreateTemplate(WriteData writeData);
	}
}
