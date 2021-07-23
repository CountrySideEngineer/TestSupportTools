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
		public virtual void Run(object data)
		{
			var argument = data as ProjectCommandArgument;
			string filePath = this.GetFilePath(argument.FilePath);
			this.Save(filePath, argument.TestDataInfo);
			argument.FilePath = filePath;
		}

		public virtual void Save(string filePath, TestDataInfo testDataInfo)
		{
			var xmlSerializer = new XmlSerializer(typeof(TestDataInfo));
			using (var xmlStream = new StreamWriter(filePath, false, Encoding.UTF8))
			{
				xmlSerializer.Serialize(xmlStream, testDataInfo);
			}
		}

		protected virtual string GetFilePath(string filePath)
		{
			if ((string.IsNullOrEmpty(filePath)) ||
				(string.IsNullOrWhiteSpace(filePath)))
			{
				filePath = this.GetSaveFilePath();
			}
			else
			{
				if (File.Exists(filePath))
				{
					DirectoryInfo dirInfo = new DirectoryInfo(filePath);
					DirectoryInfo parentDir = dirInfo.Parent;
					filePath = this.GetSaveFilePath(parentDir);
				}
				else
				{
					filePath = this.GetSaveFilePath();
				}
			}
			return filePath;
		}

		protected virtual string GetSaveFilePath()
		{
			var dialog = new SaveFileDialog();
			string filePath = this.GetSaveFilePath(dialog);
			return filePath;
		}

		protected virtual string GetSaveFilePath(DirectoryInfo directoryInfo)
		{
			var dialog = new SaveFileDialog();
			dialog.InitialDirectory = directoryInfo.FullName;
			string filePath = this.GetSaveFilePath(dialog);
			return filePath;
		}

		protected virtual string GetSaveFilePath(FileDialog dialog)
		{
			string filePath = string.Empty;
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
