using Plugin;
using Plugin.Manager;
using StubDriverPlugin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Command.Argument
{
	public class PluginCommandArgument
	{
		/// <summary>
		/// Plugin information to pass to command.
		/// </summary>
		public PluginInfo PluginInfo { get; protected set; }

		/// <summary>
		/// Plugin input data.
		/// </summary>
		public PluginInput PluginInput { get; protected set; }

		/// <summary>
		/// Result of command.
		/// </summary>
		public PluginOutput PluginOutput { get; set; }

		/// <summary>
		/// Default 
		/// </summary>
		public PluginCommandArgument()
		{
			this.PluginInfo = null;
			this.PluginInput = null;
			this.PluginOutput = null;
		}

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="pluginInfo">Plugin information.</param>
		public PluginCommandArgument(PluginInfo pluginInfo, PluginInput pluginInput)
		{
			this.PluginInfo = pluginInfo;
			this.PluginInput = pluginInput;
			this.PluginOutput = null;
		}
	}
}
