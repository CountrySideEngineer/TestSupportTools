using AutoTestPrep.Command.Argument;
using AutoTestPrep.Model.InputInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoTestPrep.Command
{
	public class NewProjectCommand : AProjectChangeCommand
	{
		/// <summary>
		/// Execute command to create project.
		/// </summary>
		/// <param name="parameter">Command parameter.</param>
		public override void Execute(object parameter)
		{
			var argument = parameter as ProjectCommandArgument;
			this.SaveBaseDataIfChanged(argument);
			argument.LatestData = new TestDataInfo();
		}
	}
}
