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
		/// Default constructor.
		/// </summary>
		public TestData()
		{
			this.Condition = string.Empty;
			this.Descriotion = string.Empty;
			this.Name = string.Empty;
			this.Value = string.Empty;
		}

		/// <summary>
		/// Copy constructor.
		/// </summary>
		/// <param name="src"></param>
		public TestData(TestData src)
		{
			this.Condition = src.Condition;
			this.Descriotion = src.Descriotion;
			this.Name = src.Name;
			this.Value = src.Value;
		}

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
