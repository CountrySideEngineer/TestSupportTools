using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Config;
using TestParser.Data;
using TestParser.ParserException;

namespace TestParser.Converter
{
	public class TestCaseTableItemConverter : ATestCaseTableItemConverter
	{
		public TestConfig TestConfig { get; set; }

		public TestCaseTableItemConverter()
		{
			TestConfig = null;
		}

		public TestCaseTableItemConverter(TestConfig testConfig)
		{
			TestConfig = testConfig;
		}

		public override void Convert(ref TestData testData)
		{
			try
			{
				if ((TestConfig.TestCaseConfig.Expect.Equals(testData.Condition)) &&
					(TestConfig.TestCaseConfig.ReturnValue.Equals(testData.Descriotion)))
				{
					testData.Name = TestConfig.TestCaseConfig.ReturnVariableName;
				}
			}
			catch (NullReferenceException)
			{
				FATAL("TestCase configuration data has not been set.");
				throw new TestParserException(TestParserException.Code.PARSRE_ERROR_TEST_CASE_TABLE_PARSER_CONFIGURATION_INVALID);
			}
		}
	}
}
