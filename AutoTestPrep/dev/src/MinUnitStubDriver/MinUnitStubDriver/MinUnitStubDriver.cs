using CodeGenerator;
using CodeGenerator.Data;
using CodeGenerator.Stub;
using CodeGenerator.TestDriver.MinUnit;
using CountrySideEngineer.ProgressWindow.Model;
using CountrySideEngineer.ProgressWindow.Model.Interface;
using StubDriverPlugin.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Data;
using TestParser.ParserException;

namespace StubDriverPlugin.MinUnitStubDriver
{
	public class MinUnitStubDriver : IStubDriverPlugin, IAsyncTask<ProgressInfo>
	{
		/// <summary>
		/// Plugin input data.
		/// </summary>
		PluginInput pluginInput;

		/// <summary>
		/// Plugin result, output data.
		/// </summary>
		PluginOutput pluginOutput;

		/// <summary>
		/// Execute process to create stub and test driver code using google test framework.
		/// </summary>
		/// <param name="data">Pluing input data.</param>
		/// <returns>Plugin ouput data containig result of the plugin.</returns>
		public virtual PluginOutput Execute(PluginInput data)
		{
			pluginInput = data;
			var progressWindow = new CountrySideEngineer.ProgressWindow.ProgressWindow();
			progressWindow.Start(this);

			return pluginOutput;
		}

		/// <summary>
		/// Execute task asynchronously.
		/// </summary>
		/// <param name="progress">IProgress object to notify progress of the process.</param>
		public void RunTask(IProgress<ProgressInfo> progress)
		{
			Task task = ExecuteAsync(progress, pluginInput);
		}

		/// <summary>
		/// Execute task asynchronously.
		/// </summary>
		/// <param name="progress">IProgress object to notify progress of the process.</param>
		/// <param name="data">Pluing input data.</param>
		/// <returns>Task executed.</returns>
		protected virtual async Task ExecuteAsync(IProgress<ProgressInfo> progress, PluginInput data)
		{
			Task<PluginOutput> task = CreateTask(progress, data);
			await task;
		}

		/// <summary>
		/// Create task to run process to create stub and test driver code.
		/// </summary>
		/// <param name="progress">IProgress object to notify progress of the process.</param>
		/// <param name="data">Pluing input data.</param>
		/// <returns>Task executed.</returns>
		protected virtual Task<PluginOutput> CreateTask(IProgress<ProgressInfo> progress, PluginInput data)
		{
			Task<PluginOutput> task = Task<PluginOutput>.Run(() =>
			{
				int denominator = 100;
				var basePogInfo = new ProgressInfo()
				{
					Title = data.InputFilePath,
					ProcessName = "解析中",
					Denominator = denominator,
					Numerator = 0
				};
				progress.Report(basePogInfo);

				PluginOutput pluginOutput = _Execute(data);

				var progInfo = new ProgressInfo(basePogInfo);
				progInfo.Numerator = 100;
				progInfo.Progress = 100;
				progress.Report(progInfo);

				return pluginOutput;
			});
			return task;
		}

		/// <summary>
		/// Execute plugin to create stub and driver code for test.
		/// </summary>
		/// <param name="data">Plugin input data.</param>
		/// <returns>Result of plugin.</returns>
		protected virtual PluginOutput _Execute(PluginInput data)
		{

			string outputAbout = "Min unit";
			try
			{
				IEnumerable<Test> tests = this.ParseExecute(data);
				DirectoryInfo rootDirInfo = new DirectoryInfo(data.OutputDirPath);
				CodeConfiguration stubConfig = this.Input2CodeConfigForStub(data);
				CodeConfiguration driverConfig = this.Input2CodeConfigForDriver(data);

				foreach (var testItem in tests)
				{
					this.CreateStubCode(testItem, rootDirInfo, stubConfig);
					this.CreateDriverCode(testItem, rootDirInfo, driverConfig);
				}

				pluginOutput = new PluginOutput(outputAbout, "min_unitフレームワークを使用したコードの生成が完了しました。");
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

				string errorMessage = "min_unitフレームワークを使用したコードの生成中にエラーが発生しました。";
				pluginOutput = new PluginOutput(outputAbout, errorMessage);
			}
			return pluginOutput;
		}

		/// <summary>
		/// Create code.
		/// </summary>
		/// <param name="test">Test data for the code.</param>
		/// <param name="rootDirInfo">Directory information for output.</param>
		/// <param name="config"><para>CodeConfiguration</para> object.</param>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		protected void CreateStubCode(Test test, DirectoryInfo rootDirInfo, CodeConfiguration config)
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
		/// Create code.
		/// </summary>
		/// <param name="test">Test data for the code.</param>
		/// <param name="rootDirInfo">Directory information for output.</param>
		/// <param name="config"><para>CodeConfiguration</para> object.</param>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		protected void CreateDriverCode(Test test, DirectoryInfo rootDirInfo, CodeConfiguration config)
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
		protected void CreateStubCode(DirectoryInfo outputRootDirInfo, WriteData data)
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

		/// <summary>
		/// Create test driver code.
		/// </summary>
		/// <param name="outputRootDirInfo">Directory information for output.</param>
		/// <param name="data">Write data.</param>
		protected void CreateDriverCode(DirectoryInfo outputRootDirInfo, WriteData data)
		{
			DirectoryInfo parentDirInfo = this.CreateOutputDirInfo(outputRootDirInfo, data);
			DirectoryInfo outputDirInfo = new DirectoryInfo($@"{parentDirInfo.FullName}\driver");
			Directory.CreateDirectory(outputDirInfo.FullName);

			//Create test driver source file.
			ICodeGenerator codeGenerator = new MinUnitSourceCodeGenerator();
			string outputFilePath = outputDirInfo.FullName + $@"\{data.Test.Name}_test.cpp";
			FileInfo sourceFileInfo = new FileInfo(outputFilePath);
			this.CreateCode(data, codeGenerator, sourceFileInfo);

			//Create test driver header file.
			codeGenerator = new MinUnitMainSourceCodeGenerator();
			outputFilePath = outputDirInfo.FullName + $@"\{data.Test.Name}_test_main.cpp";
			FileInfo headerFileInfo = new FileInfo(outputFilePath);
			this.CreateCode(data, codeGenerator, headerFileInfo);
		}

		/// <summary>
		/// Create code
		/// </summary>
		/// <param name="writeData">Write date</param>
		/// <param name="codeGenerator">Object to generate code.</param>
		/// <param name="fileInfo">Output file information.</param>
		protected void CreateCode(WriteData writeData, ICodeGenerator codeGenerator, FileInfo fileInfo)
		{
			string content = codeGenerator.Generate(writeData);
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
		protected IEnumerable<Test> ParseExecute(PluginInput data)
		{
			TestParser.IParser parser = new TestParser.Parser.TestParser();
			IEnumerable<Test> tests = this.ParseExecute(parser, data);

			return tests;
		}

		/// <summary>
		/// Parse and create test datas.
		/// </summary>
		/// <param name="parser">Parser inherits <para>IParser</para> interface.</param>
		/// <param name="input">Input data for the plugin.</param>
		/// <returns>Collection of <para>Test</para> object.</returns>
		protected IEnumerable<Test> ParseExecute(TestParser.IParser parser, PluginInput input)
		{
			string path = input.InputFilePath;
			IEnumerable<Test> tests = (IEnumerable<Test>)parser.Parse(path);
			return tests;
		}

		/// <summary>
		/// Convert plugin input data into to <para>CodeConfiguration</para> object to set CodeGenerator interface.
		/// </summary>
		/// <param name="input">Plugin input </param>
		/// <returns>WriteData object for stub.</returns>
		protected CodeConfiguration Input2CodeConfigForStub(PluginInput input)
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
		/// Convert plugin input data into to <para>CodeConfiguration</para> object to set CodeGenerator interface.
		/// </summary>
		/// <param name="input">Plugin input </param>
		/// <returns>WriteData object for driver.</returns>
		protected CodeConfiguration Input2CodeConfigForDriver(PluginInput input)
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
