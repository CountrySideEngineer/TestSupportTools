using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestParser.Config
{
	public class DefaultTestParserConfigFactory
	{
		/// <summary>
		/// Create default TestParserConfig object.
		/// </summary>
		/// <returns>TestParserConfig object with default parameter.</returns>
		public static TestParserConfig Create()
		{
			TestListConfig testListConfig = CreateTestList();
			TargetFunctionConfig targetFunctionConfig = CreateTargetFunctionConfig();
			TestConfig testConfig = CreateTestConfig();
			var testParserConfig = new TestParserConfig()
			{
				TestList = testListConfig,
				TargetFunction = targetFunctionConfig,
				Test = testConfig
			};
			return testParserConfig;
		}

		/// <summary>
		/// Create default TestListConfig object.
		/// </summary>
		/// <returns>TestListConfig object with default parameter.</returns>
		protected static TestListConfig CreateTestList()
		{
			var tableConfig = new TableConfig()
			{
				Name = "○テスト対象関数一覧",
				TableTopRowOffset = 1,
				TableTopColOffset = 1,
				RowDataOffset = 1,
				ColDataOffset = 0,
			};
			var testListConfig = new TestListConfig()
			{
				SheetName = "テスト一覧",
				TableConfig = tableConfig
			};
			return testListConfig;
		}

		/// <summary>
		/// Create TargetFunctionConfig object.
		/// </summary>
		/// <returns>TargetFunctionConfig object with default parameter.</returns>
		protected static TargetFunctionConfig CreateTargetFunctionConfig()
		{
			var tableConfig = new TableConfig()
			{
				Name = "○対象関数情報",
				TableTopRowOffset = 1,
				TableTopColOffset = 1,
				RowDataOffset = 1,
				ColDataOffset = 0,
			};
			var functionCofig = new FunctionConfig()
			{
				Type = "テスト対象関数",
				Body = "本体",
				Argument = "引数",
			};
			var subFunctionCofig = new FunctionConfig()
			{
				Type = "子関数",
				Body = "本体",
				Argument = "引数",
			};
			var globalVariableConfig = new GlobalVariableConfig()
			{
				Type = "グローバル変数",
				External = "外部",
				Internal = "内部"
			};
			var targetFunctionConfig = new TargetFunctionConfig()
			{
				TableConfig = tableConfig,
				TestFunctionConfig = functionCofig,
				SubFunctionConfig = subFunctionCofig,
				GlobalVariableConfig = globalVariableConfig,
			};
			return targetFunctionConfig;
		}

		/// <summary>
		/// Create TestConfig object.
		/// </summary>
		/// <returns>TestConfig object with default parameter.</returns>
		protected static TestConfig CreateTestConfig()
		{
			var tableConfig = new TestCaseTableConfig()
			{
				Name = "○テスト/デシジョンテーブル",
				TableTopRowOffset = 1,
				TableTopColOffset = 1,
				RowDataOffset = 1,
				ColDataOffset = 0,
				TestCaseRowOffset = 0,
				TestCaseColOffset = 5
			};
			var testCaseConfig = new TestCaseConfig()
			{
				InputExpect = "入力/期待値",
				Input = "入力",
				Expect = "期待値",
			};
			var testConfig = new TestConfig()
			{
				TableConfig = tableConfig,
				TestCaseConfig = testCaseConfig
			};
			return testConfig;
		}
	}
}
