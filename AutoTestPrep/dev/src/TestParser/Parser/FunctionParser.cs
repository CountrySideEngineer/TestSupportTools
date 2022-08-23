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
using TestParser.Config;
using TestParser.Converter;
using TableReader.Excel;
using TableReader.TableData;

namespace TestParser.Parser
{
	public class FunctionParser : AParser
	{
		/// <summary>
		/// Configuration of target function table parser.
		/// </summary>
		public TargetFunctionConfig Config;

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
		/// Constructor with arguments, parsing configuration.
		/// </summary>
		/// <param name="config"></param>
		public FunctionParser(TargetFunctionConfig config) : base()
		{
			Config = config;
		}

		/// <summary>
		/// Constructor with arguments, target sheet name and parsing configuration.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="config"></param>
		public FunctionParser(string target, TargetFunctionConfig config) : base(target)
		{
			Config = config;
		}

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
			var reader = new ExcelTableReader(stream)
			{
				SheetName = this.Target
			};
			Parameter readFunction = GetFunctionInfo(reader);

			return readFunction;
		}

		/// <summary>
		/// Get function information from excel file using object <para>ExcelTableReader</para>.
		/// </summary>
		/// <param name="reader">Object to read function information from Excel.</param>
		/// <returns>Function information in Paramter object.</returns>
		protected Parameter GetFunctionInfo(ExcelTableReader reader)
		{
			Range range = GetRangeToStartReading(reader);
			Parameter function = ReadFunctionTable(reader, range);
			return function;
		}

		/// <summary>
		/// Read function data from table pointed by argument "range".
		/// </summary>
		/// <param name="reader">Excel reader object.</param>
		/// <param name="range">Range about table to read.</param>
		/// <returns>Parameter object read from table.</returns>
		protected Parameter ReadFunctionTable(ExcelTableReader reader, Range range)
		{
			IEnumerable<IEnumerable<string>> tableContent = ReadTable(reader, range);
			Parameter function = new Function();
			foreach (IEnumerable<string> rowItem in tableContent)
			{
				Items2FunctionData(rowItem, ref function);
			}
			return function;
		}

		/// <summary>
		/// Read table contents.
		/// </summary>
		/// <param name="reader">ExcelTableReader object.</param>
		/// <param name="range">Range row and col data to read.</param>
		/// <returns>Collection of items in table.</returns>
		protected IEnumerable<IEnumerable<string>> ReadTable(ExcelTableReader reader, Range range)
		{
			Range rangeToRead = new Range(range);
			List<List<string>> tableItems = new List<List<string>>();
			do
			{
				try
				{
					var readItem = reader.ReadRow(rangeToRead).ToList();
					string type = readItem.ElementAt(0);
					if ((string.IsNullOrWhiteSpace(type)) || (string.IsNullOrEmpty(type)))
					{
						INFO($"The cell in ({rangeToRead.StartRow}, {rangeToRead.StartColumn}) is empty.");
						INFO($"Stop reading Table.");

						break;
					}
					tableItems.Add(readItem);
					rangeToRead.StartRow++;
				}
				catch (ArgumentOutOfRangeException)
				{
					INFO($"An empty cell is found in function table at ({rangeToRead.StartRow}, {rangeToRead.StartColumn}).");
					INFO($"Stop reading Table.");

					break;
				}
			} while (true);

			return tableItems;
		}

		/// <summary>
		/// Get row and col to start reading table.
		/// </summary>
		/// <param name="reader">ExcelTableReader object.</param>
		/// <returns>Range to start reading function table.</returns>
		protected Range GetRangeToStartReading(ExcelTableReader reader)
		{
			Range targetFuncRange = null;
			try
			{
				if ((string.IsNullOrEmpty(Config.TableConfig.Name)) || (string.IsNullOrWhiteSpace(Config.TableConfig.Name)))
				{
					ERROR("Function table name has not been set.");
					throw new TestParserException(TestParserException.Code.TARGET_FUNCTION_TABLE_FORMAT_INVALID);
				}

				INFO($"Start getting target function table in \"{this.Target}\" sheet");
				targetFuncRange = reader.FindFirstItem(Config.TableConfig.Name);

				INFO($"    Find {Config.TableConfig.Name} in {this.Target} sheet cell at({targetFuncRange.StartRow}, {targetFuncRange.StartColumn}).");

				targetFuncRange.StartRow += (Config.TableConfig.TableTopRowOffset + Config.TableConfig.RowDataOffset);
				targetFuncRange.StartColumn += (Config.TableConfig.TableTopColOffset + Config.TableConfig.ColDataOffset);

				INFO($"The cell coordinates to start reading is ({targetFuncRange.StartRow}, {targetFuncRange.StartColumn}).");
				return targetFuncRange;
			}
			catch (NullReferenceException)
			{
				FATAL("Function parser configuration has not been set.");
				throw new TestParserException(TestParserException.Code.TEST_PARSE_FAILED);
			}
			catch (ArgumentException)
			{
				WARN($"\"{Config.TableConfig.Name}\" cell can not be found in \"{this.Target}\" sheet.");

				throw new TestParserException(TestParserException.Code.TARGET_FUNCTION_TABLE_FORMAT_INVALID);
			}
		}

		/// <summary>
		/// Convert and set table item in a row into Parameter object.
		/// </summary>
		/// <param name="items">Collection of table item in a row.</param>
		/// <param name="function">Reference to Parameter object to set converted items.</param>
		protected void Items2FunctionData(IEnumerable<string> items, ref Parameter function)
		{
			ConverterFactory factory = new ConverterFactory(Config);
			AFunctionTableItemConverter converter = factory.Create(items);
			converter.Convert(items, ref function);
		}
	}
}
