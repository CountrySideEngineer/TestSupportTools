using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSEngineer.Logger;

namespace TestParser.Parser
{
	public abstract class AParser : IParser, CSEngineer.Logger.Interface.ILog
	{
		/// <summary>
		/// Delegate to notify progress of parsing test.
		/// </summary>
		/// <param name="stage">Parse stage name.</param>
		/// <param name="messgae">Message</param>
		/// <param name="numerator">Progress numerator</param>
		/// <param name="denominator">Progress denominator</param>
		public delegate void NotifyParseProgress(string stage, string messgae, int numerator, int denominator);
		public NotifyParseProgress NotifyParseProgressDelegate;

		public Delegate Delegate;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public AParser()
		{
			Target = string.Empty;
		}

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="target">Target name to parse.</param>
		public AParser(string target)
		{
			Target = target;
		}

		/// <summary>
		/// Target name to parse.
		/// </summary>
		public string Target { get; set; }

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

		public void TRACE(string message)
		{
			var logger = Log.GetInstance();
			logger.TRACE(message);
		}

		public void DEBUG(string message)
		{
			var logger = Log.GetInstance();
			logger.DEBUG(message);
		}

		public void INFO(string message)
		{
			var logger = Log.GetInstance();
			logger.INFO(message);
		}

		public void WARN(string message)
		{
			var logger = Log.GetInstance();
			logger.WARN(message);
		}

		public void ERROR(string message)
		{
			var logger = Log.GetInstance();
			logger.ERROR(message);
		}

		public void FATAL(string message)
		{
			var logger = Log.GetInstance();
			logger.FATAL(message);
		}
	}
}
