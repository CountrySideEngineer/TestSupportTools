using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Command
{
	public interface IPluginCommand
	{
		void Execute(object commandArg);
	}
}
