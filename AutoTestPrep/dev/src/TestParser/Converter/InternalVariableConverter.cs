using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.ParserException;
using TestParser.Target;

namespace TestParser.Converter
{
	public class InternalVariableConverter : VariableConverter
	{
		/// <summary>
		/// Convert and set internal global variable data in a table to Parameter object.
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
				dstFunction.InternalVariables = dstFunction.InternalVariables.Append(parameter);
			}
			catch (TestParserException ex)
			{
				if (ex.ErrorCode.Equals(TestParserException.Code.VARIABLE_NAME_INVALID))
				{
					ERROR("\"Name\" of internal (global) variable in variable table is invalid.");
					throw new TestParserException(TestParserException.Code.INTERNAL_VARIABLE_NAME_INVALID);
				}
				else if (ex.ErrorCode.Equals(TestParserException.Code.VARIABLE_DATATYPE_INVALID))
				{
					ERROR("\"Data type\" of internal (global) variable in variable table is invalid.");
					throw new TestParserException(TestParserException.Code.INTERNAL_VARIABLE_DATATYPE_INVALID);
				}
				else if (ex.ErrorCode.Equals(TestParserException.Code.VARIABLE_TABLE_FORMAT_INVALID))
				{
					ERROR("(global) Variable in variable table is invalid.");
					throw new TestParserException(TestParserException.Code.INTERNAL_VARIABLE_TABLE_FORMAT_INVALID);
				}
				else
				{
					ERROR("Unexpected error occurred while convering internal global variable in table.");
					throw new TestParserException(TestParserException.Code.TEST_PARSE_FAILED);
				}
			}
			catch (Exception ex)
			when ((ex is InvalidCastException) || (ex is NullReferenceException))
			{
				FATAL("Unexpected error detected while converting internal global variable.");
				throw new TestParserException(TestParserException.Code.TEST_PARSE_FAILED);
			}
		}
	}
}
