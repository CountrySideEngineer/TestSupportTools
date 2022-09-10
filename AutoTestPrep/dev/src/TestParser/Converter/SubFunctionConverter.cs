using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.ParserException;
using TestParser.Target;

namespace TestParser.Converter
{
	public class SubFunctionConverter : FunctionConverter
	{
		/// <summary>
		/// Convert sub functin table content into Function object.
		/// </summary>
		/// <param name="src"></param>
		/// <param name="dst"></param>
		public override void Convert(IEnumerable<string> src, ref Parameter dst)
		{
			try
			{
				Parameter function = new Function();
				base.Convert(src, ref function);

				Function dstFunc = (Function)dst;
				dstFunc.SubFunctions = dstFunc.SubFunctions.Append((Function)function);
			}
			catch (TestParserException)
			{
				throw new TestParserException(TestParserException.Code.PARSER_ERROR_TEST_SUBFUNCTION_DATA_INVALID);
			}
			catch (Exception)
			{
				throw new TestParserException(TestParserException.Code.PARSER_ERROR_UNEXPECTED_ERROR_DETECTED_IN_FUNCTION_TABLE);
			}
		}
	}
}
