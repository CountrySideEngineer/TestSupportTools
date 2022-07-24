using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.ParserException;
using TestParser.Target;

namespace TestParser.Converter
{
	public class TargetFunctionConverter : FunctionConverter
	{
		/// <summary>
		/// Convert target function table content into Function object.
		/// </summary>
		/// <param name="src">Table item.</param>
		/// <param name="dst">Converted Function object.</param>
		public override void Convert(IEnumerable<string> src, ref Parameter dst)
		{
			try
			{
				base.Convert(src, ref dst);
			}
			catch (TestParserException ex)
			{
				if (ex.ErrorCode.Equals(TestParserException.Code.FUNCTION_NAME_INVALID))
				{
					ERROR($"\"Name\" in test function table is invalid.");
					throw new TestParserException(TestParserException.Code.TARGET_FUNCTION_NAME_INVALID);
				}
				else if (ex.ErrorCode.Equals(TestParserException.Code.FUNCTION_DATATYPE_INVALID))
				{
					ERROR($"\"Data type\" in test function table is invalid.");
					throw new TestParserException(TestParserException.Code.TARGET_FUNCTION_DATATYPE_INVALID);
				}
				else if (ex.ErrorCode.Equals(TestParserException.Code.FUNCTION_TABLE_FORMAT_INVALID))
				{
					ERROR($"Target function table format is invalid.");
					throw new TestParserException(TestParserException.Code.TARGET_FUNCTION_TABLE_FORMAT_INVALID);
				}
				else
				{
					throw ex;
				}
			}
		}
	}
}
