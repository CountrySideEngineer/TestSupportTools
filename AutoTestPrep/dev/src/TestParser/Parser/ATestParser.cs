using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestParser.Parser
{
	public abstract class ATestParser : IParser
	{
		/// <summary>
		/// Delegate to notify progress of parsing test.
		/// </summary>
		/// <param name="stage">Parse stage name.</param>
		/// <param name="messgae">Message</param>
		/// <param name="numerator">Progress numerator</param>
		/// <param name="denominator">Progress denominator</param>
		public delegate void NotifyParseProgress(int numerator, int denominator);
		public NotifyParseProgress NotifyParseProgressDelegate;

		/// <summary>
		/// Parser to read function list.
		/// </summary>
		public AParser FunctionListParser { get; set; }

		/// <summary>
		/// Parser to read function.
		/// </summary>
		public AParser FunctionParser { get; set; }

		/// <summary>
		/// Parser to read test cases.
		/// </summary>
		public AParser TestCaseParser { get; set; }

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
		public abstract object Parse(Stream stream);
	}
}
