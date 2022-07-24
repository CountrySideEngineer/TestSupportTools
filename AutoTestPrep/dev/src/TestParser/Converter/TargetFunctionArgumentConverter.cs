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
			catch (TestParserException ex)
			{
				if (ex.ErrorCode.Equals(TestParserException.Code.VARIABLE_NAME_INVALID))
				{
					ERROR($"\"Name\" of arguemnt in test function table is invalid.");
					throw new TestParserException(TestParserException.Code.TARGET_FUNCTION_ARGUMENT_NAME_INVALID);
				}
				else if (ex.ErrorCode.Equals(TestParserException.Code.VARIABLE_DATATYPE_INVALID))
				{
					ERROR($"\"Data type\" of argument in test function table is invalid.");
					throw new TestParserException(TestParserException.Code.TARGET_FUNCTION_ARGUMENT_DATATYPE_INVALID);
				}
				else if (ex.ErrorCode.Equals(TestParserException.Code.VARIABLE_TABLE_FORMAT_INVALID))
				{
					ERROR($"Argument format in target function table is invalid.");
					throw new TestParserException(TestParserException.Code.TARGET_FUNCTION_TABLE_FORMAT_INVALID);
				}
				else
				{
					ERROR($"Unexpected error occurred while converting target function arument in table.");
					throw ex;
				}
			}
			catch (Exception ex)
			when ((ex is InvalidCastException) || (ex is NullReferenceException))
			{
				FATAL("Unexpected error detected while converting target function arguments.");
				throw new TestParserException(TestParserException.Code.TEST_PARSE_FAILED);
			}
		}
	}
}
