using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace gtest2html
{
	/// <summary>
	/// Contains result of test
	/// </summary>
	public class TestResult
	{
		#region Properties
		[XmlElement("name")]
		public string Name { get; set; }

		[XmlElement("status")]
		public string Status { get; set; }

		[XmlElement("reuslt")]
		public string result { get; set; }

		[XmlElement("time")]
		public string Time { get; set; }

		[XmlElement("timestamp")]
		public string TimeStamp { get; set; }

		[XmlElement("ClassName")]
		public string ClassName { get; set; }
		#endregion
	}
}
