using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.TestParser.Parser
{
	public abstract class AParser : IParser
	{
		/// <summary>
		/// Target name to parse.
		/// </summary>
		public string Target { get; set; }

		/// <summary>
		/// Abstract function to read function.
		/// </summary>
		/// <param name="path">Paht to file designing test.</param>
		/// <returns>Object about test.</returns>
		public abstract object Parse(string path);

		/// <summary>
		/// Abstract function to read function.
		/// </summary>
		/// <param name="path">Stream to read from data to parse.</param>
		/// <returns>Object about test.</returns>
		public abstract object Parse(FileStream stream);
	}
}
