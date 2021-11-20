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
		/// About, summary of output.
		/// </summary>
		public string About { get; protected set; }

		/// <summary>
		/// Message about plugin
		/// </summary>
		public string Message { get; protected set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public PluginOutput()
		{
			this.About = string.Empty;
			this.Message = string.Empty;
		}

		/// <summary>
		/// Constructor with message.
		/// </summary>
		/// <param name="message">Message about result of plugin.</param>
		public PluginOutput(string message)
		{
			this.About = string.Empty;
			this.Message = message;
		}

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="about">About plugin.</param>
		/// <param name="message">Message about result of plugin.</param>
		public PluginOutput(string about, string messgae)
		{
			this.About = about;
			this.Message = messgae;
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
