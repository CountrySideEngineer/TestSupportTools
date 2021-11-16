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
		public long ErrorCode { get; protected set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public CodeGeneratorException() : base()
		{
			this.ErrorCode = -1;
		}

		/// <summary>
		/// Constructor with message about the exception.
		/// </summary>
		/// <param name="message">Message about exception</param>
		public CodeGeneratorException(string message) : base(message)
		{
			this.ErrorCode = -1;
		}

		/// <summary>
		/// Constructor with message and inner exception about the exception.
		/// </summary>
		/// <param name="message">Message about exception</param>
		/// <param name="innerException">Inner exception</param>
		public CodeGeneratorException(string message, System.Exception innerException)
			: base(message, innerException)
		{
			this.ErrorCode = -1;
		}

		/// <summary>
		/// Constructor with error code.
		/// </summary>
		/// <param name="errCode">Error code about exception.</param>
		public CodeGeneratorException(long errCode) : base()
		{
			this.ErrorCode = errCode;
		}
	}
}
