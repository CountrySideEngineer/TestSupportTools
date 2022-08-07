using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSEngineer.Logger;

namespace TestParser.Parser
{
	public abstract class ATestParser : IParser, CSEngineer.Logger.Interface.ILog
	{
		/// <summary>
		/// Delegate to notify progress of parsing test.
		/// </summary>
		/// <param name="numerator">Progress numerator</param>
		/// <param name="denominator">Progress denominator</param>
		public delegate void NotifyParseProgress(int numerator, int denominator);
		public NotifyParseProgress NotifyParseProgressDelegate;

		public delegate void NotifyProcessAndProgress(string procName, int numerator, int denominator);
		public NotifyProcessAndProgress NotifyProcessAndProgressDelegate;

		/// <summary>
		/// Parser to read function list.
		/// </summary>
		public AParser FunctionListParser { get; set; }

		/// <summary>
		/// Parser to read function.
		/// </summary>
		public AParser FunctionParser { get; set; }

		/// <summary>
		/// Parser to read test cases.
		/// </summary>
		public AParser TestCaseParser { get; set; }

		/// <summary>
		/// Abstract function to read function.
		/// </summary>
		/// <param name="path">Paht to file designing test.</param>
		/// <returns>Object about test.</returns>
		public abstract object Parse(string path);

		/// <summary>
		/// Abstract function to read function.
		/// </summary>
		/// <param name="path">Stream to read from data to parse.</param>
		/// <returns>Object about test.</returns>
		public abstract object Parse(Stream stream);

		/// <summary>
		/// Output TRACE level log message.
		/// </summary>
		/// <param name="message">Output message.</param>
		public void TRACE(string message)
		{
			Log.GetInstance().TRACE(message);
		}

		/// <summary>
		/// Output DEBUG level log message.
		/// </summary>
		/// <param name="message">Output message.</param>
		public void DEBUG(string message)
		{
			Log.GetInstance().DEBUG(message);
		}

		/// <summary>
		/// Output INFO (information) level log message.
		/// </summary>
		/// <param name="message">Output message.</param>
		public void INFO(string message)
		{
			Log.GetInstance().INFO(message);
		}

		/// <summary>
		/// Output WARN (warning) level log message.
		/// </summary>
		/// <param name="message">Output message.</param>
		public void WARN(string message)
		{
			Log.GetInstance().WARN(message);
		}

		/// <summary>
		/// Output ERROR level log message.
		/// </summary>
		/// <param name="message">Output message.</param>
		public void ERROR(string message)
		{
			Log.GetInstance().ERROR(message);
		}

		/// <summary>
		/// Output FATAL level log message.
		/// </summary>
		/// <param name="message">Output message.</param>
		public void FATAL(string message)
		{
			Log.GetInstance().FATAL(message);
		}
	}
}
