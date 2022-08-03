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
	/// <summary>
	/// Convert table item into Function object.
	/// </summary>
	public class FunctionConverter : AFunctionTableItemConverter
	{
		/// <summary>
		/// Convert function table content into Function object.
		/// </summary>
		/// <param name="src">Table item</param>
		/// <param name="dst">Reference to Function object to set converted.</param>
		public override void Convert(IEnumerable<string> src, ref Parameter dst)
		{
			try
			{
				string dataType = src.ElementAt(3);
				if ((string.IsNullOrEmpty(dataType)) || (string.IsNullOrWhiteSpace(dataType)))
				{
					throw new TestParserException(TestParserException.Code.FUNCTION_NAME_INVALID);
				}

				string name = src.ElementAt(5);
				if ((string.IsNullOrEmpty(name)) || (string.IsNullOrWhiteSpace(name)))
				{
					throw new TestParserException(TestParserException.Code.FUNCTION_DATATYPE_INVALID);
				}

				dst.Prefix = src.ElementAt(2);
				dst.DataType = dataType;
				string postFix = src.ElementAt(4);
				dst.PointerNum = Util.GetPointerNum(postFix);
				dst.Name = name;
				dst.Description = src.ElementAt(7);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new TestParserException(TestParserException.Code.FUNCTION_TABLE_FORMAT_INVALID);
			}
		}
	}
}
