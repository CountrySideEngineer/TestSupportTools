using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
	public class CodeGeneratorException : System.Exception
	{
		/// <summary>
		/// Error code.
		/// </summary>
		public ushort ErrorCode { get; protected set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public CodeGeneratorException() : base()
		{
			this.ErrorCode = 0xFFFF;
		}

		/// <summary>
		/// Constructor with message about the exception.
		/// </summary>
		/// <param name="message">Message about exception</param>
		public CodeGeneratorException(string message) : base(message)
		{
			this.ErrorCode = 0xFFFF;
		}

		/// <summary>
		/// Constructor with error code and message.
		/// </summary>
		/// <param name="errCode">Error code about exception.</param>
		/// <param name="message">Message about exception.</param>
		public CodeGeneratorException(ushort errCode, string message = "") : base(message)
		{
			this.ErrorCode = 0xFFFF;
		}

		/// <summary>
		/// Constructor with message and inner exception about the exception.
		/// </summary>
		/// <param name="message">Message about exception</param>
		/// <param name="innerException">Inner exception</param>
		public CodeGeneratorException(string message, System.Exception innerException)
			: base(message, innerException)
		{
			this.ErrorCode = 0xFFFF;
		}
	}
}
