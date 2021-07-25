using AutoTestPrep.Model.InputInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Command.Argument
{
	public class ProjectCommandArgument
	{
		/// <summary>
		/// Path to output 
		/// </summary>
		public string FilePath { get; set; }

		/// <summary>
		/// Read or write data.
		/// </summary>
		public TestDataInfo LatestData { get; set; }

		/// <summary>
		/// Base test data.
		/// </summary>
		public TestDataInfo BaseData { get; set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ProjectCommandArgument()
		{
			this.FilePath = string.Empty;
			this.LatestData = new TestDataInfo();
			this.BaseData = new TestDataInfo();
		}
	}
}
