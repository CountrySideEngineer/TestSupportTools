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
using CSEngineer.Logger.Interface;
using CSEngineer.Logger;

namespace CodeGenerator.Stub
{
	public abstract class StubCodeGenerator : ICodeGenerator, ILog
	{
		/// <summary>
		/// Generate stub code.
		/// </summary>
		/// <param name="data">Data for code.</param>
		/// <returns>Generated stub code.</returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		public virtual string Generate(WriteData data)
		{
			Debug.Assert(null != data, $"{nameof(StubCodeGenerator)}.{nameof(Generate)}.data");

			try
			{
				INFO("Start generating stub code.");

				var template = this.CreateTemplate(data);
				string codes = template.TransformText();
				return codes;
			}
			catch (Exception ex)
			when ((ex is ArgumentException) || (ex is ArgumentNullException))
			{
				ERROR("An error occurred while generating codes.");
				ERROR($"    {ex.Message}");

				throw;
			}
			catch (Exception ex)
			{
				ERROR("An unknown error occurred while generating codes.");
				ERROR($"    {ex.Message}");

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
		protected abstract StubTemplate CreateTemplate(WriteData writeData);
	}
}
