using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParse.Config;
using TestParser.Parser;

namespace TestParse.Parser
{
	public abstract class ATestSheetParser : AParser
	{
		/// <summary>
		/// Configuratin of parser.
		/// </summary>
		public TableConfig TableConfig { get; set; }

		/// <summary>
		/// Default constructor
		/// </summary>
		public ATestSheetParser() : base()
		{
			TableConfig = null;
		}

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="config"></param>
		public ATestSheetParser(TableConfig config) : base()
		{
			TableConfig = config;
		}

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="target"></param>
		public ATestSheetParser(string target) : base(target)
		{
			TableConfig = null;
		}
	}
}
