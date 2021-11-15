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
		/// Id of plugin in number.
		/// </summary>
		public int Id { get; set; }

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
