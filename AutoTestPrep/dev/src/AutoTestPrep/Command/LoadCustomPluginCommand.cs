using System;
using System.Collections.Generic;
using System.Linq;
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
	}
}
