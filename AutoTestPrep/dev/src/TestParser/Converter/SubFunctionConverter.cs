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
			catch (TestParserException ex)
			{
				if (ex.ErrorCode.Equals(TestParserException.Code.FUNCTION_NAME_INVALID))
				{
					ERROR("\"Name\" in sub function table is invalid.");
					throw new TestParserException(TestParserException.Code.SUB_FUNCTION_NAME_INVALID);
				}
				else if (ex.ErrorCode.Equals(TestParserException.Code.FUNCTION_DATATYPE_INVALID))
				{
					ERROR("\"Data type\" in sub function table is invalid.");
					throw new TestParserException(TestParserException.Code.SUB_FUNCTION_DATATYPE_INVALID);
				}
				else if (ex.ErrorCode.Equals(TestParserException.Code.FUNCTION_TABLE_FORMAT_INVALID))
				{
					ERROR("Sub functoin table format is invalid.");
					throw new TestParserException(TestParserException.Code.SUB_FUNCTION_TABLE_FORMAT_INVALID);
				}
				else
				{
					throw ex;
				}
			}
			catch (Exception ex)
			when ((ex is InvalidCastException) || (ex is NullReferenceException))
			{
				FATAL("Unexpected error detected while converting subfunction table data.");
				throw new TestParserException(TestParserException.Code.TEST_PARSE_FAILED);
			}
		}
	}
}
