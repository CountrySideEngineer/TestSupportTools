using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TestParser.Reader;

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

		/// <summary>
		/// Load configuration file from default config file.
		/// </summary>
		/// <returns>Loaded configuration.</returns>
		public static TestParserConfig LoadConfig()
		{
			string configFilePath = @".\TestParserConfg.xml";
			return LoadConfig(configFilePath);
		}

		public static TestParserConfig LoadConfig(string path)
		{
			try
			{
				TestParserConfig config = null;
				var reader = new XmlConfigReader();
				config = (TestParserConfig)reader.Read(path);
				return config;
			}
			catch (System.IO.FileNotFoundException)
			{
				throw;
			}
			catch (System.Exception)
			{
				throw;
			}
		}

		public static TestParserConfig LoadDefaultConfig()
		{
			TestParserConfig config = DefaultTestParserConfigFactory.Create();
			return config;
		}
	}

}
