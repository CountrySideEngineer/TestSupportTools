using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestParser.Config
{
	public class FunctionConfig
	{
		[XmlElement("Type")]
		public string Type { get; set; }

		[XmlElement("Body")]
		public string Body { get; set; }

		[XmlElement("Argument")]
		public string Argument { get; set; }
	}
}
