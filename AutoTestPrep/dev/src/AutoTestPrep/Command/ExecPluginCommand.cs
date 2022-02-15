using AutoTestPrep.Command.Argument;
using CSEngineer;
using Plugin;
using Plugin.TestStubDriver;
using StubDriverPlugin;
using StubDriverPlugin.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Command
{
	public class ExecPluginCommand : ACommonPluginCommand
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		protected ExecPluginCommand() : base() { }

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="dbName">Plugin data base file name in db directory.</param>
		/// <param name="tableName">Plugin table name.</param>
		public ExecPluginCommand(string dbName, string tableName) : base()
		{
			string currentDir = System.IO.Directory.GetCurrentDirectory();
			string dbPath = $@"{currentDir}\db\{dbName}";
			base.DbPath = dbPath;
			base.DbTableName = tableName;
		}

		/// <summary>
		/// Execute plugin.
		/// </summary>
		/// <param name="commandArg">Argument data for plugin.</param>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="NullReferenceException"></exception>
		protected void _Execute(object commandArg)
		{
			try
			{
				if (!(commandArg is PluginCommandArgument))
				{
					throw new ArgumentException();
				}
				PluginCommandArgument pluginCommandArg = commandArg as PluginCommandArgument;
				PluginInfo pluginInfo = pluginCommandArg.PluginInfo;
				PluginInput pluginInput = pluginCommandArg.PluginInput;
				PluginOutput pluginOutput = this.ExecutePlugin(pluginInfo, pluginInput);
				pluginCommandArg.PluginOutput = pluginOutput;
			}
			catch (System.Exception ex)
			when ((ex is ArgumentException) || (ex is ArgumentNullException) || (ex is NullReferenceException))
			{
				Debug.WriteLine(ex.StackTrace);

				throw;
			}
		}

		/// <summary>
		/// Execute plugin.
		/// </summary>
		/// <param name="commandArg">Argument data for plugin.</param>
		public override void Execute(object commandArg)
		{
			try
			{
				string logFilePath = this.GetLogFilePath();
				using (var stream = new StreamWriter(logFilePath, false, Encoding.UTF8))
				{
					Logger.Level = Logger.LogLevel.All;
					Logger.AddStream(stream);
					Logger.INFO("Start logging!");
					this._Execute(commandArg);
					Logger.RemoveStream(stream);
				}
			}
			catch (System.Exception ex)
			when ((ex is ArgumentException) || (ex is ArgumentNullException) || (ex is NullReferenceException))
			{
				Debug.WriteLine(ex.StackTrace);

				throw;
			}
		}

		/// <summary>
		/// Create log file path.
		/// </summary>
		/// <returns>Path of log file.</returns>
		protected string GetLogFilePath()
		{
			DateTime dateTime = DateTime.Now;
			string dateTimeString = dateTime.ToString("yyyyMMddHHmmss");
			string logFileName = $"Log_{dateTimeString}.log";
			string logFilePath = $@"./{logFileName}";
			return logFilePath;
		}

		/// <summary>
		/// Execute plugin.
		/// </summary>
		/// <param name="pluginInfo">Parameter for plugin.</param>
		/// <param name="pluginInput">Input data for plugin.</param>
		/// <returns>Result of plugin as PluginOutput object.</returns>
		protected PluginOutput ExecutePlugin(PluginInfo pluginInfo, PluginInput pluginInput)
		{
			try
			{
				var manager = new PluginManager(this.DbPath, this.DbTableName);
				IStubDriverPlugin plugin = manager.Load(pluginInfo);
				PluginOutput pluginOutput = plugin.Execute(pluginInput);
				return pluginOutput;
			}
			catch (System.Exception ex)
			when (ex is TestParser.ParserException.TestParserException)
			{
				var parserException = ex as TestParser.ParserException.TestParserException;
				ushort code = parserException.ErrorCode;
				string codeString = $"0x{Convert.ToString(code, 16)}";
				return new PluginOutput(codeString);
			}
			catch (System.Exception)
			{
				return new PluginOutput("FAILED");
			}
		}
	}
}
