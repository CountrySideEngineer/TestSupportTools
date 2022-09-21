using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestParser.Config
{
	public class TestCaseConfig
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public TestCaseConfig()
		{
			InputExpect = string.Empty;
			Input = string.Empty;
			Expect = string.Empty;
			ReturnValue = string.Empty;
			ReturnVariableName = string.Empty;
		}

		[XmlElement("InputExpect")]
		public string InputExpect { get; set; }

		[XmlElement("Input")]
		public string Input { get; set; }

		[XmlElement("Expect")]
		public string Expect { get; set; }

		[XmlElement("ReturnValue")]
		public string ReturnValue { get; set; }

		[XmlElement("ReturnVariableName")]
		public string ReturnVariableName { get; set; }
	}
}
