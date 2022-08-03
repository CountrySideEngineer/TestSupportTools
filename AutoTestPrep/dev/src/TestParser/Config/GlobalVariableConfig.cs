using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestParser.Config
{
	public class GlobalVariableConfig
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public GlobalVariableConfig()
		{
			Type = string.Empty;
			External = string.Empty;
			Internal = string.Empty;
		}

		[XmlElement("Type")]
		public string Type { get; set; }

		[XmlElement("External")]
		public string External { get; set; }

		[XmlElement("Internal")]
		public string Internal { get; set; }
	}
}
