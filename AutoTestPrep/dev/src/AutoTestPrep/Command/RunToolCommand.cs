using AutoTestPrep.Model;
using AutoTestPrep.Model.InputInfos;
using AutoTestPrep.Model.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Command
{
	/// <summary>
	/// Run main tool command.
	/// Read test design document and create test code 
	/// </summary>
	public class RunToolCommand
	{
		public void Run(object data)
		{
			try
			{
				var inputInfos = data as TestDataInfo;
				var functionListParser = new FunctionListParser();
				var parameterInfos = (IEnumerable<ParameterInfo>)functionListParser.Parse(inputInfos.TestDataFilePath);
				var tests = new List<Test>();
				foreach (var parameterInfoItem in parameterInfos)
				{
					this.ParseSequence(inputInfos, parameterInfoItem, out Test test);
					tests.Add(test);
				}
			}
			catch (InvalidCastException)
			{
				throw;
			}
		}

		/// <summary>
		/// Parse test data and set test case data to argument <para>test</para>.
		/// </summary>
		/// <param name="testInfo">Information about test design, spec.</param>
		/// <param name="paramInfo">Test parameter like sheet name, target source file path, etc...</param>
		/// <param name="test">Test data including test target function and test cases.</param>
		protected void ParseSequence(TestDataInfo testInfo, 
			ParameterInfo paramInfo, 
			out Test test)
		{
			var testParser = new TestParser()
			{
				ParamInfo = paramInfo
			};
			test = (Test)testParser.Parse(testInfo.TestDataFilePath);
		}
	}
}
