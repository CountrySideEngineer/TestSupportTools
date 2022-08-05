using CodeGenerator;
using CodeGenerator.Data;
using CodeGenerator.Stub;
using CodeGenerator.TestDriver.GoogleTest;
using CountrySideEngineer.ProgressWindow.Model;
using StubDriverPlugin.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestParser.Data;
using TestParser.ParserException;

namespace StubDriverPlugin.GTestStubDriver
{
	public class GTestStubDriverPluginExecute : IStubDriverPlugin
	{
		public delegate void NotifyParseProgress(int numerator, int denominator);
		public NotifyParseProgress NotifyParseProgressDelegate;

		public delegate void NotifyPluginFinish();
		public NotifyPluginFinish NotifyPluginFinishDelegate;

		/// <summary>
		/// /Default constructor.
		/// </summary>
		public GTestStubDriverPluginExecute() { }

		/// <summary>
		/// Create stub and driver code for test.
		/// </summary>
		/// <param name="data">Plugin input data</param>
		/// <returns>Result of plugin.</returns>
		public virtual PluginOutput Execute(PluginInput data)
		{
			Debug.Assert(null != data, $"{nameof(GTestStubDriver)}.{nameof(Execute)}, {nameof(data)}");

			string outputAbout = "Google test";
			PluginOutput pluginOutput = null;
			try
			{
				NotifyParseProgressDelegate?.Invoke(0, 100);
				IEnumerable<Test> tests = this.ParseExecute(data);
				NotifyParseProgressDelegate?.Invoke(100, 100);


				DirectoryInfo rootDirInfo = new DirectoryInfo(data.OutputDirPath);
				CodeConfiguration stubCodeConfig = this.Input2CodeConfigForStub(data);
				CodeConfiguration driverCodeConfig = this.Input2CodeConfigForDriver(data);

				int testIndex = 0;
				NotifyParseProgressDelegate?.Invoke(testIndex, tests.Count());
				foreach (var testItem in tests)
				{
					this.CreateStubCode(testItem, rootDirInfo, stubCodeConfig);
					this.CreateDriverCode(testItem, rootDirInfo, driverCodeConfig);

					testIndex++;
					NotifyParseProgressDelegate?.Invoke(testIndex, tests.Count());

					Thread.Sleep(200);
				}
				NotifyPluginFinishDelegate?.Invoke();

				pluginOutput = new PluginOutput(outputAbout, "Google Testフレームワークを使用したコードの生成が完了しました。");
			}
			catch (TestParserException ex)
			{
				string errorMessage =
					$"テストデータの解析中にエラーが発生しました。" +
					Environment.NewLine +
					$"エラーコード：0x{Convert.ToString(ex.ErrorCode, 16)}";
				pluginOutput = new PluginOutput(outputAbout, errorMessage);
			}
			catch (CodeGeneratorException ex)
			{
				string errorMessgae =
					$"コードの作成中にエラーが発生しました。" +
					Environment.NewLine +
					$"エラーコード：0x{Convert.ToString(ex.ErrorCode, 16)}";
				pluginOutput = new PluginOutput(outputAbout, errorMessgae);
			}
			catch (Exception ex)
			when ((ex is ArgumentException) || (ex is ArgumentNullException))
			{
				Debug.WriteLine(ex.StackTrace);

				pluginOutput = new PluginOutput(outputAbout, "Google Testフレームワークを使用したコードの生成中に\r\nエラーが発生しました。");
			}
			catch (Exception ex)
			when (ex is IOException)
			{
				Debug.WriteLine(ex.StackTrace);

				pluginOutput = new PluginOutput(outputAbout, "指定されたファイルを開けませんでした。");
			}
			catch (Exception ex)
			{
				pluginOutput = new PluginOutput(outputAbout, $"プラグイン実行中にエラーが発生しました。\n{ex.Message}");
			}
			return pluginOutput;
		}

		/// <summary>
		/// Create test stub code.
		/// </summary>
		/// <param name="test">Test input data.</param>
		/// <param name="rootDirInfo">Eoor directory information.</param>
		/// <param name="config">Code configuration.</param>
		protected virtual void CreateStubCode(Test test, DirectoryInfo rootDirInfo, CodeConfiguration config)
		{
			try
			{
				var writeData = new WriteData()
				{
					Test = test,
					CodeConfig = config
				};
				this.CreateStubCode(rootDirInfo, writeData);
			}
			catch (Exception ex)
			when ((ex is ArgumentException) || (ex is ArgumentNullException))
			{
				Debug.WriteLine(ex.StackTrace);

				throw;
			}
		}

		/// <summary>
		/// Create test driver code.
		/// </summary>
		/// <param name="test">Test input data.</param>
		/// <param name="rootDirInfo">Root directory information.</param>
		/// <param name="config">Code configuration.</param>
		protected virtual void CreateDriverCode(Test test, DirectoryInfo rootDirInfo, CodeConfiguration config)
		{
			try
			{
				var writeData = new WriteData()
				{
					Test = test,
					CodeConfig = config
				};
				this.CreateDriverCode(rootDirInfo, writeData);
			}
			catch (Exception ex)
			when ((ex is ArgumentException) || (ex is ArgumentNullException))
			{
				Debug.WriteLine(ex.StackTrace);

				throw;
			}
		}

		/// <summary>
		/// Create stub code.
		/// </summary>
		/// <param name="outputRootDirInfo">Directory information for output.</param>
		/// <param name="data">Write data.</param>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		protected virtual void CreateStubCode(DirectoryInfo outputRootDirInfo, WriteData data)
		{
			try
			{
				if ((null == data.Test.Target.SubFunctions) || (data.Test.Target.SubFunctions.Count() < 1))
				{
					/*
					 * In a case that a target function has no sub function, stub codes are not needed.
					 * So, prevent the codes from creating, skip operations below.
					 */
					return;
				}
				//Create output directory.
				DirectoryInfo parentDirInfo = this.CreateOutputDirInfo(outputRootDirInfo, data);
				DirectoryInfo outputDirInfo = new DirectoryInfo($@"{parentDirInfo.FullName}\stub");
				Directory.CreateDirectory(outputDirInfo.FullName);

				//Create stub source file.
				ICodeGenerator codeGenerator = new StubSourceGenerator();
				string outputName = outputDirInfo.FullName + $@"\{data.Test.Target.Name}_stub.cpp";
				FileInfo outputFileInfo = new FileInfo(outputName);
				this.CreateCode(data, codeGenerator, outputFileInfo);

				//Create stub header file.
				codeGenerator = new StubHeaderGenerator();
				outputName = outputDirInfo.FullName + $@"\{data.Test.Target.Name}_stub.h";
				outputFileInfo = new FileInfo(outputName);
				this.CreateCode(data, codeGenerator, outputFileInfo);
			}
			catch (Exception ex)
			when ((ex is ArgumentException) || (ex is ArgumentNullException))
			{
				Debug.WriteLine(ex.StackTrace);

				throw;
			}
		}

		/// <summary>
		/// Create test driver code.
		/// </summary>
		/// <param name="outputRootDirInfo">Directory information for output.</param>
		/// <param name="data">Write data.</param>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		protected virtual void CreateDriverCode(DirectoryInfo outputRootDirInfo, WriteData data)
		{
			try
			{
				DirectoryInfo parentDirInfo = this.CreateOutputDirInfo(outputRootDirInfo, data);
				DirectoryInfo outputDirInfo = new DirectoryInfo($@"{parentDirInfo.FullName}\driver");
				Directory.CreateDirectory(outputDirInfo.FullName);

				//Create test driver source file.
				ICodeGenerator codeGenerator = new GoogleTestSourceCodeGenerator();
				string outputFilePath = outputDirInfo.FullName + $@"\{data.Test.Name}_test.cpp";
				FileInfo sourceFileInfo = new FileInfo(outputFilePath);
				this.CreateCode(data, codeGenerator, sourceFileInfo);

				//Create test driver header file.
				codeGenerator = new GoogleTestHeaderCodeGenerator();
				outputFilePath = outputDirInfo.FullName + $@"\{data.Test.Name}_test.h";
				FileInfo headerFileInfo = new FileInfo(outputFilePath);
				this.CreateCode(data, codeGenerator, headerFileInfo);
			}
			catch (Exception ex)
			when ((ex is ArgumentException) || (ex is ArgumentNullException))
			{
				Debug.WriteLine(ex.StackTrace);

				throw;
			}
		}

		/// <summary>
		/// Create code
		/// </summary>
		/// <param name="writeData">Write date</param>
		/// <param name="codeGenerator">Object to generate code.</param>
		/// <param name="fileInfo">Output file information.</param>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		protected virtual void CreateCode(WriteData writeData, ICodeGenerator codeGenerator, FileInfo fileInfo)
		{
			string content = string.Empty;
			try
			{
				content = codeGenerator.Generate(writeData);
			}
			catch (Exception ex)
			when ((ex is ArgumentException) || (ex is ArgumentNullException))
			{
				Debug.Write(ex.StackTrace);

				throw;
			}
			using (var stream = File.CreateText(fileInfo.FullName))
			{
				stream.Write(content);
			}
		}

		/// <summary>
		/// Parse test data and create test datas.
		/// </summary>
		/// <param name="data">Test data input.</param>
		/// <returns>Test data parsed by a parser </returns>
		protected virtual IEnumerable<Test> ParseExecute(PluginInput data)
		{
			var parser = new TestParser.Parser.TestParser();
			IEnumerable<Test> tests = this.ParseExecute(parser, data);

			return tests;
		}

		/// <summary>
		/// Parse and create test datas.
		/// </summary>
		/// <param name="parser">Parser inherits <para>IParser</para> interface.</param>
		/// <param name="input">Input data for the plugin.</param>
		/// <returns>Collection of <para>Test</para> object.</returns>
		protected virtual IEnumerable<Test> ParseExecute(TestParser.IParser parser, PluginInput input)
		{
			string path = input.InputFilePath;
			IEnumerable<Test> tests = (IEnumerable<Test>)parser.Parse(path);
			return tests;
		}

		/// <summary>
		/// Convert plugin input data for stub code into <para>CodeConfiguration</para> object to set CodeGenerator interface.
		/// </summary>
		/// <param name="input">Plugin input </param>
		/// <returns>WriteData object for stub.</returns>
		protected virtual CodeConfiguration Input2CodeConfigForStub(PluginInput input)
		{
			var config = new CodeConfiguration()
			{
				BufferSize1 = Convert.ToInt32(input.StubBufferSize1),
				BufferSize2 = Convert.ToInt32(input.StubBufferSize2),
				StandardHeaderFiles = new List<string>(input.StubIncludeStandardHeaderFiles),
				UserHeaderFiles = new List<string>(input.StubIncludeUserHeaderFiles)
			};
			return config;
		}

		/// <summary>
		/// Convert plugin input data for test driver into <para>CodeConfiguration</para> object to set CodeGenerator interface.
		/// </summary>
		/// <param name="input">Plugin input</param>
		/// <returns>Write data object for driver.</returns>
		protected virtual CodeConfiguration Input2CodeConfigForDriver(PluginInput input)
		{
			var config = new CodeConfiguration()
			{
				BufferSize1 = Convert.ToInt32(input.StubBufferSize1),
				BufferSize2 = Convert.ToInt32(input.StubBufferSize2),
				StandardHeaderFiles = new List<string>(input.DriverIncludeStandardHeaderFiles),
				UserHeaderFiles = new List<string>(input.DriverIncludeUserHeaderFiles)
			};
			return config;
		}

		/// <summary>
		/// Create information about directory to output data.
		/// </summary>
		/// <param name="rootDir">Root directory information of output root.</param>
		/// <param name="data">Write data</param>
		/// <returns><para>DirectoryInfo</para> object about output.</returns>
		protected virtual DirectoryInfo CreateOutputDirInfo(DirectoryInfo rootDir, WriteData data)
		{
			string outputDirPath = $@"{rootDir.FullName}\{data.Test.Target.Name}_test";
			var outputDirInfo = new DirectoryInfo(outputDirPath);
			return outputDirInfo;
		}
	}
}
