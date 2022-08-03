using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Config;
using TestParser.Parser;

namespace TestParser.Parser
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

		/// <summary>
		/// Constructror with argument.
		/// </summary>
		/// <param name="target">Target sheet name.</param>
		/// <param name="config">Configuration of test.</param>
		public ATestSheetParser(string target, TableConfig config) : base(target)
		{
			TableConfig = config;
		}
	}
}
