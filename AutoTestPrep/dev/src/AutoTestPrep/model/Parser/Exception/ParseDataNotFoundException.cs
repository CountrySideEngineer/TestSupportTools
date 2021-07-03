using AutoTestPrep.Model.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Parser.Exception
{
	public class ParseDataNotFoundException : System.Exception
	{
		public Range Range { get; protected set; }


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="range">Range the exception occurred.</param>
		public ParseDataNotFoundException(Range range)
		{
			this.Range = range;
		}
	}
}
