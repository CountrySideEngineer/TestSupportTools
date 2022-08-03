using StubDriverPlugin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Parser;
using TestParser.ParserException;
using TestParser.Data;
using CodeGenerator.Data;
using CodeGenerator.Stub;
using System.IO;
using CodeGenerator;
using CodeGenerator.TestDriver.GoogleTest;
using System.Diagnostics;
using CountrySideEngineer.ProgressWindow.Model;
using CountrySideEngineer.ProgressWindow.Model.Interface;
using System.Threading;

namespace StubDriverPlugin.GTestStubDriver
{
	public class GTestStubDriver : IStubDriverPlugin, IAsyncTask<ProgressInfo>
	{
		PluginInput pluginInput;
		PluginOutput pluginOutput;

		/// <summary>
		/// Execute process to create stub and test driver code using google test framework.
		/// </summary>
		/// <param name="data">Pluing input data.</param>
		/// <returns>Plugin ouput data containig result of the plugin.</returns>
		public virtual PluginOutput Execute(PluginInput data)
		{
			pluginInput = data;
			var progressWindow = new CountrySideEngineer.ProgressWindow.ProgressWindow();
			progressWindow.Start(this);

			return pluginOutput;
		}

		/// <summary>
		/// Execute task asynchronously.
		/// </summary>
		/// <param name="progress">IProgress object to notify progress of the process.</param>
		public void RunTask(IProgress<ProgressInfo> progress)
		{
			Task task = ExecuteAsync(progress, pluginInput);
		}

		/// <summary>
		/// Execute task asynchronously.
		/// </summary>
		/// <param name="progress">IProgress object to notify progress of the process.</param>
		/// <param name="data">Pluing input data.</param>
		/// <returns>Task executed.</returns>
		protected virtual async Task ExecuteAsync(IProgress<ProgressInfo> progress, PluginInput data)
		{
			Task<PluginOutput> task = CreateTask(progress, data);
			await task;
		}

		/// <summary>
		/// Create task to run process to create stub and test driver code.
		/// </summary>
		/// <param name="progress">IProgress object to notify progress of the process.</param>
		/// <param name="data">Pluing input data.</param>
		/// <returns>Task executed.</returns>
		protected virtual Task<PluginOutput> CreateTask(IProgress<ProgressInfo> progress, PluginInput data)
		{
			Task<PluginOutput> task = Task<PluginOutput>.Run(() =>
			{
				GTestStubDriverPluginExecute plugin = new GTestStubDriverPluginExecute();
				plugin.NotifyParseProgressDelegate += (numerator, denominator) =>
				{
					var progressInfo = new ProgressInfo()
					{
						Title = data.InputFilePath,
						ProcessName = "解析中",
						Denominator = denominator,
						Numerator = numerator,
						Progress = (numerator * 100) / denominator,
					};
					progress.Report(progressInfo);
				};
				pluginOutput = ExecutePlugin(plugin, data);
				return pluginOutput;
			});
			return task;
		}

		protected virtual PluginOutput ExecutePlugin(IStubDriverPlugin plugin, PluginInput input)
		{
			PluginOutput exePluginOutput = plugin.Execute(input);

			return exePluginOutput;
		}
	}
}
