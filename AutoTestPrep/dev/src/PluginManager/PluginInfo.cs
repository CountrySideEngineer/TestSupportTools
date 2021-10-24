using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin
{
	public class PluginInfo
	{
		/// <summary>
		/// Name of plugin.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Path to plugin dll.
		/// </summary>
		public string FileName { get; set; }
	}
}
