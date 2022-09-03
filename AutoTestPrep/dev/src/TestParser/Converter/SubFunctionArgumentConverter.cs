using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.ParserException;
using TestParser.Target;

namespace TestParser.Converter
{
	public class SubFunctionArgumentConverter : VariableConverter
	{
		public override void Convert(IEnumerable<string> src, ref Parameter dst)
		{
			try
			{
				Parameter argument = new Parameter();
				base.Convert(src, ref argument);

				Function dstFunction = (Function)dst;
				int dstSubfunctionIndex = dstFunction.SubFunctions.Count() - 1;
				Function dstSubfunction = dstFunction.SubFunctions.ElementAt(dstSubfunctionIndex);
				dstSubfunction.Arguments = dstSubfunction.Arguments.Append(argument);
			}
			catch (TestParserException)
			{
				throw new TestParserException(TestParserException.Code.PARSER_ERROR_TEST_SUBFUNCTION_ARGUMENT_DATA_INVALID);
			}
			catch (Exception)
			{
				throw new TestParserException(TestParserException.Code.PARSER_ERROR_UNEXPECTED_ERROR_DETECTED_IN_FUNCTION_TABLE);
			}
		}
	}
}
