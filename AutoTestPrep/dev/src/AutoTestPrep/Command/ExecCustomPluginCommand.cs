using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Command
{
	public class ExecCustomPluginCommand : ExecPluginCommand
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public ExecCustomPluginCommand() : base("CustomPlugin.plugin", "CustomPlugin") { }
	}
}
