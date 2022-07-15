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
		/// Default constructor.
		/// </summary>
		public FunctionParser() : base() { }

		/// <summary>
		/// Constructor with argument, target sheet name.
		/// </summary>
		/// <param name="targe">Target sheet name.</param>
		public FunctionParser(string targe) : base(targe) { }

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
			string targetFuncTag = "対象関数";
			try
			{
				//「対象関数」のセルを取得する
				INFO($"Start getting target function data in \"{this.Target}\" sheet.");
				targetFuncRange = reader.FindFirstItem(targetFuncTag);

				INFO($"Find {targetFuncTag} in {this.Target} sheet at ({targetFuncRange.StartRow}, {targetFuncRange.StartColumn}).");
			}
			catch (ArgumentException)
			{
				WARN($"\"{targetFuncRange}\" cell can not be found in \"{this.Target}\" sheet.");

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
			string itemTag = "説明";

			try
			{
				INFO($"Start getting \"{itemTag}\" about function.");

				Range rangeToRead = reader.FindFirstItemInRow(itemTag, range);

				INFO($"    Find {itemTag} in ({rangeToRead.StartRow}, {rangeToRead.StartColumn})");

				List<string> itemsInRow = reader.ReadRow(rangeToRead).ToList();
				description = itemsInRow[1];

				INFO($"    Get \"{itemTag}\" about function.");
				DEBUG($"        descriptoin : {description}");
			}
			catch (ArgumentException)
			{
				WARN($"    \"{itemTag}\" cell can not be found in \"{this.Target}\" sheet.");
				WARN("        The description set empty.");

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
			string itemTag = "関数名";
			try
			{
				INFO($"Start getting \"{itemTag}\" about function.");

				string name = string.Empty;

				Range rangeToRead = reader.FindFirstItemInRow(itemTag, range);

				INFO($"    Find {itemTag} in ({rangeToRead.StartRow}, {rangeToRead.StartColumn})");

				List<string> itemsInRow = reader.ReadRow(rangeToRead).ToList();
				name = itemsInRow[1];
				if ((string.IsNullOrEmpty(name)) || (string.IsNullOrWhiteSpace(name)))
				{
					throw new InvalidDataException();
				}

				INFO($"    Get \"{itemTag}\" about function.");
				DEBUG($"        name: {name}");

				return name;
			}
			catch (ArgumentException)
			{
				WARN($"    \"{itemTag}\" cell can not be found in \"{this.Target}\" sheet.");

				throw new FormatException();
			}
			catch (InvalidDataException)
			{
				WARN($"    Function name has not been set in range ({range.StartRow}, {range.StartColumn})");

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
			string itemTag = "データ型";
			try
			{
				INFO($"Start getting \"{itemTag}\" of function.");

				Range rangeToRead = reader.FindFirstItemInRow(itemTag, range);

				INFO($"    Find {itemTag} in ({rangeToRead.StartRow}, {rangeToRead.StartColumn})");

				List<string> items = reader.ReadRow(rangeToRead).ToList();
				var dataType = items[1];
				pointerNum = Util.GetPointerNum(dataType);
				dataTypeWithoutPointer = Util.RemovePointer(dataType);
				if ((string.IsNullOrEmpty(dataTypeWithoutPointer)) ||
					(string.IsNullOrWhiteSpace(dataTypeWithoutPointer)))
				{
					throw new InvalidDataException();
				}

				INFO($"    Get \"{itemTag}\" of the function.");
				DEBUG($"        data type   : {dataTypeWithoutPointer}");
				DEBUG($"        pointer num : {pointerNum}");

				return (dataTypeWithoutPointer, pointerNum);
			}
			catch (ArgumentException)
			{
				WARN($"\"{itemTag}\" of the function or argument can not be found in \"{this.Target}\" sheet.");
				WARN($"The data type will be \"void\"");

				throw new FormatException();
			}
			catch (InvalidDataException)
			{
				WARN($"The data typehas not been set in range ({range.StartRow}, {range.StartColumn})");

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
			string itemTag = "引数情報";
			try
			{
				INFO($"Start getting \"{itemTag}\" of function.");

				//Range argument Range
				Range argRange = reader.FindFirstItemInRow(itemTag, range);
				reader.GetMergedCellRange(ref argRange);
				argRange.StartRow++;
				argRange.RowCount--;	//Do not count the table header.

				var arguments = new List<Parameter>();
				var rangeToRead = new Range
				{
					StartRow = argRange.StartRow,
					StartColumn = argRange.StartColumn
				};

				INFO($"    Find {itemTag} in ({rangeToRead.StartRow}, {rangeToRead.StartColumn})");

				for (int index = 0; index < argRange.RowCount; index++)
				{
					Parameter argInfo = GetArgument(reader, rangeToRead);
					arguments.Add(argInfo);

					rangeToRead.StartRow++;
				}

				INFO($"Get \"{itemTag}\" of the function.");

				return arguments;
			}
			catch (ArgumentException)
			{
				ERROR($"\"{itemTag}\" of the function can not be found in \"{this.Target}\" sheet.");

				throw new FormatException();
			}
			catch (InvalidDataException)
			{
				WARN("An empty cell found while argument of function searching.");

				throw;
			}
		}

		protected Parameter GetArgument(ExcelReader reader, Range range)
		{
			string itemTag = "引数情報";

			INFO($"Start getting \"{itemTag}\" of function.");
			DEBUG($"    start row    = {range.StartRow}");
			DEBUG($"    start column = {range.StartColumn}");
			DEBUG($"    Row count    = {range.RowCount}");
			DEBUG($"    Column count = {range.ColumnCount}");

			List<string> argInfos = reader.ReadRow(range).ToList();

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

			INFO($"Get \"{itemTag}\" of function");
			DEBUG($"Argument data:");
			DEBUG($"    Name        = {argInfo.Name}");
			DEBUG($"    DataType    = {argInfo.DataType}");
			DEBUG($"    Pointer Num = {argInfo.PointerNum}");
			DEBUG($"    Description = {argInfo.Description}");
			DEBUG($"    Mode        = {argInfo.Mode.ToString()}");

			return argInfo;
		}

		/// <summary>
		/// Get sub function information.
		/// </summary>
		/// <param name="reader">Object to read data from an excel file.</param>
		/// <returns>Collection of subfunvtion.</returns>
		/// <exception cref="FormatException">No subfunction data found in excel.</exception>
		protected IEnumerable<Function> GetSubfunctions(ExcelReader reader)
		{
			string itemTag = "子関数";
			try
			{
				IEnumerable<Range> subfuncRanges = reader.FindItem(itemTag);
				var parameters = new List<Function>();
				foreach (var rangeItem in subfuncRanges)
				{
					var subfuncParam = this.GetSubfunction(reader, rangeItem);
					parameters.Add(subfuncParam);
				}

				return parameters;
			}
			catch (ArgumentException)
			{
				ERROR($"\"Subfunction\" cell can not be found in \"{this.Target}\" sheet.");

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
			string itemTag = "子関数";
			try
			{
				INFO($"Start getting \"{itemTag}\" of function.");
				DEBUG($"    start row    = {range.StartRow}");
				DEBUG($"    start column = {range.StartColumn}");
				DEBUG($"    Row count    = {range.RowCount}");
				DEBUG($"    Column count = {range.ColumnCount}");

				Function function = GetFunctionInfo(reader, range);

				INFO($"Get subfunction.");
				DEBUG($"    {function.ToString()}");

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
