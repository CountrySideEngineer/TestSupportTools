using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Target;

namespace TestParser.Data
{
	/// <summary>
	/// Test model class.
	/// </summary>
	public class Test
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public Test()
		{
			this.Target = null;
			this.TestCases = null;
			this.Name = string.Empty;
			this.TestInformation = string.Empty;
			this.SourcePath = string.Empty;
		}

		/// <summary>
		/// Test target function class.
		/// </summary>
		public Function Target { get; set; }

		/// <summary>
		/// List of test data class.
		/// </summary>
		public IEnumerable<TestCase> TestCases { get; set; }

		/// <summary>
		/// Test name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// About test
		/// </summary>
		public string TestInformation { get; set; }

		/// <summary>
		/// Path to file to test.
		/// </summary>
		public string SourcePath { get; set; }
	}
}
