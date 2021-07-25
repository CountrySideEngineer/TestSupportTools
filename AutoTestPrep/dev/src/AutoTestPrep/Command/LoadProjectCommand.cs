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
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace AutoTestPrep.Command
{
	public class LoadProjectCommand : NewProjectCommand
	{
		/// <summary>
		/// Load data user selected.
		/// </summary>
		/// <param name="parameter">Parameters about command.</param>
		/// <exception cref="CommandCancelException">Load data canceled.</exception>
		public override void Execute(object parameter)
		{
			try
			{
				var argument = parameter as ProjectCommandArgument;
				this.SaveBaseDataIfChanged(argument);

				string filePath = this.GetLoadFilePath();
				var xmlDeserializer = new XmlSerializer(typeof(TestDataInfo));
				using (var xmlStream = new StreamReader(filePath, Encoding.UTF8))
				{
					object obj = xmlDeserializer.Deserialize(xmlStream);
					argument.LatestData = (TestDataInfo)obj;
				}
				argument.FilePath = filePath;
			}
			catch (CommandCancelException)
			{
				//Cancel does not need to notify to user.
				Debug.WriteLine("Load canceled by user");
			}
		}

		/// <summary>
		/// Get file path to load.
		/// </summary>
		/// <param name="filePath">File path to load.</param>
		/// <returns>File path to load.</returns>
		/// <exception cref="CommandCancelException">Cancel in a file dialog.</exception>
		protected string GetLoadFilePath(string filePath)
		{
			try
			{
				string loadFilePath = string.Empty;
				if ((string.IsNullOrEmpty(filePath)) || (string.IsNullOrWhiteSpace(filePath)))
				{
					loadFilePath = this.GetLoadFilePath();
				}
				else
				{
					if (File.Exists(filePath))
					{
						DirectoryInfo defaultDirectory = Directory.GetParent(filePath);
						loadFilePath = this.GetLoadFilePath(defaultDirectory);
					}
					else if (Directory.Exists(filePath))
					{
						DirectoryInfo defaultDirectory = new DirectoryInfo(filePath);
						loadFilePath = this.GetLoadFilePath(defaultDirectory);
					}
					else
					{
						loadFilePath = this.GetLoadFilePath();
					}
				}
				return loadFilePath;
			}
			catch (CommandCancelException)
			{
				Debug.WriteLine("Load canceled by user");
				throw;
			}
		}

		/// <summary>
		/// Get file path to load.
		/// </summary>
		/// <returns>File path to load.</returns>
		/// <exception cref="CommandCancelException">Cancel in a file dialog.</exception>
		protected string GetLoadFilePath()
		{
			try
			{
				var dialog = new OpenFileDialog();
				return this.GetLoadFilePath(dialog);
			}
			catch (CommandCancelException)
			{
				Debug.WriteLine("Load canceled by user");
				throw;
			}
		}

		/// <summary>
		/// Returns file path to load.
		/// </summary>
		/// <param name="directoryInfo">Information on the directory displayed when a dialog is displayed.</param>
		/// <returns>File path to load.</returns>
		/// <exception cref="CommandCancelException">Specify file canceled.</exception>
		protected string GetLoadFilePath(DirectoryInfo directoryInfo)
		{
			try
			{
				var dialog = new OpenFileDialog();
				dialog.InitialDirectory = directoryInfo.FullName;
				return this.GetLoadFilePath(dialog);
			}
			catch (CommandCancelException)
			{
				throw;
			}
		}

		/// <summary>
		/// Returns file path to load.
		/// </summary>
		/// <param name="dialog"><para>FileDialog</para> object to show to set or select file to load.</param>
		/// <returns>File path to load.</returns>
		/// <exception cref="CommandCancelException">Specify file canceled.</exception>
		protected string GetLoadFilePath(FileDialog dialog)
		{
			string filePath = string.Empty;
			dialog.Title = "テストプロジェクトを開く";
			dialog.Filter = "プロジェクトファイル|*.tstprj|全てのファイル|*.*";
			dialog.AddExtension = true;
			dialog.DefaultExt = "tstprj";
			dialog.FilterIndex = 1;
			if (true == dialog.ShowDialog())
			{
				return dialog.FileName;
			}
			else
			{
				throw new CommandCancelException();
			}
		}
	}
}
