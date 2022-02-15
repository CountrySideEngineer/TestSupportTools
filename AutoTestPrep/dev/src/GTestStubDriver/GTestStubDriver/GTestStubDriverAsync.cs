using CountrySideEngineer.ProgressWindow.Model;
using CountrySideEngineer.ProgressWindow.Model.Interface;
using StubDriverPlugin.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StubDriverPlugin.GTestStubDriver
{
	public class GTestStubDriverAsync : IAsyncTask<ProgressInfo>
	{
		/// <summary>
		/// Test stub and driver generator.
		/// </summary>
		public GTestStubDriver StubDriver { get; set; }

		/// <summary>
		/// Input data for plugin.
		/// </summary>
		public PluginInput Input { get; set; }

		/// <summary>
		/// Plugin output data.
		/// </summary>
		public PluginOutput Output { get; set; }

		/// <summary>
		/// Run task to generate stub and driver.
		/// </summary>
		/// <param name="progress"></param>
		public void RunTask(IProgress<ProgressInfo> progress)
		{
			Debug.Assert(null != progress, $"{nameof(GTestStubDriverAsync)}.{nameof(RunTask)}, {nameof(progress)}");

			throw new NotImplementedException();
		}
	}
}
