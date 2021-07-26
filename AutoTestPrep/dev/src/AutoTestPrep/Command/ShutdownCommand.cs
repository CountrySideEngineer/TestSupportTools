using AutoTestPrep.Command.Argument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Command
{
	public class ShutdownCommand : AProjectChangeCommand
	{
		/// <summary>
		/// Execute command to shutdowns application.
		/// </summary>
		/// <param name="parameter"></param>
		public override void Execute(object parameter)
		{
			var argument = parameter as ProjectCommandArgument;
			base.SaveBaseDataIfChanged(argument);

			System.Windows.Application.Current.Shutdown();
		}

	}
}
