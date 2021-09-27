using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Data;
using TestParser.Target;

namespace CodeWriter.Data
{
	/// <summary>
	/// Data to write into code.
	/// </summary>
	public class WriteData
	{
		/// <summary>
		/// Configuration about code.
		/// </summary>
		public CodeConfiguration CodeConfig;

		/// <summary>
		/// Collection of test.
		/// </summary>
		public IEnumerable<Test> Tests;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public WriteData()
		{
			this.CodeConfig = new CodeConfiguration();
			this.Tests = new List<Test>();
		}
	}
}
