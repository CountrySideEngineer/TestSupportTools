using StubDriverPlugin;
using StubDriverPlugin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginSample
{
	public class PluginSampleA : IStubDriverPlugin
	{
		public PluginOutput Execute(PluginInput data)
		{
			var output = new PluginOutput()
			{
				Message = "PluginSampleDLL_A"
			};
			return output;
		}
	}
}
