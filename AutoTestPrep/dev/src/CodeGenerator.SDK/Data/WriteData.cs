using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Data;
using TestParser.Target;

namespace CodeGenerator.Data
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
		public Test Test;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public WriteData()
		{
			this.CodeConfig = new CodeConfiguration();
			this.Test = new Test();
		}
	}
}
