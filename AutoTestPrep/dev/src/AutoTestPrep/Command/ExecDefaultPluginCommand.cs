using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Command
{
	public class ExecDefaultPluginCommand : ExecPluginCommand
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public ExecDefaultPluginCommand() : base("DefaultPlugin.plugin", "DefaultPlugin") { }
	}
}
