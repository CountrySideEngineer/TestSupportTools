using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestParser.Config
{
	public class TestCaseTableConfig : TableConfig
	{
		/// <summary>
		/// Offset to test case top in row.
		/// </summary>
		[XmlElement("TestCaseRowOffset")]
		public int TestCaseRowOffset { get; set; }

		/// <summary>
		/// Offset to test case top in column.
		/// </summary>
		[XmlElement("TestCaseColOffset")]
		public int TestCaseColOffset { get; set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public TestCaseTableConfig() : base()
		{
			TestCaseRowOffset = 0;
			TestCaseColOffset = 0;
		}
	}
}
