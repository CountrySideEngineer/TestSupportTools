using CSEngineer.Logger;
using CSEngineer.Logger.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Data;

namespace TestParser.Converter
{
	public abstract class ATestCaseTableItemConverter : ITestCaseTableItemConverter, ILog
	{
		public abstract void Convert(ref TestData testData);

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
