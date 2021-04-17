using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model
{
	/// <summary>
	/// Test model class.
	/// </summary>
	public class Test
	{
		/// <summary>
		/// Test target function class.
		/// </summary>
		public Parameter Target { get; set; }

		/// <summary>
		/// List of test data class.
		/// </summary>
		public IEnumerable<TestCase> TestCases { get; set; }
	}
}
