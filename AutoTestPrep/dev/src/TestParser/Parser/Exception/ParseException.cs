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

		public ushort ErrorCode { get; protected set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ParseException()
		{
			Range = null;
			ErrorCode = 0;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="message">Message about exception</param>
		public ParseException(string message) : base(message)
		{
			ErrorCode = 0;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="errCode"></param>
		public ParseException(ushort errCode)
		{
			ErrorCode = errCode;
		}

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
