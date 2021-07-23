using AutoTestPrep.Command.Argument;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Command
{
	public class OverWriteProjectCommand : SaveProjectCommand
	{
		public override void Run(object data)
		{
			var argument = data as ProjectCommandArgument;
			string filePath = argument.FilePath;
			if ((string.IsNullOrEmpty(filePath)) ||
				(string.IsNullOrWhiteSpace(filePath)))
			{
				base.Run(data);
			}
			else
			{
				if (!(File.Exists(filePath)))
				{
					base.Run(data);
				}
				else
				{
					base.Save(filePath, argument.TestDataInfo);
				}
			}
		}
	}
}
