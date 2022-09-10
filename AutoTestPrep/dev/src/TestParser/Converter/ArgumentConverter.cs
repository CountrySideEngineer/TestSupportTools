using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.ParserException;
using TestParser.Target;

namespace TestParser.Converter
{
	public class ArgumentConverter : VariableConverter
	{
		/// <summary>
		/// Convert argument data into "Parameter" object in Function object.
		/// </summary>
		/// <param name="src">Collection of table item to be converted.</param>
		/// <param name="dst">Function object to set converted Parameter object.</param>
		public override void Convert(IEnumerable<string> src, ref Parameter dst)
		{
			try
			{
				Parameter argument = new Parameter();
				base.Convert(src, ref argument);

				string accessMode = string.Empty;
				try
				{
					accessMode = src.ElementAt(6).ToLower();
					if (((string.IsNullOrEmpty(accessMode)) || (string.IsNullOrWhiteSpace(accessMode))) ||
						("in".Equals(accessMode.ToLower())))
					{
						argument.Mode = Parameter.AccessMode.In;
					}
					else if ("out".Equals(accessMode))
					{
						argument.Mode = Parameter.AccessMode.Out;
					}
					else if ("in/out".Equals(accessMode))
					{
						argument.Mode = Parameter.AccessMode.Both;
					}
					else
					{
						throw new TestParserException(TestParserException.Code.PARSER_ERROR_TEST_FUNCTION_ARGUMENT_DATA_INVALID);
					}
				}
				catch (ArgumentOutOfRangeException)
				{
					argument.Mode = Parameter.AccessMode.In;
				}

				dst.Prefix = argument.Prefix;
				dst.DataType = argument.DataType;
				dst.PointerNum = argument.PointerNum;
				dst.Name = argument.Name;
				dst.Description = argument.Description;
				dst.Mode = argument.Mode;
			}
			catch (Exception ex)
			when ((ex is InvalidCastException) || (ex is NullReferenceException))
			{
				FATAL("Input data has been invalid.");
				throw new TestParserException(TestParserException.Code.PARSER_ERROR_UNEXPECTED_ERROR_DETECTED_IN_FUNCTION_TABLE);
			}
			catch (TestParserException)
			{
				throw new TestParserException(TestParserException.Code.PARSER_ERROR_TEST_FUNCTION_ARGUMENT_DATA_INVALID);
			}
		}
	}
}
