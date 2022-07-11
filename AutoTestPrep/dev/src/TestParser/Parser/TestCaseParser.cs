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

namespace TestParser.Parser
{

	public class TestCaseParser : AParser
	{
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
			this.SetupDatas(reader);
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
			this.SetupDatas(reader);
			IEnumerable<TestCase> testCases = this.ReadTestCase(reader);

			return testCases;
		}

		/// <summary>
		/// Setup data needed to get test case iformation.
		/// </summary>
		/// <param name="reader">Object to read data from excel.</param>
		protected void SetupDatas(ExcelReader reader)
		{
			INFO($"Start getting test data of target function in \"{this.Target}\" sheet.");

			this.SetupTableTopRange(reader);
			this.SetupInputRange(reader);
			this.SetupOutputRange(reader);
			this.SetupDataNames(reader);
			this.SetupDataValues(reader);
			this.SetupAllTestDatas();
		}

		/// <summary>
		/// Read test case from reader.
		/// </summary>
		/// <param name="reader">Object to read data from excel.</param>
		/// <returns>List of test case.</returns>
		protected IEnumerable<TestCase> ReadTestCase(ExcelReader reader)
		{
			try
			{
				//「代表値」のRangeオブジェクトを元に、テストケースのRangeオブジェクトを生成する
				Range testCaseRange = reader.FindFirstItem("代表値");
				testCaseRange.StartColumn++;
				IEnumerable<string> testCaseNumbers = reader.ReadRow(testCaseRange);
				int testCaseCount = testCaseNumbers.Count();
				var testCases = new List<TestCase>();
				for (int index = 0; index < testCaseCount; index++)
				{
					Range range = new Range(testCaseRange);
					range.StartColumn += index;
					TestCase testCase = this.ReadTestCase(reader, range);
					testCase.Id = testCaseNumbers.ElementAt(index);
					testCases.Add(testCase);
				}

				INFO("Test case datas:");
				foreach (var testCaseItem in testCases)
				{
					INFO($"Test case id = {testCaseItem.Id}");
					INFO($"    Inputs:");
					foreach (var inputItem in testCaseItem.Input)
					{
						INFO($"        Name  = {inputItem.Name}");
						INFO($"        Value = {inputItem.Value}");
					}
					INFO($"    Expects:");
					foreach (var expeectItem in testCaseItem.Expects)
					{
						INFO($"        Name = {expeectItem.Name}, value = {expeectItem.Value}");
						INFO($"        Value = {expeectItem.Value}");
					}
				}

				return testCases;
			}
			catch (FormatException)
			{
				ERROR($"Representative value\" cell can not be found in {this.Target} sheet.");

				throw;
			}
		}

		/// <summary>
		/// Setup test case from reader in a range.
		/// </summary>
		/// <param name="reader">Object to read data from excel.</param>
		/// <param name="testCaseRange">Range test cases are defined.</param>
		/// <returns></returns>
		protected TestCase ReadTestCase(ExcelReader reader, Range testCaseRange)
		{
			IEnumerable<string> testCaseItems = reader.ReadColumn(testCaseRange);
			IEnumerable<string> inputApply = null;
			IEnumerable<string> outputApply = null;
			this.SplitToInputOutput(testCaseItems, this.InputRange, this.OutputRange, out inputApply, out outputApply);
			IEnumerable<TestData> inputTestData = this.ReadTestCase(inputApply, this.AllInputData);
			IEnumerable<TestData> outputTestData = this.ReadTestCase(outputApply, this.AllOutputData);
			var testCase = new TestCase
			{
				Input = inputTestData,
				Expects = outputTestData
			};
			return testCase;
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
		/// Set table top into Range object.
		/// </summary>
		/// <param name="reader">Object to read data from excel.</param>
		/// <exception cref="FormatException">Input or output cell can not be found.</exception>
		protected void SetupTableTopRange(ExcelReader reader)
		{
			try
			{
				this.TableTop = reader.FindFirstItem("入力/出力");
			}
			catch (FormatException)
			{
				ERROR($"\"Input/Output\" cell can not be found in {this.Target} sheet.");

				throw;
			}
		}

		/// <summary>
		/// Setup input data range into Range object.
		/// </summary>
		/// <param name="reader">Object to read data from excel.</param>
		/// <exception cref="FormatException">"Input" cell can not be found.</exception>
		protected void SetupInputRange(ExcelReader reader)
		{
			try
			{
				Range range = reader.FindFirstItem("入力情報");
				reader.GetMergedCellRange(ref range);
				this.InputRange = range;

				DEBUG("\"Input data\" range:");
				DEBUG($"    start - ({range.StartRow}, {range.StartColumn})");
				DEBUG($"    count - ({range.RowCount}, {range.ColumnCount})");
			}
			catch (FormatException)
			{
				ERROR($"\"Input\" cell can not be found in {this.Target} sheet.");

				throw;
			}
		}

		/// <summary>
		/// Setup output data range into Range object.
		/// </summary>
		/// <param name="reader">Object to read data from excel.</param>
		protected void SetupOutputRange(ExcelReader reader)
		{
			try
			{
				Range range = reader.FindFirstItem("結果(動作)");
				reader.GetMergedCellRange(ref range);
				this.OutputRange = range;

				DEBUG("\"Output data\" range:");
				DEBUG($"    start - ({range.StartRow}, {range.StartColumn})");
				DEBUG($"    count - ({range.RowCount}, {range.ColumnCount})");
			}
			catch (FormatException)
			{
				ERROR($"\"Exepect(Output)\" can not be found in {this.Target} sheet.");

				throw;
			}
		}

		/// <summary>
		/// Setup list of data names.
		/// </summary>
		/// <param name="reader">Object to read data from excel.</param>
		protected void SetupDataNames(ExcelReader reader)
		{
			try
			{
				string dataId = "代表名";
				IEnumerable<string> inputDataNames = null;
				IEnumerable<string> outputDataNames = null;
				this.SetupDataCommon(reader, dataId, out inputDataNames, out outputDataNames);
				this.InputNames = inputDataNames;
				this.OutputNames = outputDataNames;
			}
			catch (FormatException)
			{
				ERROR($"\"Representative name\" can not be found in {this.Target} sheet.");

				throw;
			}
		}

		/// <summary>
		/// Setup list of test values.
		/// </summary>
		/// <param name="reader">Object to read excel data.</param>
		/// 
		protected void SetupDataValues(ExcelReader reader)
		{
			try
			{
				string dataid = "代表値";
				IEnumerable<string> inputDataValues = null;
				IEnumerable<string> outputDataValues = null;
				this.SetupDataCommon(reader, dataid, out inputDataValues, out outputDataValues);
				this.InputValues = inputDataValues;
				this.OutputValues = outputDataValues;
			}
			catch (FormatException)
			{
				ERROR($"\"Representative value\" can not be found in {this.Target} sheet.");

				throw;
			}
		}

		/// <summary>
		/// Common function to setup test data.
		/// </summary>
		/// <param name="reader">Object to read data from excel.</param>
		/// <param name="columnId">Id of column</param>
		/// <param name="inputs">Object to set input data.</param>
		/// <param name="outputs">Object to set output data.</param>
		/// <exception cref="FormatException">Data specified by argument <para>columnid</para> can not be found.</exception>
		protected void SetupDataCommon(ExcelReader reader, string columnId, out IEnumerable<string> inputs, out IEnumerable<string> outputs)
		{
			try
			{
				Range range = reader.FindFirstItem(columnId);
				IEnumerable<string> names = reader.ReadColumn(range);
				this.SplitToInputOutput<string>(names, this.InputRange, this.OutputRange, out inputs, out outputs);
			}
			catch (FormatException)
			{
				ERROR($"\"{columnId}\" can not be found in {this.Target} sheet.");

				throw;
			}
		}

		/// <summary>
		/// Setup all test data, input and output.
		/// </summary>
		protected void SetupAllTestDatas()
		{
			//入力データの確認：代表名と代表値の件数が一致しているか否かを確認する
			if ((this.InputNames.Count() != this.InputValues.Count()) ||
				(this.OutputNames.Count() != this.OutputValues.Count()))
			{
				throw new InvalidDataException();
			}
			int inputCount = this.InputNames.Count();
			var allInputData = new List<TestData>();
			for (int index = 0; index < inputCount; index++)
			{
				var testData = new TestData
				{
					Name = this.InputNames.ElementAt(index),
					Value = this.InputValues.ElementAt(index)
				};

				DEBUG("Get test input data.");
				DEBUG($"    Name  = {testData.Name}");
				DEBUG($"    Value = {testData.Value}");

				allInputData.Add(testData);
			}

			int outputCount = this.OutputNames.Count();
			var allOutputData = new List<TestData>();
			for (int index = 0; index < outputCount; index++)
			{
				var testData = new TestData
				{
					Name = this.OutputNames.ElementAt(index),
					Value = this.OutputValues.ElementAt(index)
				};

				DEBUG("Get test output data.");
				DEBUG($"    Name  = {testData.Name}");
				DEBUG($"    Value = {testData.Value}");

				allOutputData.Add(testData);
			}

			this.AllInputData = allInputData;
			this.AllOutputData = allOutputData;
		}

		/// <summary>
		/// Split test data into input and output depending on Range object information.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="targetItems">Target items to split.</param>
		/// <param name="inputRange">Range object about input data.</param>
		/// <param name="outputRange">Range object about output data.</param>
		/// <param name="inputItems">Reference to set input data .</param>
		/// <param name="outputItems">Reference to set output data.</param>
		protected void SplitToInputOutput<T>(
			IEnumerable<T> targetItems,
			Range inputRange,
			Range outputRange,
			out IEnumerable<T> inputItems,
			out IEnumerable<T> outputItems)
		{
			inputItems = targetItems.ToList().GetRange(1, inputRange.RowCount);
			outputItems = targetItems.ToList().GetRange(inputRange.RowCount + 1, outputRange.RowCount);
		}
	}
}
