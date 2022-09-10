using CSEngineer.TestSupport.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.ParserException;
using TestParser.Target;

namespace TestParser.Converter
{
	public class VariableConverter : AFunctionTableItemConverter
	{
		/// <summary>
		/// Convert variable data into "Parameter" object in Function object.
		/// </summary>
		/// <param name="src">Collection of table item to be converted.</param>
		/// <param name="dst">Function object to set converted Parameter object.</param>
		public override void Convert(IEnumerable<string> src, ref Parameter dst)
		{
			try
			{
				string dataType = src.ElementAt(3);
				if ((string.IsNullOrEmpty(dataType)) || (string.IsNullOrWhiteSpace(dataType)))
				{
					ERROR("Variable data type has not been set.");
					throw new TestParserException(TestParserException.Code.PARSER_ERROR_TEST_FUNCTION_VARIABLE_DATA_INVALID);
				}
				string name = src.ElementAt(5);
				if ((string.IsNullOrEmpty(name)) || (string.IsNullOrWhiteSpace(name)))
				{
					ERROR("Variable name has not been set.");
					throw new TestParserException(TestParserException.Code.PARSER_ERROR_TEST_FUNCTION_VARIABLE_DATA_INVALID);
				}

				dst.Prefix = src.ElementAt(2);
				dst.DataType = dataType;
				string postFix = src.ElementAt(4);
				dst.PointerNum = Util.GetPointerNum(postFix);
				dst.Name = name;
				try
				{
					dst.Description = src.ElementAt(7);
				}
				catch (ArgumentOutOfRangeException)
				{
					DEBUG($"Remarks or description about {name} has not been set, skip!");
					dst.Description = string.Empty;
				}
			}
			catch (ArgumentOutOfRangeException)
			{
				ERROR("Variable data in function table is invalid.");
				throw new TestParserException(TestParserException.Code.PARSER_ERROR_TEST_FUNCTION_VARIABLE_DATA_INVALID);
			}
		}
	}
}
