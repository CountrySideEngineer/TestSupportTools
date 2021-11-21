using AutoTestPrep.Command.Argument;
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
		public ExecPluginCommand() : base()
		{
			string currentDir = System.IO.Directory.GetCurrentDirectory();
			string dbPath = $@"{currentDir}\db\DefaultPlugin.plugin";
			string tableName = "DefaultPlugin";
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
		public override void Execute(object commandArg)
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
		/// <param name="pluginInfo">Parameter for plugin.</param>
		/// <param name="pluginInput">Input data for plugin.</param>
		/// <returns>Result of plugin as PluginOutput object.</returns>
		protected PluginOutput ExecutePlugin(PluginInfo pluginInfo, PluginInput pluginInput)
		{
			var manager = new PluginManager(this.DbPath, this.DbTableName);
			IStubDriverPlugin plugin = manager.Load(pluginInfo);
			PluginOutput pluginOutput = plugin.Execute(pluginInput);
			return pluginOutput;
		}
	}
}
