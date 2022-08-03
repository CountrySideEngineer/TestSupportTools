using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestParser.Config
{
	/// <summary>
	/// Test list configuraton.
	/// </summary>
	public class TestListConfig
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public TestListConfig()
		{
			SheetName = string.Empty;
			TableConfig = new TableConfig();
		}

		/// <summary>
		/// Test sheet name.
		/// </summary>
		[XmlElement("SheetName")]
		public string SheetName { get; set; }

		/// <summary>
		/// Table configuration.
		/// </summary>
		[XmlElement("TableConfig")]
		public TableConfig TableConfig { get; set; }
	}
}
