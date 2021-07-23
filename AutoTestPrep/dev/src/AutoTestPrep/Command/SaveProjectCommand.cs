using AutoTestPrep.Command.Argument;
using AutoTestPrep.Model.InputInfos;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AutoTestPrep.Command
{
	public class SaveProjectCommand
	{
		public void Run(object data)
		{
			var argument = data as ProjectCommandArgument;
			argument.FilePath = this.GetSaveFilePath();

			var xmlSerializer = new XmlSerializer(typeof(TestDataInfo));
			using (var xmlStream = new StreamWriter(argument.FilePath, false, Encoding.UTF8))
			{
				xmlSerializer.Serialize(xmlStream, argument.TestDataInfo);
			}
		}

		protected string GetSaveFilePath()
		{
			string filePath = string.Empty;
			var dialog = new SaveFileDialog();
			dialog.Title = "名前をつけて保存";
			dialog.Filter = "プロジェクトファイル|*.tstprj|全てのファイル|*.*";
			dialog.AddExtension = true;
			dialog.DefaultExt = "tstprj";
			dialog.FilterIndex = 1;
			if (true == dialog.ShowDialog())
			{
				filePath = dialog.FileName;
			}
			return filePath;
		}


	}
}
