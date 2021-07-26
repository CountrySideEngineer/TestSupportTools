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
	public abstract class AProjectChangeCommand : IProjectCommand
	{
		public abstract void Execute(object parameter);

		/// <summary>
		/// If the data <para>argument.BaseData</para> has been changed, confirm with the user
		/// and save the data.
		/// </summary>
		/// <param name="argument">Data to compare, base and latest.</param>
		protected virtual void SaveBaseDataIfChanged(ProjectCommandArgument argument)
		{
			if (!(argument.BaseData.Equals(argument.BaseData)))
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
