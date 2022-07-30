using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSEngineer;
using TestParser.Parser;
using TestParser.Reader;
using TestParser.Data;
using TestParser.Config;
using TestParser.ParserException;

namespace TestParser.Parser
{
	public class TestCaseParser : AParser
	{
		public TestConfig Config;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public TestCaseParser() : base()
		{
			Config = null;
		}

		/// <summary>
		/// Constructor with argument about sheet name to parse.
		/// </summary>
		/// <param name="target">Sheet name the test case are defined.</param>
		public TestCaseParser(string target) : base(target) { }

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="config">Parser configuration.</param>
		public TestCaseParser(TestConfig config) : base()
		{
			Config = config;
		}

		/// <summary>
		/// Constructor with arguments.
		/// </summary>
		/// <param name="target">Sheet name the test case are defined.</param>
		/// <param name="config">Parser configuration.</param>
		public TestCaseParser(string target, TestConfig config) : base(target)
		{
			Config = config;
		}

		/// <summary>
		/// Range of test data table top.
		/// </summary>
		private Range TableTop { get; set; }

		/// <summary>
		/// Range of input data.
		/// </summary>
		private Range InputRange { get; set; }

		/// <summary>
		/// Range of output data.
		/// </summary>
		private Range OutputRange { get; set; }

		/// <summary>
		/// All input data defined in test design table.
		/// </summary>
		private IEnumerable<TestData> AllInputData { get; set; }

		/// <summary>
		/// All output data defined in test design table.
		/// </summary>
		private IEnumerable<TestData> AllOutputData { get; set; }

		/// <summary>
		/// List of input data names.
		/// </summary>
		private IEnumerable<string> InputNames { get; set; }

		/// <summary>
		/// List of input data values.
		/// </summary>
		private IEnumerable<string> InputValues { get; set; }

		/// <summary>
		/// List of output data names.
		/// </summary>
		private IEnumerable<string> OutputNames { get; set; }

		/// <summary>
		/// List of output data values.
		/// </summary>
		private IEnumerable<string> OutputValues { get; set; }

		/// <summary>
		/// Returns the test data, test case data, input and expect value, 
		/// </summary>
		/// <param name="srcPath"></param>
		/// <returns></returns>
		public override object Parse(string srcPath)
		{
			return this.Read(srcPath);
		}

		/// <summary>
		/// Read the test data, test case data, input and expect value from stream <para>stream</para>.
		/// </summary>
		/// <param name="stream">Stream to test data.</param>
		/// <returns>Collection of test case.</returns>
		public override object Parse(Stream stream)
		{
			return this.ReadTestCase(stream);
		}

		/// <summary>
		/// Read test case data from file specified by argument <para>srcPath</para>
		/// </summary>
		/// <param name="srcPath">Path to file contains test case data.</param>
		/// <returns>List of test case in TestCase object.</returns>
		/// <exception cref="IOException">The file <para>srcPath</para> has already been opened by other process.</exception>
		protected object Read(string srcPath)
		{
			try
			{
				using (var stream = new FileStream(srcPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				{
					IEnumerable<TestCase> testCases = this.ReadTestCase(stream);
					return testCases;
				}
			}
			catch (IOException)
			{
				throw;
			}
		}

		/// <summary>
		/// Read test case data from <para>reader</para>, the object to read excel file.
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public object Read(ExcelReader reader)
		{
			IEnumerable<TestCase> testCases = this.ReadTestCase(reader);

			return testCases;
		}

		/// <summary>
		/// Read test case data from Stream object.
		/// </summary>
		/// <param name="stream">Stream of test case file.</param>
		/// <returns>List of test case in TestCase object.</returns>
		protected IEnumerable<TestCase> ReadTestCase(Stream stream)
		{
			var reader = new ExcelReader(stream)
			{
				SheetName = this.Target
			};
			IEnumerable<TestCase> testCases = this.ReadTestCase(reader);

			return testCases;
		}

		/// <summary>
		/// Read test case from reader.
		/// </summary>
		/// <param name="reader">Object to read data from excel.</param>
		/// <returns>List of test case.</returns>
		protected IEnumerable<TestCase> ReadTestCase(ExcelReader reader)
		{
			Range range = GetRangeToStartReadingTableHeader(reader);
			IEnumerable<TestData> testInputsAndExpects = ReadInputsAndExpects(reader, range);
			Range rangeApplied = GetRangeToStartReadingTestCase(reader);
			rangeApplied.RowCount = testInputsAndExpects.Count();
			IEnumerable<IEnumerable<string>> applied = ReadAppliedDatas(reader, rangeApplied);

			List<TestCase> testCases = new List<TestCase>();
			foreach (var appliedItem in applied)
			{
				(IEnumerable<TestData> inputs, IEnumerable<TestData> expects) = 
					ExtractInputsAndExpects(testInputsAndExpects, appliedItem);
				var testCase = new TestCase()
				{
					Input = inputs,
					Expects = expects
				};
				testCases.Add(testCase);
			}
			return testCases;
		}

		/// <summary>
		/// Get the coordinates of the title cell with a Range object.
		/// </summary>
		/// <param name="reader">Excel reader object.</param>
		/// <returns>The coordinate of the title cell in Range object.</returns>
		protected Range GetTableTitle(ExcelReader reader)
		{
			try
			{
				Range tableRange = null;
				string tableName = Config.TableConfig.Name;
				if ((string.IsNullOrEmpty(tableName)) || (string.IsNullOrWhiteSpace(tableName)))
				{
					ERROR("Test case table has not been set in configuration file.");
					throw new TestParserException(TestParserException.Code.TEST_PARSE_FAILED);
				}

				INFO($"Start getting target function table in \"{this.Target}\" sheet.");
				tableRange = reader.FindFirstItem(Config.TableConfig.Name);

				INFO($"    Find \"{tableName}\" in {this.Target} sheet cell at ({tableRange.StartRow}, {tableRange.StartColumn}).");

				return tableRange;
			}
			catch (NullReferenceException)
			{
				FATAL("Test case parser configuration has not been set.");
				throw new TestParserException(TestParserException.Code.TEST_PARSE_FAILED);
			}
			catch (ArgumentException)
			{
				WARN($"\"{Config.TableConfig.Name}\" cell can not be found in \"{this.Target}\" sheet.");
				throw new TestParserException(TestParserException.Code.TEST_CASE_TABLE_NOT_FOUND);
			}
		}

		/// <summary>
		/// Get the starting coordinate of the test case header with a Range object.
		/// </summary>
		/// <param name="reader">Excel reader</param>
		/// <returns>The starting coordinate of the test case header with a Range object.</returns>
		protected Range GetRangeToStartReadingTableHeader(ExcelReader reader)
		{
			try
			{
				Range tableTopRange = GetTableTitle(reader);
				tableTopRange.StartRow += (Config.TableConfig.TableTopRowOffset + Config.TableConfig.RowDataOffset);
				tableTopRange.StartColumn += (Config.TableConfig.TableTopColOffset + Config.TableConfig.ColDataOffset);

				INFO($"    The cell corrdinates to start reading test table header is ({tableTopRange.StartRow}, {tableTopRange.StartColumn}).");
				return tableTopRange;
			}
			catch (System.Exception)
			{
				throw;

			}
		}

		/// <summary>
		/// Get the starting coordinate of the test case with a Range object.
		/// </summary>
		/// <param name="reader">Excel reader.</param>
		/// <returns>The starting coordinate of the test case with a Range object.</returns>
		protected Range GetRangeToStartReadingTestCase(ExcelReader reader)
		{
			Range tableTop = GetTableTitle(reader);
			tableTop.StartRow += Config.TableConfig.TableTopRowOffset;
			tableTop.StartColumn += (Config.TableConfig.TableTopColOffset + Config.TableConfig.TestCaseColOffset);

			INFO($"    The cell corrdinates to start reading test table header is ({tableTop.StartRow}, {tableTop.StartColumn}).");

			return tableTop;
		}

		/// <summary>
		/// Get the starting coordinate of the test case offset from table top with a Range object.
		/// </summary>
		/// <param name="reader">Excel reader.</param>
		/// <param name="tableTopRange">Table top position as a Range.</param>
		/// <returns>The starting coordinate of the test case with a Range object.</returns>
		protected Range GetRangeToStartReadingTestCase(ExcelReader reader, Range tableTopRange)
		{
			Range tableRange = new Range(tableTopRange);
			tableRange.StartColumn += Config.TableConfig.TestCaseColOffset;

			INFO($"    The cell corrdinates to start reading test case is ({tableRange.StartRow}, {tableRange.StartColumn}).");

			return tableRange;
		}

		/// <summary>
		/// Get the starting corrdinate of the test inputs and expects data with a Range object.
		/// </summary>
		/// <param name="reader">Excel reader.</param>
		/// <param name="range">Range to read.</param>
		/// <returns></returns>
		protected IEnumerable<TestData> ReadInputsAndExpects(ExcelReader reader, Range range)
		{
			IEnumerable<IEnumerable<string>> items = ReadInputAndExpectItem(reader, range);
			List<TestData> testDataItems = new List<TestData>();
			foreach (var item in items)
			{
				TestData testData = Items2InAndExpectTestData(item);
				testDataItems.Add(testData);
			}
			return testDataItems;
		}

		/// <summary>
		/// Read inputs and expect data.
		/// </summary>
		/// <param name="reader">Excel reader.</param>
		/// <param name="range">Range to read.</param>
		/// <returns>Inputs and expects.</returns>
		protected IEnumerable<IEnumerable<string>> ReadInputAndExpectItem(ExcelReader reader, Range range)
		{
			Range rangeToRead = new Range(range);
			List<List<string>> tableItems = new List<List<string>>();
			do
			{
				try
				{
					var readItem = reader.ReadRow(rangeToRead).ToList();
					string inOrExpect = readItem.ElementAt(0);
					if ((string.IsNullOrEmpty(inOrExpect)) || (string.IsNullOrWhiteSpace(inOrExpect)))
					{
						break;
					}
					tableItems.Add(readItem);
					rangeToRead.StartRow++;
				}
				catch (ArgumentOutOfRangeException)
				{
					INFO($"An empty cell is found in ({rangeToRead.StartRow}, {rangeToRead.StartColumn}).");
					INFO($"Stop reading table.");

					break;
				}
			} while (true);

			return tableItems;
		}

		/// <summary>
		/// Read test case from excel file.
		/// </summary>
		/// <param name="apply">Data of a test case which test datas are applied to test.</param>
		/// <param name="allTestDatas">All test data designed.</param>
		/// <returns>Applied test datas.</returns>
		protected IEnumerable<TestData> ReadTestCase(IEnumerable<string> apply, IEnumerable<TestData> allTestDatas)
		{
			var appliedTestDatas = new List<TestData>();
			for (int index = 0; index < apply.Count(); index++)
			{
				if (("A").Equals(apply.ElementAt(index), StringComparison.Ordinal))
				{
					var testData = new TestData(allTestDatas.ElementAt(index));
					appliedTestDatas.Add(testData);

					DEBUG($"Applied test data:");
					DEBUG($"    Name  = {testData.Name}");
					DEBUG($"    Value = {testData.Value}");
				}
			}
			return appliedTestDatas;
		}

		/// <summary>
		/// Convert collection of test item headers (input and expect) to TestDataObject.
		/// </summary>
		/// <param name="items">Collection of items to be converted.</param>
		/// <returns>TestData object converted from items.</returns>
		protected TestData Items2InAndExpectTestData(IEnumerable<string> items)
		{
			string condition = items.ElementAt(0);
			if ((string.IsNullOrEmpty(condition)) ||
				(string.IsNullOrWhiteSpace(condition)))
			{
				ERROR($"\"INPUT\" or \"EXPECT\" has not been set.");
				throw new TestParserException(TestParserException.Code.TEST_CASE_IN_OUT_FORMAT_INVALID);
			}
			string description = items.ElementAt(1);
			string name = items.ElementAt(2);
			if ((string.IsNullOrEmpty(name)) ||
				(string.IsNullOrWhiteSpace(name)))
			{
				ERROR($"Variable name has not been set."); ;
				throw new TestParserException(TestParserException.Code.TEST_CASE_VARIABLE_NAME_INVALID);
			}
			string representativeValue = items.ElementAt(4);
			if ((string.IsNullOrEmpty(representativeValue)) ||
				(string.IsNullOrWhiteSpace(representativeValue)))
			{
				ERROR($"Variable name or representative value has not been set.");
				throw new TestParserException(TestParserException.Code.TEST_CASE_TEST_VALUE_INVALID);
			}
			DEBUG("Get test data below:");
			DEBUG($"      Condition : {condition}");
			DEBUG($"           Name : {name}");
			DEBUG($"    Descriotion : {description}");
			DEBUG($"          Value : {representativeValue}");
			TestData testData = new TestData()
			{
				Condition = condition,
				Name = name,
				Descriotion = description,
				Value = representativeValue,
			};
			return testData;
		}

		/// <summary>
		/// Get about the which inputs and expects are applied as a test case.
		/// </summary>
		/// <param name="reader">Excel reader.</param>
		/// <param name="range">Range to read.</param>
		/// <returns></returns>
		protected IEnumerable<IEnumerable<string>> ReadAppliedDatas(ExcelReader reader, Range range)
		{
			Range rangeToRead = new Range(range);
			List<List<string>> items = new List<List<string>>();
			do
			{
				try
				{
					List<string> testCaseApply = ReadAppliedDataItem(reader, rangeToRead).ToList();
					string testCaseNo = testCaseApply[0];
					if ((string.IsNullOrEmpty(testCaseNo)) || (string.IsNullOrWhiteSpace(testCaseNo)))
					{
						INFO($"The cell ({rangeToRead.StartRow}, {rangeToRead.StartColumn}) is empty.");
						INFO($"Stop reading table.");

						break;
					}
					//Remove item as header in table.
					List<string> testCase = testCaseApply.GetRange(1, testCaseApply.Count() - 1);
					items.Add(testCase);
					rangeToRead.StartColumn++;
				}
				catch (ArgumentOutOfRangeException)
				{
					INFO($"An empty cell is found while reading test case at ({rangeToRead.StartRow}, {rangeToRead.StartColumn}).");
					INFO($"Stop reading table.");

					break;
				}

			} while (true);
			return items;
		}

		/// <summary>
		/// Read applied item data as a collection.
		/// </summary>
		/// <param name="reader">Excel reader.</param>
		/// <param name="range">Range to read.</param>
		/// <returns>Collectio of test case applied.</returns>
		protected IEnumerable<string> ReadAppliedDataItem(ExcelReader reader, Range range)
		{
			IEnumerable<string> item = reader.ReadColumn(range);
			return item;
		}

		/// <summary>
		/// Extract input and expects applied as a test case.
		/// </summary>
		/// <param name="inputAndExpects">Collection of test data as a test case.</param>
		/// <param name="appliedItems">Collection of applied code.</param>
		/// <returns>Inputs and expects of test case in tuple.</returns>
		protected (IEnumerable<TestData>, IEnumerable<TestData>) ExtractInputsAndExpects(
				IEnumerable<TestData> inputAndExpects, 
				IEnumerable<string> appliedItems)
		{
			int index = 0;
			List<TestData> appliedDatas = new List<TestData>();
			foreach (var item in appliedItems)
			{
				if (item.ToLower().Equals("a"))
				{
					TestData baseItem = inputAndExpects.ElementAt(index);
					TestData testData = new TestData(baseItem);
					appliedDatas.Add(testData);
				}
				index++;
			}
			IEnumerable<TestData> inputs = appliedDatas.Where(_ => _.Condition.Equals("入力")).ToList();
			IEnumerable<TestData> expects = appliedDatas.Where(_ => _.Condition.Equals("期待値")).ToList();

			INFO("Applied test case:");
			INFO($"    Inputs : ");
			foreach (var item in inputs)
			{
				INFO($"        {item.Name} = {item.Value}");
			}
			INFO($"    Expects : ");
			foreach (var item in expects)
			{
				INFO($"        {item.Name} = {item.Value}");
			}

			return (inputs, expects);
		}
	}
}
