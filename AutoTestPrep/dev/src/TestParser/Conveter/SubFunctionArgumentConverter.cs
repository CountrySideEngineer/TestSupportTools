using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.ParserException;
using TestParser.Target;

namespace TestParser.Conveter
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
			catch (TestParserException ex)
			{
				if (ex.ErrorCode.Equals(TestParserException.Code.VARIABLE_NAME_INVALID))
				{
					ERROR("\"Name\" of arguemnt in test function table is invalid.");
					throw new TestParserException(TestParserException.Code.TARGET_FUNCTION_ARGUMENT_NAME_INVALID);
				}
				else if (ex.ErrorCode.Equals(TestParserException.Code.VARIABLE_DATATYPE_INVALID))
				{
					ERROR("\"Data type\" of argument in test function table is invalid.");
					throw new TestParserException(TestParserException.Code.TARGET_FUNCTION_ARGUMENT_DATATYPE_INVALID);
				}
				else if (ex.ErrorCode.Equals(TestParserException.Code.VARIABLE_TABLE_FORMAT_INVALID))
				{
					ERROR("Argument format in target function table is invalid.");
					throw new TestParserException(TestParserException.Code.TARGET_FUNCTION_TABLE_FORMAT_INVALID);
				}
				else
				{
					ERROR("Unexpected error occurred while convering sub function argument in table.");
					throw ex;
				}
			}
			catch (Exception ex)
			when ((ex is InvalidCastException) || (ex is NullReferenceException))
			{
				FATAL("Unexpected error detected while convering sub function argument in table.");
				throw new TestParserException(TestParserException.Code.TEST_PARSE_FAILED);
			}
			catch (ArgumentOutOfRangeException)
			{
				ERROR("Sub function or its argument table format has been invalid.");
				throw new TestParserException(TestParserException.Code.SUB_FUNCTION_TABLE_FORMAT_INVALID);
			}
		}
	}
}
