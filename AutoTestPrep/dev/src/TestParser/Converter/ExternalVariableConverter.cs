using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.ParserException;
using TestParser.Target;

namespace TestParser.Converter
{
	public class ExternalVariableConverter : VariableConverter
	{
		/// <summary>
		/// Convert and set external global variable data in a table to Parameter object.
		/// </summary>
		/// <param name="src">Collection of table item to be converted.</param>
		/// <param name="dst">Function object to set converted Parameter object.</param>
		public override void Convert(IEnumerable<string> src, ref Parameter dst)
		{
			try
			{
				Parameter parameter = new Parameter();
				base.Convert(src, ref parameter);

				Function dstFunction = (Function)dst;
				dstFunction.ExternalVariables = dstFunction.ExternalVariables.Append(parameter);
			}
			catch (TestParserException)
			{
				throw new TestParserException(TestParserException.Code.PARSER_ERROR_EXTERNAL_VARIABLE_DATA_INVALID);
			}
			catch (Exception)
			{
				throw new TestParserException(TestParserException.Code.PARSER_ERROR_UNEXPECTED_ERROR_DETECTED_IN_FUNCTION_TABLE);
			}
		}
	}
}
