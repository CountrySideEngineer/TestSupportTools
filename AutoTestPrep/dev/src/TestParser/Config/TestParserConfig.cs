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

}
