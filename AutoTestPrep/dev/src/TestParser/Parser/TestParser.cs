using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.TestParser.Parser
{
	using AutoTestPrep.Test;
	using AutoTestPrep.TestParser.Parameter;
	using AutoTestPrep.TestParser.Parser;
	using Reader;

	public class TestParser : ATestParser
	{
		/// <summary>
		/// Returns the test data, test case data, input and expect value, 
		/// </summary>
		/// <param name="srcPath">Input src file path.</param>
		/// <returns>Collection of test.</returns>
		/// <exception cref="NullReferenceException">
		/// Object to parse function list, function, or test case has not been set.
		/// </exception>
		public override object Parse(string srcPath)
		{
			try
			{
				return this.Read(srcPath);
			}
			catch (NullReferenceException)
			{
				throw;
			}
		}

		/// <summary>
		/// Returns the test data, test case data, input and expect value, 
		/// </summary>
		/// <param name="stream">Stream of input data.</param>
		/// <returns>Collection of test data including target function and test case.</returns>
		/// <exception cref="NullReferenceException">
		/// Object to parse function list, function, or test case has not been set.
		/// </exception>
		public override object Parse(FileStream stream)
		{
			try
			{
				return this.Read(stream);
			}
			catch(NullReferenceException)
			{
				throw;
			}
		}

		/// <summary>
		/// Read data from file.
		/// </summary>
		/// <param name="srcPath">Input src file path.</param>
		/// <returns>Object read from file <para>srcFile.</para></returns>
		/// <exception cref="NullReferenceException">
		/// Object to parse function list, function, or test case has not been set.
		/// </exception>
		protected object Read(string srcPath)
		{
			using (var stream = new FileStream(srcPath, FileMode.Open, FileAccess.Read))
			{
				try
				{
					return this.Read(stream);
				}
				catch (NullReferenceException)
				{
					throw;
				}
			}
		}

		/// <summary>
		/// Read test data from <para>stream</para>.
		/// </summary>
		/// <param name="stream">Stream to read data from.</param>
		/// <returns>Test data read from <para>stream</para>.</returns>
		/// <exception cref="NullReferenceException">
		/// Object to parse function list, function, or test case has not been set.
		/// </exception>
		protected object Read(FileStream stream)
		{
			try
			{
				this.FunctionListParser.Target = "テスト一覧";
				var testTargetFunctionInfos = (IEnumerable<ParameterInfo>)this.FunctionListParser.Parse(stream);

				var tests = new List<Test>();
				foreach (var testTargetFunctionInfoItem in testTargetFunctionInfos)
				{
					Test test = this.Read(stream, testTargetFunctionInfoItem);
					tests.Add(test);
				}

				return tests;
			}
			catch (NullReferenceException)
			{
				throw;
			}
		}

		/// <summary>
		/// Read test data from <para>stream</para> and specified by <para>paramInfo</para>.
		/// </summary>
		/// <param name="stream">Stream to read test data from.</param>
		/// <param name="paramInfo">Parameter information to read.</param>
		/// <returns>Test data read from <para>stream</para> and <para>paramInfo</para>.</returns>
		/// <exception cref="NullReferenceException">Object to parse function or test case has not been set.</exception>
		protected Test Read(FileStream stream, ParameterInfo paramInfo)
		{
			try
			{
				this.FunctionParser.Target = paramInfo.InfoName;
				var targetFunction = (Function)this.FunctionParser.Parse(stream);

				this.TestCaseParser.Target = paramInfo.InfoName;
				var testCases = (IEnumerable<TestCase>)this.TestCaseParser.Parse(stream);
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
			catch (NullReferenceException)
			{
				throw;
			}
		}
	}
}
