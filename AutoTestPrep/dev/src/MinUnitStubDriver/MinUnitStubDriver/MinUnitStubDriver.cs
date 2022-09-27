using CodeGenerator;
using CodeGenerator.Data;
using CodeGenerator.Stub;
using CodeGenerator.TestDriver.MinUnit;
using CountrySideEngineer.ProgressWindow.Model;
using CountrySideEngineer.ProgressWindow.Task;
using CountrySideEngineer.ProgressWindow.Task.Interface;
using MinUnitStubDriver.MinUnitStubDriver;
using StubDriverPlugin.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Data;
using TestParser.ParserException;

namespace StubDriverPlugin.MinUnitStubDriver
{
	public class MinUnitStubDriver : IStubDriverPlugin
	{
		/// <summary>
		/// Plugin input data.
		/// </summary>
		PluginInput pluginInput;

		/// <summary>
		/// Plugin result, output data.
		/// </summary>
		PluginOutput pluginOutput;

		/// <summary>
		/// ProgressInfo object.
		/// </summary>
		IProgress<ProgressInfo> _progress;

		/// <summary>
		/// Execute process to create stub and test driver code using google test framework.
		/// </summary>
		/// <param name="data">Pluing input data.</param>
		/// <returns>Plugin ouput data containig result of the plugin.</returns>
		public virtual PluginOutput Execute(PluginInput data)
		{
			pluginInput = data;
			var task = new AsyncTask<ProgressInfo>();
			SetupAction(ref task);
			var progressWindow = new CountrySideEngineer.ProgressWindow.ProgressWindow();
			progressWindow.Start(task);
			return pluginOutput;
		}

		/// <summary>
		/// Setup action to task to run in other thread.
		/// </summary>
		/// <param name="task">Reference AsyncTask object to setup task.</param>
		protected virtual void SetupAction(ref AsyncTask<ProgressInfo> task)
		{
			task.TaskAction = ((progress) =>
			{
				_progress = progress;

				var pluginExecute = new MinUnitStubDriverPluginExecute();
				pluginExecute.NotifyPluginProgressDelegate += NotifyParseProgress;
				pluginExecute.NotifyPluginFinishDelegate += NotifyPluginFinish;
				pluginOutput = pluginExecute.Execute(pluginInput);
			});
		}

		/// <summary>
		/// Event handler to notify plugin progress.
		/// </summary>
		/// <param name="name">Process name.</param>
		/// <param name="numerator">Numerator of progress.</param>
		/// <param name="denominator">Denominator of progress.</param>
		protected virtual void NotifyParseProgress(string name, int numerator, int denominator)
		{
			ProgressInfo progress = SetupProgressInfo(name, numerator, denominator);
			_progress.Report(progress);
		}

		/// <summary>
		/// Event handler to notify plugin finished.
		/// </summary>
		protected virtual void NotifyPluginFinish()
		{
			ProgressInfo progress = SetupFinishInfo();
			_progress.Report(progress);
		}

		/// <summary>
		/// Setup progress information to notify progress window.
		/// </summary>
		/// <param name="name">Process name.</param>
		/// <param name="numerator">Numerator of progress.</param>
		/// <param name="denominator">Denominator of progress.</param>
		/// <returns>ProgressInfo object which includes the progress information about plugin.</returns>
		protected virtual ProgressInfo SetupProgressInfo(string name, int numerator, int denominator)
		{
			int percent = 0;
			try
			{
				percent = (numerator * 100) / denominator;
			}
			catch (DivideByZeroException)
			{
				percent = 0;
			}

			string procName = string.Empty;
			if ((!string.IsNullOrEmpty(name)) || (!string.IsNullOrWhiteSpace(name)))
			{
				procName = name;
			}

			string title = string.Empty;
			try
			{
				title = pluginInput.InputFilePath;
			}
			catch (NullReferenceException)
			{
				title = string.Empty;
			}

			var progressInfo = new ProgressInfo()
			{
				Title = title,
				Denominator = denominator,
				Numerator = numerator,
				Progress = percent,
				ProcessName = procName
			};
			return progressInfo;
		}

		/// <summary>
		/// Seutp progress information to notify progress that the plugin finished.
		/// </summary>
		/// <returns>ProgressInfo object which incudes information that the plugin process is complete.</returns>
		protected virtual ProgressInfo SetupFinishInfo()
		{
			string title = string.Empty;
			try
			{
				title = pluginInput.InputFilePath;
			}
			catch (NullReferenceException)
			{
				title = string.Empty;
			}
			var progressInfo = new ProgressInfo()
			{
				Title = title,
				ProcessName = "完了",
				ShouldContinue = false,
			};
			return progressInfo;
		}
	}
}
