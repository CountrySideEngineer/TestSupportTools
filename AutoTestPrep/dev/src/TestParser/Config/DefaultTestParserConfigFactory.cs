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
				RowOffset = 1,
				ColOffset = 1,
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
				RowOffset = 1,
				ColOffset = 1,
				RowDataOffset = 1,
				ColDataOffset = 0,
			};
			var targetFunctionConfig = new TargetFunctionConfig()
			{
				TableConfig = tableConfig
			};
			return targetFunctionConfig;
		}

		/// <summary>
		/// Create TestConfig object.
		/// </summary>
		/// <returns>TestConfig object with default parameter.</returns>
		protected static TestConfig CreateTestConfig()
		{
			var tableConfig = new TableConfig()
			{
				Name = "○テスト/デシジョンテーブル",
				RowOffset = 1,
				ColOffset = 1,
				RowDataOffset = 1,
				ColDataOffset = 0,
			};
			var testConfig = new TestConfig()
			{
				TableConfig = tableConfig
			};
			return testConfig;
		}
	}
}
