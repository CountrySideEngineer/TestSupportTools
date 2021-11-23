using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Command
{
	public class LoadCustomPluginCommand : LoadPluginCommand
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public LoadCustomPluginCommand() : base()
		{
			string currentDir = System.IO.Directory.GetCurrentDirectory();
			string dbPath = $@"{currentDir}\db\CustomPlugin.plugin";
			string tableName = "CustomPlugin";
			base.DbPath = dbPath;
			base.DbTableName = tableName;
		}

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="dbPath">Database file path.</param>
		/// <param name="dbTableName">Database table name.</param>
		public LoadCustomPluginCommand(string dbPath, string dbTableName) : base(dbPath, dbTableName) { }

		/// <summary>
		/// Execute load custom plugins.
		/// </summary>
		/// <param name="commandArg">Command argument data.</param>
		public override void Execute(object commandArg)
		{
			try
			{
				this.CreateDbDirectroyIfNotExists();
				base.Execute(commandArg);
			}
			catch (System.Exception ex)
			when ((ex is IOException) || (ex is SecurityException))
			{
				Debug.WriteLine(ex.StackTrace);

				throw;
			}
		}
	}
}
