using StubDriverPlugin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StubDriverPlugin
{
	/// <summary>
	/// Interface of plugin for stub and driver.
	/// </summary>
	public interface IStubDriverPlugin
	{
		/// <summary>
		/// Interface to execute plugin
		/// </summary>
		/// <param name="data">Data to be used in plugin.</param>
		/// <returns>Result of plugin execution.</returns>
		PluginOutput Execute(PluginInput data);
	}
}
