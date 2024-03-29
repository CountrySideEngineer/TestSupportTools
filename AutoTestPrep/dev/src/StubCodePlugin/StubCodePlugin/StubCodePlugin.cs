﻿using CountrySideEngineer.ProgressWindow.Model;
using CountrySideEngineer.ProgressWindow.Task.Interface;
using StubDriverPlugin;
using StubDriverPlugin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StubDriverPlugin.StubCodePlugin
{
	public class StubCodePlugin : IStubDriverPlugin, IAsyncTask<ProgressInfo>
	{
		/// <summary>
		/// Plugin input data.
		/// </summary>
		PluginInput pluginInput;

		/// <summary>
		/// Plugin output data.
		/// </summary>
		PluginOutput pluginOutput;

		public virtual PluginOutput Execute(PluginInput data)
		{
			pluginInput = data;
			var progressWindow = new CountrySideEngineer.ProgressWindow.ProgressWindow();
			progressWindow.Start(this);

			return pluginOutput;
		}

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
				StubCodePluginExecute plugin = new StubCodePluginExecute();
				plugin.NotifyParseProgressDelegate += (name, numerator, denominator) =>
				{
					int percent = 0;
					if (0 == denominator)
					{
						percent = 0;
					}
					else
					{
						percent = (numerator * 100) / denominator;
					}

					var progressInfo = new ProgressInfo()
					{
						Title = data.InputFilePath,
						Denominator = denominator,
						Numerator = numerator,
						Progress = percent,
					};
					if ((!string.IsNullOrEmpty(name)) || (!string.IsNullOrWhiteSpace(name)))
					{
						progressInfo.ProcessName = name;
					}
					progress.Report(progressInfo);
				};
				plugin.NotifyPluginFinishDelegate += () =>
				{
					var progressInfo = new ProgressInfo()
					{
						Title = data.InputFilePath,
						ProcessName = "完了",
						ShouldContinue = false,
					};
					progress.Report(progressInfo);
				};
				pluginOutput = plugin.Execute(pluginInput);
				return pluginOutput;
			});
			return task;
		}
	}
}
