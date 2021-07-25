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
	public class NewProjectCommand : IProjectCommand
	{
		public virtual void Execute(object parameter)
		{
			var argument = parameter as ProjectCommandArgument;
			this.SaveBaseDataIfChanged(argument);
			argument.LatestData = new TestDataInfo();
		}

		/// <summary>
		/// If the data <para>argument.BaseData</para> has been changed, confirm with the user
		/// and save the data.
		/// </summary>
		/// <param name="argument">Data to compare, base and latest.</param>
		protected virtual void SaveBaseDataIfChanged(ProjectCommandArgument argument)
		{
			TestDataInfo baseData = argument.BaseData;
			TestDataInfo latestData = argument.LatestData;
			if (!(baseData.Equals(latestData)))
			{
				MessageBoxResult result = MessageBox.Show("データが変更されています。\n" +
					"保存しますか？",
					"変更/保存確認",
					MessageBoxButton.YesNo,
					MessageBoxImage.None);
				if (MessageBoxResult.Yes == result)
				{

					var command = new OverWriteProjectCommand();
					command.Execute(argument);
				}
			}
		}
	}
}
