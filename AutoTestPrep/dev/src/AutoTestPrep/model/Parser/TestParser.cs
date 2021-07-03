using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Parser
{
	using AutoTestPrep.Model.InputInfos;
	using Reader;

	public class TestParser : IParser
	{
		/// <summary>
		/// Returns the test data, test case data, input and expect value, 
		/// </summary>
		/// <param name="srcPath">Input src file path.</param>
		/// <returns></returns>
		public object Parse(string srcPath)
		{
			return this.Read(srcPath);
		}

		/// <summary>
		/// Returns the test data, test case data, input and expect value, 
		/// </summary>
		/// <param name="stream">Stream of input data.</param>
		/// <returns>Collection of test data including target function and test case.</returns>
		public object Parse(FileStream stream)
		{
			return this.Read(stream);
		}

		/// <summary>
		/// Read data from file.
		/// </summary>
		/// <param name="srcPath">Input src file path.</param>
		/// <returns>Object read from file <para>srcFile.</para></returns>
		protected object Read(string srcPath)
		{
			using (var stream = new FileStream(srcPath, FileMode.Open, FileAccess.Read))
			{
				return this.Read(stream);
			}
		}

		protected object Read(FileStream stream)
		{
			var functionListParser = new FunctionListParser();
			var testTargetFunctionInfos = (IEnumerable<ParameterInfo>)functionListParser.Parse(stream);
			var reader = new ExcelReader(stream);

			var tests = new List<Test>();
			foreach (var testTargetFunctionInfoItem in testTargetFunctionInfos)
			{
				Test test = this.Read(stream, testTargetFunctionInfoItem);
				tests.Add(test);
			}

			return tests;
		}

		/// <summary>
		/// Read test data from <para>stream</para> and specified by <para>paramInfo</para>.
		/// </summary>
		/// <param name="stream">Stream to read test data from.</param>
		/// <param name="paramInfo">Parameter information to read.</param>
		/// <returns>Test data read from <para>stream</para> and <para>paramInfo</para>.</returns>
		protected Test Read(FileStream stream, ParameterInfo paramInfo)
		{
			var functionParser = new FunctionParser()
			{
				Target = paramInfo.InfoName
			};
			var targetFunction = (Function)functionParser.Parse(stream);

			var testCaseParser = new TestCaseParser()
			{
				Target = paramInfo.InfoName
			};
			var testCases = (IEnumerable<TestCase>)testCaseParser.Parse(stream);
			var test = new Test
			{
				Name = paramInfo.Name,
				TestCases = testCases,
				Target = targetFunction,
				TestInformation = paramInfo.InfoName,
				SourcePath = paramInfo.FileName
			};
			return test;
		}
	}
}
