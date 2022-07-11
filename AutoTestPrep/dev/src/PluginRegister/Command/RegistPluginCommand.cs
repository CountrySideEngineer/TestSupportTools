using AutoTestPrep.Command;
using AutoTestPrep.Command.Argument;
using Plugin;
using Plugin.Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginRegister.Command
{
	public class RegistPluginCommand : ACommonPluginCommand
	{
		public RegistPluginCommand() : base()
		{
			string currentDir = System.IO.Directory.GetCurrentDirectory();
			string dbPath = $@"{currentDir}\db\CustomPlugin.plugin";
			string tableName = "CustomPlugin";
			base.DbPath = dbPath;
			base.DbTableName = tableName;
		}

		/// <summary>
		/// Execute command 
		/// </summary>
		/// <param name="commandArg"></param>
		public override void Execute(object commandArg)
		{
			if (!(commandArg is PluginCommandArgument))
			{
				throw new ArgumentException();
			}
			var pluginCommandArg = commandArg as PluginCommandArgument;
			PluginInfo pluginInfo = pluginCommandArg.PluginInfo;
			this.Check(pluginInfo);
			this.Copy(pluginInfo);

			var manager = new PluginManager(this.DbPath, this.DbTableName);
			string fileName = Path.GetFileName(pluginInfo.FileName);
			var pluginInfoToRegist = new PluginInfo()
			{
				Name = pluginInfo.Name,
				FileName = fileName
			};
			manager.Regist(pluginInfoToRegist);
		}

		/// <summary>
		/// Check plugin information.
		/// </summary>
		/// <param name="pluginInfo">Plugin information.</param>
		protected void Check(PluginInfo pluginInfo)
		{
			if ((string.IsNullOrEmpty(pluginInfo.Name)) || (string.IsNullOrWhiteSpace(pluginInfo.Name)))
			{
				throw new ArgumentException();
			}

			if ((string.IsNullOrEmpty(pluginInfo.FileName)) || (string.IsNullOrWhiteSpace(pluginInfo.FileName)))
			{
				throw new ArgumentException();
			}
			FileInfo fileInfo = new FileInfo(pluginInfo.FileName);
			bool isExists = fileInfo.Exists;
			if (!isExists)
			{
				throw new FileNotFoundException();
			}
		}

		/// <summary>
		/// Copy pluing file into directory.
		/// </summary>
		/// <param name="pluginInfo">Plugin information.</param>
		protected void Copy(PluginInfo pluginInfo)
		{
			try
			{
				string pluginFilePath = pluginInfo.FileName;
				string fileName = Path.GetFileName(pluginFilePath);
				string currentDir = System.IO.Directory.GetCurrentDirectory();
				string dstPath = $@"{currentDir}\{fileName}";
				File.Copy(pluginFilePath, dstPath, true);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.StackTrace);

				throw;
			}
		}
	}
}
