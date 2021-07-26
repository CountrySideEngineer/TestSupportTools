using AutoTestPrep.Command.Argument;
using AutoTestPrep.Command.Exception;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Command
{
	public class OverWriteProjectCommand : SaveProjectCommand
	{
		/// <summary>
		/// Write data into a file.
		/// </summary>
		/// <param name="parameter">Parameters about command.</param>
		public override void Execute(object parameter)
		{
			try
			{
				var argument = parameter as ProjectCommandArgument;
				string filePath = argument.FilePath;
				if ((string.IsNullOrEmpty(filePath)) ||
					(string.IsNullOrWhiteSpace(filePath)))
				{
					base.Execute(parameter);
				}
				else
				{
					if (!(File.Exists(filePath)))
					{
						base.Execute(parameter);
					}
					else
					{
						base.Save(filePath, argument.LatestData);
					}
				}
			}
			catch (CommandCancelException)
			{
				//Cancel does not need to notify to user.
				Debug.WriteLine("Load canceled by user");
			}
		}
	}
}
