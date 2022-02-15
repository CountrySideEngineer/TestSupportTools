using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Command
{
	public abstract class ACommonPluginCommand : IPluginCommand
	{
		/// <summary>
		/// Path of database file.
		/// </summary>
		public string DbPath { get; protected set; }

		/// <summary>
		/// Name of table to access.
		/// </summary>
		public string DbTableName { get; protected set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		protected ACommonPluginCommand()
		{
			this.DbPath = string.Empty;
			this.DbTableName = string.Empty;
		}

		public abstract void Execute(object commandArg);
	}
}
