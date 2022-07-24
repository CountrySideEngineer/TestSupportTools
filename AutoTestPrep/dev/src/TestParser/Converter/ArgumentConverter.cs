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
		public override void Convert(IEnumerable<string> src, ref Parameter dst)
		{
			try
			{
				Parameter argument = new Parameter();
				base.Convert(src, ref argument);

				string accessMode = src.ElementAt(6).ToLower();
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
					throw new TestParserException(TestParserException.Code.TARGET_FUNCTION_ARGUMENT_IN_OUT_INVALID);
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
				throw new TestParserException(TestParserException.Code.TEST_PARSE_FAILED);
			}
		}
	}
}
