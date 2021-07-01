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
		/// Test parameter information.
		/// </summary>
		public ParameterInfo ParamInfo { get; set; }

		/// <summary>
		/// Returns the test data, test case data, input and expect value, 
		/// </summary>
		/// <param name="srcPath">Input src file path.</param>
		/// <returns></returns>
		public object Parse(string srcPath)
		{
			using (var stream = new FileStream(srcPath, FileMode.Open, FileAccess.Read))
			{
				var reader = new ExcelReader(stream);
				var test = this.Read(reader);

				return test;
			}
		}

		/// <summary>
		/// Read test data from <para>reader</para>.
		/// </summary>
		/// <param name="reader">Object to read test datas.</param>
		/// <returns>Object of test data.</returns>
		protected object Read(ExcelReader reader)
		{
			reader.SheetName = this.ParamInfo.InfoName;
			var functionParser = new FunctionParser();
			var testTargetFunction = (Function)functionParser.Read(reader);
			var testCaseParser = new TestCaseParser();
			IEnumerable<TestCase> testCases = (IEnumerable<TestCase>)testCaseParser.Read(reader);
			var test = new Test
			{
				Name = this.ParamInfo.Name,
				TestCases = testCases,
				Target = testTargetFunction,
				TestInformation = this.ParamInfo.InfoName,
				SourcePath = this.ParamInfo.FileName
			};
			return test;
		}
	}
}
