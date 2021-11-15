using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestParser
{
	/// <summary>
	/// Interface of parser.
	/// </summary>
	public interface IParser
	{
		/// <summary>
		/// Interface of test parser.
		/// </summary>
		/// <param name="srcPath">Path to file to parse.</param>
		/// <returns>Result of parse.</returns>
		object Parse(string srcPath);

		/// <summary>
		/// Interface of test parser.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		object Parse(Stream stream);
	}
}
