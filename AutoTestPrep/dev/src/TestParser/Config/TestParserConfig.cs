using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestParser.Config
{
	/// <summary>
	/// Root class of test parser configuration.
	/// </summary>
	[XmlRoot("TestParserConfig")]
	public class TestParserConfig
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public TestParserConfig()
		{
			TestList = new TestListConfig();
			TargetFunction = new TargetFunctionConfig();
			Test = new TestConfig();
		}

		/// <summary>
		/// Configuration about list of test.
		/// </summary>
		[XmlElement("TestList")]
		public TestListConfig TestList { get; set; }

		/// <summary>
		/// Configuration about target test definition table.
		/// </summary>
		[XmlElement("TargetFunction")]
		public TargetFunctionConfig TargetFunction { get; set; }

		/// <summary>
		/// Configuration about test.
		/// </summary>
		[XmlElement("Test")]
		public TestConfig Test { get; set; }
	}

	/// <summary>
	/// Configuration of table.
	/// </summary>
	public class TableConfig
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public TableConfig()
		{
			Name = string.Empty;
			RowOffset = 0;
			ColOffset = 0;
		}

		/// <summary>
		/// Table name.
		/// </summary>
		[XmlElement("Name")]
		public string Name { get; set; }

		/// <summary>
		/// Offset of table top in row from "name" cell.
		/// </summary>
		[XmlElement("RowOffset")]
		public int RowOffset { get; set; }

		/// <summary>
		/// Offset of table top in column from "name" cell.
		/// </summary>
		[XmlElement("ColOffset")]
		public int ColOffset { get; set; }

		/// <summary>
		/// Offset to data in column from table top.
		/// </summary>
		[XmlElement("RowDataOffset")]
		public int RowDataOffset { get; set; }

		/// <summary>
		/// Offset to data in row from table top.
		/// </summary>
		[XmlElement("ColDataOffset")]
		public int ColDataOffset { get; set; }
	}

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

	/// <summary>
	/// Target function configuration.
	/// </summary>
	public class TargetFunctionConfig
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public TargetFunctionConfig()
		{
			TableConfig = new TableConfig();
		}

		/// <summary>
		/// Table configuration.
		/// </summary>
		[XmlElement("TableConfig")]
		public TableConfig TableConfig { get; set; }
	}

	/// <summary>
	/// Test configuration.
	/// </summary>
	public class TestConfig
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public TestConfig()
		{
			TableConfig = new TableConfig();
		}

		/// <summary>
		/// Tablec configuration.
		/// </summary>
		[XmlElement("TableConfig")]
		public TableConfig TableConfig { get; set; }
	}
}
