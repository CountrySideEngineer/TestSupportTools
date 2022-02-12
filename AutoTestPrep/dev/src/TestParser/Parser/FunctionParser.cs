using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSEngineer;
using CSEngineer.TestSupport.Utility;
using TestParser.Reader;
using TestParser;
using TestParser.Target;
using TestParser.ParserException;

namespace TestParser.Parser
{
	public class FunctionParser : AParser
	{
		/// <summary>
		/// Returns the function parameter data in srcPath file.
		/// </summary>
		/// <param name="srcPath">Path to function data.</param>
		/// <returns>Function information data as Parameter object.</returns>
		/// <exception cref="TestParserException">Exception detected while parsing test data.</exception>
		public override object Parse(string srcPath)
		{
			try
			{
				return this.Read(srcPath);
			}
			catch (TestParserException)
			{
				throw;
			}
			catch (System.Exception)
			{
				throw new TestParserException(TestParserException.Code.TEST_PARSE_FAILED);
			}
		}

		/// <summary>
		/// Returns the function parameter data read from stream.
		/// </summary>
		/// <param name="stream">Stream to read data from.</param>
		/// <returns>Parameter object read from stream.</returns>
		/// <exception cref="TestParserException">Exception detected while parsing test data.</exception>
		public override object Parse(Stream stream)
		{
			try
			{
				Parameter parameter = this.ReadTargetFunction(stream);

				return parameter;
			}
			catch (TestParserException)
			{
				throw;
			}
			catch (System.Exception)
			{
				throw new TestParserException(TestParserException.Code.TEST_PARSE_FAILED);
			}
		}

		/// <summary>
		/// Read function information from a file specified by argument <para>srcPath</para>.
		/// </summary>
		/// <param name="srcPath">Path to file which contains the target function information.</param>
		/// <returns>Object of function data.</returns>
		/// <exception cref="ParseException">The file <para>srcPath</para> has already been opened by other process.</exception>
		protected object Read(string srcPath)
		{
			try
			{
				using (var stream = new FileStream(srcPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
				{
					Parameter parameter = this.ReadTargetFunction(stream);
					return parameter;
				}
			}
			catch (IOException)
			{
				throw new Exception.ParseException(0x1001);
			}
		}

		/// <summary>
		/// Read target function information from stream.
		/// </summary>
		/// <param name="stream">Stream of file.</param>
		/// <returns>Parameter of target function.</returns>
		protected Parameter ReadTargetFunction(Stream stream)
		{
			var reader = new ExcelReader(stream)
			{
				SheetName = this.Target
			};
			Parameter readFunction = GetFunctionInfo(reader);

			return readFunction;
		}

		/// <summary>
		/// Get function information from excel file using object <para>ExcelReader</para>.
		/// </summary>
		/// <param name="reader">Object to read function information from Excel.</param>
		/// <returns>Function information in Paramter object.</returns>
		protected Function GetFunctionInfo(ExcelReader reader)
		{
			Function function = null;
			Range targetFuncRange = null;
			try
			{
				//「対象関数」のセルを取得する
				Logger.INFO($"Start getting target function data in \"{this.Target}\" sheet.");
				targetFuncRange = reader.FindFirstItem("対象関数");
			}
			catch (ArgumentException)
			{
				Logger.WARN($"\t\t-\t\"Target function\" cell can not be found in \"{this.Target}\" sheet.");

				throw new TestParserException(TestParserException.Code.TARGET_FUNCTION_TABLE_FORMAT_INVALID);
			}

			try
			{
				//取得したRangeを引数として、GetFunCtionInfo()を実行する
				function = this.GetFunctionInfo(reader, targetFuncRange);

				//取得したRangeを引数として、子関数情報を取得する。
				IEnumerable<Function> subFunctions = this.GetSubfunctions(reader);
				function.SubFunctions = subFunctions;
			}
			catch (ArgumentException)
			{
				function.SubFunctions = null;
			}

			return function;
		}

		/// <summary>
		/// Get function information in a area specified in range.
		/// </summary>
		/// <param name="reader">Excel reader.</param>
		/// <param name="range">Range to read from excel file.</param>
		/// <returns>Function data read from excel.</returns>
		/// <exception cref="FormatException">Function information format is invalid.</exception>
		/// <exception cref="InvalidDataException">A data is invalid, or has not been set (empty).</exception>
		protected Function GetFunctionInfo(ExcelReader reader, Range range)
		{
			try
			{
				string description = this.GetDescription(reader, range);
				string name = this.GetFunctionName(reader, range);
				int pointerNum = 0;
				string dataTypeWithoutPointer = string.Empty;
				(dataTypeWithoutPointer, pointerNum) = GetDataType(reader, range);
				IEnumerable<Parameter> arguments = this.GetArguments(reader, range);

				var function = new Function
				{
					Description = description,
					Name = name,
					DataType = dataTypeWithoutPointer,
					Arguments = arguments,
					PointerNum = pointerNum
				};
				return function;
			}
			catch (FormatException)
			{
				throw new TestParserException(TestParserException.Code.SUB_FUNCTION_TABLE_FORMAT_INVALID);
			}
			catch (InvalidDataException)
			{
				throw new TestParserException(TestParserException.Code.TARGET_FUNCTION_DEFINITION_INVALID);
			}
		}

		/// <summary>
		/// Read description about function or argument.
		/// </summary>
		/// <param name="reader">Excel reader.</param>
		/// <param name="range">Range to read.</param>
		/// <returns>Descrition.</returns>
		protected string GetDescription(ExcelReader reader, Range range)
		{
			string description = string.Empty;

			try
			{
				Range rangeToRead = reader.FindFirstItemInRow("説明", range);
				List<string> itemsInRow = reader.ReadRow(rangeToRead).ToList();
				description = itemsInRow[1];

				Logger.INFO($"\t\t-\tGet \"description\" about function ... DONE!");
			}
			catch (ArgumentException)
			{
				Logger.WARN($"\t\t-\t\"Description\" cell can not be found in \"{this.Target}\" sheet.");
				Logger.WARN("\t\t\tThe valus will be empty");

				description = string.Empty;
			}
			return description;
		}

		/// <summary>
		/// Get function name.
		/// </summary>
		/// <param name="reader">Excel reader.</param>
		/// <param name="range">Range to read.</param>
		/// <returns>Function name.</returns>
		/// <exception cref="FormatException">The sheet does not have cell contains "FunctionName".</exception>
		/// <exception cref="InvalidDataException">The "FunctionName" cell has been empty.</exception>
		protected string GetFunctionName(ExcelReader reader, Range range)
		{
			try
			{
				string name = string.Empty;

				Range rangeToRead = reader.FindFirstItemInRow("関数名", range);
				List<string> itemsInRow = reader.ReadRow(rangeToRead).ToList();
				name = itemsInRow[1];
				if ((string.IsNullOrEmpty(name)) || (string.IsNullOrWhiteSpace(name)))
				{
					throw new InvalidDataException();
				}

				Logger.INFO($"\t\t-\tGet \"description\" about function ... DONE!");

				return name;
			}
			catch (ArgumentException)
			{
				Logger.WARN($"\t\t-\t\"Function name\" cell can not be found in \"{this.Target}\" sheet.");

				throw new FormatException();
			}
			catch (InvalidDataException)
			{
				Logger.WARN($"\t\t-\tFunction name has not been set in range ({range.StartRow}, {range.StartColumn})");

				throw;
			}
		}

		/// <summary>
		/// Get data type and number of pointer, pointer level.
		/// </summary>
		/// <param name="reader">Excel reader</param>
		/// <param name="range">Range to read from excel.</param>
		/// <returns>Data type name and number of pointer.</returns>
		/// <exception cref="FormatException">The sheet does not have cell contains "DataType".</exception>
		/// <exception cref="InvalidDataException">The "DataType" cell has been empty.</exception>
		protected (string, int) GetDataType(ExcelReader reader, Range range)
		{
			int pointerNum = 0;
			string dataTypeWithoutPointer = string.Empty;
			try
			{
				Range typeRange = reader.FindFirstItemInRow("データ型", range);
				List<string> items = reader.ReadRow(typeRange).ToList();
				var dataType = items[1];
				pointerNum = Util.GetPointerNum(dataType);
				dataTypeWithoutPointer = Util.RemovePointer(dataType);
				if ((string.IsNullOrEmpty(dataTypeWithoutPointer)) ||
					(string.IsNullOrWhiteSpace(dataTypeWithoutPointer)))
				{
					throw new InvalidDataException();
				}

				Logger.INFO($"\t\t-\tGet \"data type\" of the function ... DONE!");

				return (dataTypeWithoutPointer, pointerNum);
			}
			catch (ArgumentException)
			{
				Logger.WARN($"\t\t-\t\"data type\" of the function or argument can not be found in \"{this.Target}\" sheet.");
				Logger.WARN($"\t\t\tThe data type will be \"void\"");

				throw new FormatException();
			}
			catch (InvalidDataException)
			{
				Logger.WARN($"\t\t-\tThe data typehas not been set in range ({range.StartRow}, {range.StartColumn})");

				throw;
			}
		}

		/// <summary>
		/// Get first information of argument in a range specified argument <para>range</para>.
		/// </summary>
		/// <param name="reader">Object to read data from Excel.</param>
		/// <param name="range">Range to read.</param>
		/// <returns>A list of argument in <para>Parameter</para> object.</returns>
		/// <exception cref="ArgumentException">Format of data sheet is invalid.</exception>
		/// <exception cref="InvalidDataException">The "Argument" cell has been empty.</exception>
		protected IEnumerable<Parameter> GetArguments(ExcelReader reader, Range range)
		{
			try
			{
				//Range argument Range
				Range argRange = reader.FindFirstItemInRow("引数情報", range);
				reader.GetMergedCellRange(ref argRange);
				argRange.StartRow++;
				argRange.RowCount--;	//Do not count the table header.

				var arguments = new List<Parameter>();
				for (int index = 0; index < argRange.RowCount; index++)
				{
					var rowRange = new Range
					{
						StartRow = argRange.StartRow + index,
						StartColumn = argRange.StartColumn
					};
					List<string> argInfos = reader.ReadRow(rowRange).ToList();

					/*
					 * Check argument whether parameters without description have been set.
					 * If not, it means the parameters are invalid and this function should notify error
					 * by throwing eception.
					 */
					if (((string.IsNullOrEmpty(argInfos[1])) || (string.IsNullOrWhiteSpace(argInfos[1]))) ||
						((string.IsNullOrEmpty(argInfos[2])) || (string.IsNullOrWhiteSpace(argInfos[2]))))
					{
						throw new InvalidDataException();
					}

					var dataTypeWithoutPointer = Util.RemovePointer(argInfos[2]);
					int poinuterNum = Util.GetPointerNum(argInfos[2]);
					var argInfo = new Parameter
					{
						Name = argInfos[1],
						DataType = dataTypeWithoutPointer,
						PointerNum = poinuterNum,
						Description = argInfos[3],
						Mode = Parameter.ToMode(argInfos[4])
					};
					arguments.Add(argInfo);
				}

				Logger.INFO($"\t\t-\tGet \"argument\" of the function ... DONE!");
				return arguments;
			}
			catch (ArgumentException)
			{
				Logger.ERROR($"\t\t-\t\"Argument\" of the function can not be found in \"{this.Target}\" sheet.");

				throw new FormatException();
			}
			catch (InvalidDataException)
			{
				Logger.WARN("\t\t-\tAn empty cell found while argument of function searching.");

				throw;
			}
		}

		/// <summary>
		/// Get sub function information.
		/// </summary>
		/// <param name="reader">Object to read data from an excel file.</param>
		/// <returns>Collection of subfunvtion.</returns>
		/// <exception cref="FormatException">No subfunction data found in excel.</exception>
		protected IEnumerable<Function> GetSubfunctions(ExcelReader reader)
		{
			try
			{
				IEnumerable<Range> subfuncRanges = reader.FindItem("子関数");
				var parameters = new List<Function>();
				foreach (var rangeItem in subfuncRanges)
				{
					var subfuncParam = this.GetSubfunction(reader, rangeItem);
					parameters.Add(subfuncParam);
				}

				Logger.INFO($"\t\t-\tGet \"subfunction\" datas of the function ... DONE!");
				return parameters;
			}
			catch (ArgumentException)
			{
				Logger.ERROR($"\t\t-\t\"Subfunction\" cell can not be found in \"{this.Target}\" sheet.");

				throw;
			}
			catch (TestParserException)
			{
				throw;
			}
		}

		/// <summary>
		/// Get sub function information in a range.
		/// </summary>
		/// <param name="reader">Object to read data from an excel file.</param>
		/// <param name="range">Range to read.</param>
		/// <returns>Read parameter</returns>
		/// <exception cref="TestParserException">The data has been invalid.</exception>
		protected Function GetSubfunction(ExcelReader reader, Range range)
		{
			try
			{
				Function function = GetFunctionInfo(reader, range);

				return function;
			}
			catch (FormatException)
			{
				throw new TestParserException(TestParserException.Code.SUB_FUNCTION_TABLE_FORMAT_INVALID);
			}
			catch (InvalidDataException)
			{
				throw new TestParserException(TestParserException.Code.SUB_FUNCTION_DEFINITION_INVALID);
			}
		}
	}
}
