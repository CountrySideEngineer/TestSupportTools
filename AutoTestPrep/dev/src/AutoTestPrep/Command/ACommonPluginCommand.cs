using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggerInterface =  CSEngineer.Logger.Interface;
using CSEngineer.Logger;

namespace AutoTestPrep.Command
{
	public abstract class ACommonPluginCommand : IPluginCommand, LoggerInterface.ILog
	{
		/// <summary>
		/// Path of database file.
		/// </summary>
		public string DbPath { get; protected set; }

		/// <summary>
		/// Name of table to access.
		/// </summary>
		public string DbTableName { get; protected set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		protected ACommonPluginCommand()
		{
			this.DbPath = string.Empty;
			this.DbTableName = string.Empty;
		}

		public abstract void Execute(object commandArg);

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
