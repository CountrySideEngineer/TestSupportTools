using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestParser.ParserException
{
	public class TestParserException : Exception
	{
		/// <summary>
		/// Error code.
		/// </summary>
		public ushort ErrorCode { get; protected set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public TestParserException() : base()
		{
			this.ErrorCode = 0xFFFF;
		}

		/// <summary>
		/// Constructor with message.
		/// </summary>
		/// <param name="message">Error message</param>
		public TestParserException(string message) : base(message)
		{
			this.ErrorCode = 0xFFFF;
		}

		/// <summary>
		/// Constructor with error code and message
		/// </summary>
		/// <param name="errCode">Error code.</param>
		/// <param name="message">Error message</param>
		/// <remarks>Message is empty in default</remarks>
		public TestParserException(ushort errCode, string message = "") : base(message)
		{
			ErrorCode = errCode;
		}

		/// <summary>
		/// Constructor with error code and message.
		/// </summary>
		/// <param name="errCode">Error code</param>
		/// <param name="message">Error message</param>
		public TestParserException(Code errCode, string message = "") : base(message)
		{
			try
			{
				ErrorCode = Convert.ToUInt16(errCode);
			}
			catch (Exception)
			{
				ErrorCode = 0xFFFF;
			}
		}

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="message">Error message.</param>
		/// <param name="innerException">Inner exception.</param>
		public TestParserException(string message, Exception innerException) 
			: base(message, innerException)
		{
			this.ErrorCode = 0xFFFF;
		}

		public enum Code
		{
			FILE_CAN_NOT_OPEN = 0x1001,
			NO_TEST_LIST_SHEET,
			NO_TEST_FUNCTION_IN_LIST,
			TARGET_TEST_SHEET_NOT_FOUND,
			TARGET_FUNCTION_TABLE_FORMAT_INVALID,
			SUB_FUNCTION_TABLE_FORMAT_INVALID,
			TARGET_FUNCTION_DEFINITION_INVALID,
			SUB_FUNCTION_DEFINITION_INVALID,
			TEST_PARSE_FAILED = 0xFFFF
		};
	}
}
