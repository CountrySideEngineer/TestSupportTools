using System;
using System.IO;
using System.Collections.Generic;
using TestParser.Target;
using TestParser.Data;
using System.Linq;
using TestParser.Reader;
using TestParser.Config;
using TestParser.ParserException;
using System.Security;

namespace TestParser.Parser
{
	public class TestParser : ATestParser
	{
		protected TestParserConfig _testConfig;

		protected string _configFilePath;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public TestParser()
		{
			_testConfig = null;
			_configFilePath = @".\TestParserConfg.xml";
			this.FunctionListParser = null;
			this.FunctionParser = null;
			this.TestCaseParser = null;
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
		/// <exception cref="TestParserException">Error while opening and reading input file.</exception>
		protected object Read(string srcPath)
		{
			try
			{
				using (var stream = new FileStream(srcPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				{
					INFO($"Start reading input file : {srcPath}");
					try
					{
						return Read(stream);
					}
					catch (NullReferenceException)
					{
						throw;
					}
				}
			}
			catch (System.Exception ex)
			when (ex is ArgumentNullException)
			{
				ERROR("No test file path has been set.");
				throw new TestParserException(TestParserException.Code.PARSER_ERROR_FILE_CAN_NOT_OPEN);
			}
			catch (System.Exception ex)
			when ((ex is ArgumentException) ||
				(ex is FileNotFoundException) ||
				(ex is SecurityException) ||
				(ex is DirectoryNotFoundException) ||
				(ex is PathTooLongException))
			{
				ERROR($"File path {srcPath} is invalid.");
				throw new TestParserException(TestParserException.Code.PARSER_ERROR_FILE_CAN_NOT_OPEN);
			}
			catch (SecurityException)
			{
				ERROR($"File {srcPath} can not access.");
				throw new TestParserException(TestParserException.Code.PARSER_ERROR_FILE_CAN_NOT_OPEN);
			}
			catch (System.Exception ex)
			when ((ex is NotSupportedException) || (ex is ArgumentOutOfRangeException))
			{
				ERROR($"File path {srcPath} is not supported.");
				throw new TestParserException(TestParserException.Code.PARSER_ERROR_FILE_CAN_NOT_OPEN);
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
				IEnumerable<ParameterInfo> testTargetFunctionInfos = ReadFunctionList(stream);

				string procName = "テスト設計情報読出し";
				var tests = new List<Test>();
				int index = 0;
				foreach (var paramInfoItem in testTargetFunctionInfos)
				{
					Test test = Read(stream, paramInfoItem);
					tests.Add(test);

					index++;
					string processName = $"{procName} : {paramInfoItem.Name}";
					NotifyProcessAndProgressDelegate?.Invoke(processName, index, testTargetFunctionInfos.Count());
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
				if (!string.IsNullOrEmpty(ex.Message))
				{
					ERROR($"{ex.Message}");
				}

				NotifyParseProgressDelegate?.Invoke(100, 100);
				throw ex;
			}
		}

		/// <summary>
		/// Read target function list datas.
		/// </summary>
		/// <param name="stream">Stream to read data.</param>
		/// <returns>Collection of ParameterInfo object about test target function.</returns>
		protected IEnumerable<ParameterInfo> ReadFunctionList(Stream stream)
		{
			string procName = "対象関数一覧読出し";
			NotifyProcessAndProgressDelegate?.Invoke(procName, 0, 0);
			INFO("Start function list.");
			LoadConfig();

			if (null == FunctionListParser)
			{
				FunctionListParser = new FunctionListParser(
					_testConfig.TestList.SheetName, 
					_testConfig.TestList);
			}
			var testTargetFunctionInfos = (IEnumerable<ParameterInfo>)FunctionListParser.Parse(stream);

			NotifyProcessAndProgressDelegate?.Invoke(procName, 100, 100);
			if (0 == testTargetFunctionInfos.Count())
			{
				ERROR("No test function data has been set in function table.");
				throw new TestParserException(TestParserException.Code.PARSER_ERROR_NO_TEST_FUNCTION_SET);
			}

			return testTargetFunctionInfos;
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
				if (null == FunctionParser)
				{
					FunctionParser = new FunctionParser(
						_testConfig.TargetFunction.TableConfig.Name,
						_testConfig.TargetFunction);
				}
				this.FunctionParser.Target = paramInfo.InfoName;
				var targetFunction = (Function)FunctionParser.Parse(stream);

				INFO("Start reading test case data.");
				if (null == this.TestCaseParser)
				{
					this.TestCaseParser = new TestCaseParser(_testConfig.Test);
				}
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
				ERROR("protected Test Read(Stream stream, ParameterInfo paramInfo)");
				throw;
			}
			catch (InvalidCastException ex)
			{
				ERROR(ex.Message);
				throw;
			}
		}

		/// <summary>
		/// Load configuration file.
		/// </summary>
		protected void LoadConfig()
		{
			try
			{
				_testConfig = TestParserConfig.LoadConfig(_configFilePath);
			}
			catch (System.IO.FileNotFoundException)
			{
				WARN($"The test config file {_configFilePath} has not been found.");
				WARN("Load default config setting.");
				_testConfig = TestParserConfig.LoadDefaultConfig();
			}
			catch (System.Exception)
			{
				WARN("The test config file can not load.");
				WARN("    Use default config setting.");
				_testConfig = TestParserConfig.LoadDefaultConfig();
			}
			finally
			{
				DEBUG("TestParserConfig");
				DEBUG($"    Sheet name : {_testConfig.TestList.SheetName}");
				DEBUG($"    Row offset : {_testConfig.TestList.TableConfig.TableTopRowOffset}");
				DEBUG($"    Col offset : {_testConfig.TestList.TableConfig.TableTopColOffset}");
			}
		}
	}
}
