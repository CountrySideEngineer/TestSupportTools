using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model
{
	/// <summary>
	/// Test data model.
	/// </summary>
	public class TestData
	{
		/// <summary>
		/// Condition of test data
		/// </summary>
		public string Condition { get; set; }

		/// <summary>
		/// Description of test data.
		/// </summary>
		public string Descriotion { get; set; }

		/// <summary>
		/// Name of test data.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Test data value.
		/// </summary>
		public string Value { get; set; }
	}
}
