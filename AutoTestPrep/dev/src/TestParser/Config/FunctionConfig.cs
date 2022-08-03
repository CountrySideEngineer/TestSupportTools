using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestParser.Config
{
	/// <summary>
	/// Function configuration.
	/// </summary>
	public class FunctionConfig
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public FunctionConfig()
		{
			Type = string.Empty;
			Body = string.Empty;
			Argument = string.Empty;
		}

		/// <summary>
		/// Type tag content.
		/// </summary>
		[XmlElement("Type")]
		public string Type { get; set; }

		/// <summary>
		/// Body tag content.
		/// </summary>
		[XmlElement("Body")]
		public string Body { get; set; }

		/// <summary>
		/// Argument tag argument.
		/// </summary>
		[XmlElement("Argument")]
		public string Argument { get; set; }
	}
}
