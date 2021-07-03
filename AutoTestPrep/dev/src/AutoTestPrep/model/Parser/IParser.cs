using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Parser
{
	/// <summary>
	/// Interface of parser.
	/// </summary>
	public interface IParser
	{
		object Parse(string srcPath);

		object Parse(FileStream stream);
	}
}
