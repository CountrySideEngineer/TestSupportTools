using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestParser.Config
{
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
			TestFunctionConfig = new FunctionConfig();
		}

		/// <summary>
		/// Table configuration.
		/// </summary>
		[XmlElement("TableConfig")]
		public TableConfig TableConfig { get; set; }

		[XmlElement("TestFunctionConfig")]
		public FunctionConfig TestFunctionConfig { get; set; }

		[XmlElement("SubFunctionConfig")]
		public FunctionConfig SubFunctionConfig { get; set; }

		[XmlElement("GlobalVariableConfig")]
		public GlobalVariableConfig GlobalVariableConfig { get; set; }
	}
}
