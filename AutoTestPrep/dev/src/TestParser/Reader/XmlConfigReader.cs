using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TestParser.Config;

namespace TestParser.Reader
{
	public class XmlConfigReader : IReader
	{
		/// <summary>
		/// Read configuration from path in xml file.
		/// </summary>
		/// <param name="path">Path to file in xml.</param>
		/// <returns>Configuratoin read from path.</returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="FileNotFoundException"></exception>
		/// <exception cref="DriveNotFoundException"></exception>
		/// <exception cref="IOException"></exception>
		/// <exception cref="InvalidCastException"></exception>
		public object Read(string path)
		{
			using (var stream = new StreamReader(path))
			{
				object configObj = Read(stream);
				var config = (TestParserConfig)configObj;
				return config;
			}
		}

		/// <summary>
		/// Read configuration from stream.
		/// </summary>
		/// <param name="stream">Stream to read configuration.</param>
		/// <returns>Configuration read from stream.</returns>
		/// <exception cref="InvalidOperationException"></exception>
		/// <exception cref="InvalidCastException"></exception>
		public object Read(TextReader stream)
		{
			var deserializer = new XmlSerializer(typeof(TestParserConfig));
			object obj = deserializer.Deserialize(stream);
			var config = (TestParserConfig)obj;
			return config;
		}
	}
}
