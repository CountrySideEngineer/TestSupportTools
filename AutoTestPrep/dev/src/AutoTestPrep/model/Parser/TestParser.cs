using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Parser
{
	using Reader;

	public class TestParser : IParser
	{
		/// <summary>
		/// Returns the test data, test case data, input and expect value, 
		/// </summary>
		/// <param name="srcPath"></param>
		/// <returns></returns>
		public object Parse(string srcPath)
		{
			using (var stream = new FileStream(srcPath, FileMode.Open, FileAccess.Read))
			{
				var reader = new ExcelReader(stream);
				var tests = this.Read(reader);

				return tests;
			}
		}

		/// <summary>
		/// Read test data from <para>reader</para>.
		/// </summary>
		/// <param name="reader">Object to read test datas.</param>
		/// <returns>Object of test data.</returns>
		protected object Read(ExcelReader reader)
		{
			var functionParser = new FunctionParser();
			var testCaseParser = new TestCaseParser();
			var tests = new List<Test>();
			IEnumerable<(string name, string sheetName, string sourceFilePath)> testList = this.ReadTestList(reader);
			foreach (var (name, sheetName, sourceFilePath) in testList)
			{
				reader.SheetName = sheetName;
				Parameter targetFunction = (Parameter)functionParser.Read(reader);
				IEnumerable<TestCase> testCases = (IEnumerable<TestCase>)testCaseParser.Read(reader);
				var test = new Test
				{
					Name = name,
					TestCases = testCases,
					Target = targetFunction
				};
				tests.Add(test);
			}

			return tests;
		}

		/// <summary>
		/// Read table of test items, name of test, sheet and source file path.
		/// </summary>
		/// <param name="reader">Object to read test data.</param>
		/// <returns>Name of test, sheet to read, and source file path in <para>Tuple</para> format.</returns>
		protected IEnumerable<(string name, string sheetName, string sourceFilePath)> ReadTestList(ExcelReader reader)
		{
			reader.SheetName = "テスト一覧";
			Range testNoRange = reader.FindFirstItem("No.");
			IEnumerable<string> testNos = reader.ReadColumn(testNoRange);
			int testCount = testNos.Count() - 1;
			Range testListItemRange = new Range(testNoRange);
			testListItemRange.StartRow++;
			var testList = new List<(string name, string sheetName, string sourceFilePath)>();
			for (int index = 0; index < testCount; index++)
			{
				IEnumerable<string> testListItem = reader.ReadRow(testListItemRange);
				testList.Add((testListItem.ElementAt(1), testListItem.ElementAt(2), testListItem.ElementAt(2)));
			}

			return testList;
		}
	}
}
