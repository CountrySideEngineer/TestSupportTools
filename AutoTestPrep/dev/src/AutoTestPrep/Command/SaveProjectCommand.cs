using AutoTestPrep.Command.Argument;
using AutoTestPrep.Command.Exception;
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

		public virtual void Execute(object data)
		{
			try
			{
				var argument = data as ProjectCommandArgument;
				string filePath = this.GetFilePath(argument.FilePath);
				this.Save(filePath, argument.LatestData);
				argument.FilePath = filePath;
			}
			catch (CommandCancelException)
			{
				//Cancel does not need to notify to user.
				Debug.WriteLine("Save canceled by user");
			}
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
			try
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
			catch (CommandCancelException)
			{
				Debug.WriteLine("Save canceled by user");
				throw;
			}
		}

		/// <summary>
		/// Get file path to write data into via file dialog.
		/// </summary>
		/// <returns>File path to write data into.</returns>
		/// <exception cref="CommandCancelException">Specify file canceled.</exception>
		protected virtual string GetSaveFilePath()
		{
			try
			{
				var dialog = new SaveFileDialog();
				string filePath = this.GetSaveFilePath(dialog);
				return filePath;
			}
			catch (CommandCancelException)
			{
				Debug.WriteLine("Save canceled by user");
				throw;
			}
		}

		/// <summary>
		/// Get file path to write data into via file dialog.
		/// </summary>
		/// <param name="directoryInfo">Information on the directory displayed when a dialog is displayed.</param>
		/// <returns>File path to write data into.</returns>
		/// <exception cref="CommandCancelException">Specify file canceled.</exception>
		protected virtual string GetSaveFilePath(DirectoryInfo directoryInfo)
		{
			try
			{
				var dialog = new SaveFileDialog();
				dialog.InitialDirectory = directoryInfo.FullName;
				string filePath = this.GetSaveFilePath(dialog);
				return filePath;
			}
			catch (CommandCancelException)
			{
				Debug.WriteLine("Save canceled by user");
				throw;
			}
		}

		/// <summary>
		/// Get file path to write data into via file dialog.
		/// </summary>
		/// <param name="dialog">File dialog object.</param>
		/// <returns>File path to write data into.</returns>
		/// <exception cref="CommandCancelException">Specify file canceled.</exception>
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
			else
			{
				throw new CommandCancelException();
			}
			return filePath;
		}


	}
}
