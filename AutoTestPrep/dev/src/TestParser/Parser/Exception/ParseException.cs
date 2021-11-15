using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Reader;

namespace TestParser.Parser.Exception
{
	/// <summary>
	/// Exception while parsing test information.
	/// </summary>
	public class ParseException : System.Exception
	{
		public Range Range { get; protected set; }


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="message">Message about exception</param>
		public ParseException(string message) : base(message) { }

		/// <summary>
		/// Constructor with argument about range.
		/// </summary>
		/// <param name="range"></param>
		public ParseException(Range range) : base()
		{
			this.Range = range;
		}
	}
}
