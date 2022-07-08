using System;
using System.IO;
using System.Collections.Generic;
using TestParser.Target;
using TestParser.Data;
using System.Linq;

namespace TestParser.Parser
{
	public class TestParser : ATestParser
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public TestParser()
		{
			this.FunctionListParser = new FunctionListParser("テスト一覧");
			this.FunctionParser = new FunctionParser();
			this.TestCaseParser = new TestCaseParser();
		}

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
		public override object Parse(Stream stream)
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
		/// <exception cref="IOException">The file <para>srcPath</para> are already opened by other process.</exception>
		protected object Read(string srcPath)
		{
			try
			{
				using (var stream = new FileStream(srcPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				{
					base.INFO($"Start reading input file : {srcPath}");
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
			catch (IOException)
			{
				ERROR($"Error occurred when reading input file.");
				throw;
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
		protected object Read(Stream stream)
		{
			try
			{
				NotifyParseProgressDelegate?.Invoke(0, 100);
				var testTargetFunctionInfos = (IEnumerable<ParameterInfo>)this.FunctionListParser.Parse(stream);
				NotifyParseProgressDelegate?.Invoke(100, 100);

				var tests = new List<Test>();
				int index = 0;
				foreach (var paramInfoItem in testTargetFunctionInfos)
				{
					Test test = this.Read(stream, paramInfoItem);
					tests.Add(test);

					NotifyParseProgressDelegate?.Invoke((index + 1), testTargetFunctionInfos.Count());
					index++;
				}

				return tests;
			}
			catch (NullReferenceException)
			{
				ERROR($"NullReferenceException detected while reading data.");
				throw;
			}
			catch (System.Exception ex)
			{
				ERROR($"{nameof(ex)}");
				ERROR($"{ex.Message}");

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
		protected Test Read(Stream stream, ParameterInfo paramInfo)
		{
			try
			{
				INFO("Start reading target function data.");
				this.FunctionParser.Target = paramInfo.InfoName;
				var targetFunction = (Function)this.FunctionParser.Parse(stream);

				INFO("Start reading test case data.");
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
