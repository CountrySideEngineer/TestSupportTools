using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Reader;

namespace TestParser.Parser.Exception
{
	public class ParseDataNotFoundException : ParseException
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public ParseDataNotFoundException() { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="errCode">Error code.</param>
		public ParseDataNotFoundException(ushort errCode) : base(errCode) { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="range">Range the exception occurred.</param>
		public ParseDataNotFoundException(Range range) : base(range) { }

		/// <summary>
		/// Constructor with message
		/// </summary>
		/// <param name="message">Exception message.</param>
		public ParseDataNotFoundException(string message) : base(message) { }
	}
}
