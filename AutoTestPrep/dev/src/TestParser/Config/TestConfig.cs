using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestParser.Config
{
	/// <summary>
	/// Test configuration.
	/// </summary>
	public class TestConfig
	{
		/// <summary>
		/// Tablec configuration.
		/// </summary>
		[XmlElement("TableConfig")]
		public TestCaseTableConfig TableConfig { get; set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public TestConfig()
		{
			TableConfig = new TestCaseTableConfig();
		}

	}
}
