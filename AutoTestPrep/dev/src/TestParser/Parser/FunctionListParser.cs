using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSEngineer;
using TestParser.Reader;
using TestParser.Target;
using TestParser.Config;
using TableReader.Excel;
using TableReader.TableData;
using TestParser.ParserException;
using System.Security;

namespace TestParser.Parser
{
	/// <summary>
	/// Parse function info in a test design file.
	/// </summary>
	public class FunctionListParser : AParser
	{
		/// <summary>
		/// Configuration for test list reading.
		/// </summary>
		public TestListConfig Config { get; set; }

		/// <summary>
		/// Default constructor
		/// </summary>
		public FunctionListParser() : base() { }

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="target">Function list parser sheet name in excel.</param>
		public FunctionListParser(string target) : base(target) { }

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="config"></param>
		public FunctionListParser(TestListConfig config) : base()
		{
			Config = config;
		}

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="target">Sheet name to parser.</param>
		/// <param name="config">Parser configuration.</param>
		public FunctionListParser(string target, TestListConfig config) : base(target)
		{
			Config = config;
		}

		/// <summary>
		/// Parse function information in file <para>srcPath</para>.
		/// </summary>
		/// <param name="srcPath">Path to input file.</param>
		/// <returns>Collection of functin information to parse.</returns>
		/// <exception cref="TestParserException"></exception>
		public override object Parse(string srcPath)
		{
			try
			{
				using (var stream = new FileStream(srcPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				{
					try
					{
						IEnumerable<ParameterInfo> functionInfoList = this.ReadFunctionList(stream);
						return functionInfoList;
					}
					catch (TestParserException)
					{
						throw;
					}
				}
			}
			catch (System.Exception ex)
			when (ex is ArgumentNullException)
			{
				ERROR("No test file path has been set.");
				throw new TestParserException(TestParserException.Code.PARSER_ERROR_FILE_CAN_NOT_OPEN);
			}
			catch (System.Exception ex)
			when ((ex is ArgumentException) ||
				(ex is FileNotFoundException) ||
				(ex is DirectoryNotFoundException) ||
				(ex is PathTooLongException))
			{
				ERROR($"File path {srcPath} is invalid.");
				throw new TestParserException(TestParserException.Code.PARSER_ERROR_FILE_CAN_NOT_OPEN);
			}
			catch (SecurityException)
			{
				ERROR($"File {srcPath} can not access.");
				throw new TestParserException(TestParserException.Code.PARSER_ERROR_FILE_CAN_NOT_OPEN);
			}
			catch (System.Exception ex)
			when ((ex is NotSupportedException) || (ex is ArgumentOutOfRangeException))
			{
				ERROR($"File path {srcPath} is not supported.");
				throw new TestParserException(TestParserException.Code.PARSER_ERROR_FILE_CAN_NOT_OPEN);
			}
		}

		/// <summary>
		/// Parse function information from stream <para>stream</para>.
		/// </summary>
		/// <param name="stream">FileStream of function information.</param>
		/// <returns>Function information list.</returns>
		/// <exception cref="ParseDataNotFoundException"></exception>
		public override object Parse(Stream stream)
		{
			try
			{
				IEnumerable<ParameterInfo> functionInfoList = ReadFunctionList(stream);
				return functionInfoList;
			}
			catch (TestParserException)
			{
				throw;
			}
		}

		/// <summary>
		/// Read function information from stream, FileStream object.
		/// </summary>
		/// <param name="stream">Stream object from file.</param>
		/// <returns>List of ParameterInfo object.</returns>
		/// <exception cref="TestParserException"></exception>
		protected IEnumerable<ParameterInfo> ReadFunctionList(Stream stream)
		{
			if (string.IsNullOrEmpty(Target) || (string.IsNullOrWhiteSpace(Target)))
			{
				FATAL("The name of sheet with the list of target functions is not specified.");
				throw new TestParserException(TestParserException.Code.PARSER_ERROR_TEST_FUNCTION_LIST_SHEET_NOT_FOUND);
			}
			var reader = new ExcelTableReader(stream)
			{
				SheetName = this.Target
			};
			IEnumerable<ParameterInfo> parameterInfolist = ReadFunctionInfo(reader);
			return parameterInfolist;
		}

		/// <summary>
		/// Read function informations from 
		/// </summary>
		/// <param name="reader">Object to read test data information from excel file.</param>
		/// <returns>List of function information.</returns>
		/// <exception cref="TestParserException"></exception>
		protected IEnumerable<ParameterInfo> ReadFunctionInfo(ExcelTableReader reader)
		{
			INFO($"Start getting target function list from \"{reader.SheetName}\" sheet.");

			Range tableItemRange = GetRangeToRead(reader);

			DEBUG($"Range to read;");
			DEBUG($"    Start row    = {tableItemRange.StartRow}");
			DEBUG($"    Start column = {tableItemRange.StartColumn}");
			DEBUG($"    Row count    = {tableItemRange.RowCount}");
			DEBUG($"    Column count = {tableItemRange.ColumnCount}");

			var infoList = ReadFunctionInfo(reader, tableItemRange);
			return infoList;
		}

		/// <summary>
		/// Read function information to read.
		/// </summary>
		/// <param name="reader">ExcelTableReader object.</param>
		/// <param name="range">Range to read.</param>
		/// <returns>Read function informations.</returns>
		/// <exception cref="TestParserException"></exception>
		public IEnumerable<ParameterInfo> ReadFunctionInfo(ExcelTableReader reader, Range range)
		{
			var rangeToRead = new Range(range);
			var parameterInfoList = new List<ParameterInfo>();
			for (int index = 0; index < rangeToRead.RowCount; index++)
			{
				try
				{
					ParameterInfo parameterInfo = this.ReadFunctionInfoItem(reader, rangeToRead);
					parameterInfoList.Add(parameterInfo);
				}
				catch (TestParserException ex)
				{
					if (ex.ErrorCode.Equals(TestParserException.Code.PARSER_ERROR_UNEXPECTED_ERROR_DETECTED_IN_FUNCTION_TABLE))
					{
						throw ex;
					}
					WARN($"Skip reading row {range.StartRow} because an invalid data has been set.");
				}
				rangeToRead.StartRow++;
			}

			return parameterInfoList;
		}

		/// <summary>
		/// Get range t to read.
		/// </summary>
		/// <param name="reader">ExcelTableReader object</param>
		/// <returns>Range to read to get function information.</returns>
		protected Range GetRangeToRead(ExcelTableReader reader)
		{
			try
			{
				Range tableNameRange = reader.FindFirstItem(Config.TableConfig.Name);

				INFO($"    Find \"{Config.TableConfig.Name}\" in \"{reader.SheetName}\" sheet at ({tableNameRange.StartRow}, {tableNameRange.StartColumn}).");

				Range tableEndRange = new Range();
				reader.GetRowRange(ref tableEndRange);
				reader.GetColumnRange(ref tableEndRange);

				int startRow = tableNameRange.StartRow + Config.TableConfig.TableTopRowOffset + Config.TableConfig.RowDataOffset;
				int startColumn = tableNameRange.StartColumn + Config.TableConfig.TableTopColOffset + Config.TableConfig.ColDataOffset;
				int lastRow = tableEndRange.StartRow + tableEndRange.RowCount - 1;
				int lastColumn = tableEndRange.StartColumn + tableEndRange.ColumnCount - 1;
				int rowCount = lastRow - (startRow - 1);
				int columnCount = lastColumn - (startColumn - 1);

				if ((startRow < 1) ||
					(startColumn < 1) ||
					(lastRow < 1) || (lastRow < startRow) ||
					(lastColumn < 1) || (lastColumn < startColumn) ||
					(rowCount < 1) ||
					(columnCount < 1))
				{
					ERROR("Function list sheet or table format invalid.");
					throw new TestParserException(TestParserException.Code.PARSER_ERROR_INVALID_DATA_INPUT_IN_TEST_FUNCTION_TABLE);
				}

				Range rangeToRead = new Range()
				{
					StartRow = startRow,
					StartColumn = startColumn,
					RowCount = rowCount,
					ColumnCount = columnCount,
				};
				return rangeToRead;
			}
			catch (ArgumentException)
			{
				ERROR($"The table can not found in {reader.SheetName}.");
				throw new TestParserException(TestParserException.Code.PARSER_ERROR_TEST_FUNCTION_TABLE_NOT_FOUND);
			}
			catch (InvalidDataException)
			{
				FATAL("No object has no been set.");

				throw new TestParserException(TestParserException.Code.PARSER_ERROR_UNEXPECTED_ERROR_DETECTED_IN_FUNCTION_TABLE);
			}
		}

		/// <summary>
		/// Read function information 
		/// </summary>
		/// <param name="reader">Object to read test data information from excel file.</param>
		/// <param name="range">Range to read.</param>
		/// <returns><para>ParameterInfo</para> object read from excel.</returns>
		/// <exception cref="TestParserException"></exception>
		protected ParameterInfo ReadFunctionInfoItem(ExcelTableReader reader, Range range)
		{
			IEnumerable<string> rowItem = reader.ReadRow(range);
			if (0 == rowItem.Count())
			{
				WARN($"No item found in row ({range.StartRow}).");
				throw new TestParserException(TestParserException.Code.PARSER_ERROR_INVALID_DATA_INPUT_IN_TEST_FUNCTION_TABLE);
			}
			foreach (var item in rowItem)
			{
				if ((string.IsNullOrWhiteSpace(item)) || (string.IsNullOrEmpty(item)))
				{
					WARN("Invalid data found in function list.");
					throw new TestParserException(TestParserException.Code.PARSER_ERROR_INVALID_DATA_INPUT_IN_TEST_FUNCTION_TABLE);
				}
			}

			try
			{
				ParameterInfo parameterInfo = new ParameterInfo();
				parameterInfo.Index = Convert.ToInt32(rowItem.ElementAt(0));
				parameterInfo.Name = rowItem.ElementAt(1);
				parameterInfo.InfoName = rowItem.ElementAt(2);
				parameterInfo.FileName = rowItem.ElementAt(3);

				INFO($"Function table info:");
				INFO($"    Index    = {parameterInfo.Index}");
				INFO($"    Name     = {parameterInfo.Name}");
				INFO($"    InfoName = {parameterInfo.InfoName}");
				INFO($"    FileName = {parameterInfo.FileName}");

				return parameterInfo;
			}
			catch (FormatException)
			{
				ERROR("Invalid data found in function table.");
				throw new TestParserException(TestParserException.Code.PARSER_ERROR_INVALID_DATA_INPUT_IN_TEST_FUNCTION_TABLE);
			}
		}
	}
}
