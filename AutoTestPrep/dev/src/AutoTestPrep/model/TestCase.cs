using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model
{
	/// <summary>
	/// Test data model class.
	/// </summary>
	public class TestCase
	{
		/// <summary>
		/// Input information of a test case.
		/// </summary>
		public IEnumerable<TestData> Input { get; set; }

		/// <summary>
		/// Expectation of a test case.
		/// </summary>
		public IEnumerable<TestData> Expects { get; set; }
	}
}
