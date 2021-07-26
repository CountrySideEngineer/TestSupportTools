using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Command.Exception
{
	public class CommandCancelException : System.Exception
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public CommandCancelException() : base() { }

		/// <summary>
		/// Constructor with message about exception
		/// </summary>
		/// <param name="message">Message about exception.</param>
		public CommandCancelException(string message) : base(message) { }
	}
}
