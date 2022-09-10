using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.ParserException;
using TestParser.Target;

namespace TestParser.Converter
{
	public class TargetFunctionArgumentConverter : VariableConverter
	{
		/// <summary>
		/// Convert argument data into "Parameter" object as argument in Function object.
		/// </summary>
		/// <param name="src">Collection of table item to be converted.</param>
		/// <param name="dst">Function object to set converted Parameter object.</param>
		public override void Convert(IEnumerable<string> src, ref Parameter dst)
		{
			try
			{
				Parameter argument = new Parameter();
				base.Convert(src, ref argument);

				Function dstFunction = (Function)dst;
				dstFunction.Arguments = dstFunction.Arguments.Append(argument);
			}
			catch (TestParserException)
			{
				throw new TestParserException(TestParserException.Code.PARSER_ERROR_TEST_FUNCTION_ARGUMENT_DATA_INVALID);
			}
			catch (Exception)
			{
				throw new TestParserException(TestParserException.Code.PARSER_ERROR_UNEXPECTED_ERROR_DETECTED_IN_FUNCTION_TABLE);
			}
		}
	}
}
