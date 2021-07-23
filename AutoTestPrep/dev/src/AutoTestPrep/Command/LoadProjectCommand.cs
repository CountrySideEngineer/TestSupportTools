using AutoTestPrep.Command.Argument;
using AutoTestPrep.Model.InputInfos;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AutoTestPrep.Command
{
	public class LoadProjectCommand
	{
		public void Run(object data)
		{
			var argument = data as ProjectCommandArgument;
			string filePath = this.GetLoadFilePath();
			var xmlDeserializer = new XmlSerializer(typeof(TestDataInfo));
			using (var xmlStream = new StreamReader(filePath, Encoding.UTF8))
			{
				object obj = xmlDeserializer.Deserialize(xmlStream);
				argument.TestDataInfo = (TestDataInfo)obj;
			}
		}

		protected string GetLoadFilePath()
		{
			string filePath = string.Empty;
			var dialog = new OpenFileDialog();
			dialog.Title = "テストプロジェクトを開く";
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
