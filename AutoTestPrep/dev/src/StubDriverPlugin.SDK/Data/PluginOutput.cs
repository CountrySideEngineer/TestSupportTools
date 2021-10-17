using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StubDriverPlugin.Data
{
	public class PluginOutput
	{
		/// <summary>
		/// Message about plugin
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public PluginOutput()
		{
			this.Message = string.Empty;
		}

		/// <summary>
		/// Constructor with message.
		/// </summary>
		/// <param name="message">Message about result of plugin.</param>
		public PluginOutput(string message)
		{
			this.Message = message;
		}

		/// <summary>
		/// Copy constructor.
		/// </summary>
		/// <param name="src">Copy source object.</param>
		public PluginOutput(PluginOutput src)
		{
			this.Message = src.Message;
		}

	}
}
