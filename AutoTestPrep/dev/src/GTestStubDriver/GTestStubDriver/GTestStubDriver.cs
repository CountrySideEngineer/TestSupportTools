﻿using StubDriverPlugin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Parser;
using TestParser.ParserException;
using TestParser.Data;
using CodeGenerator.Data;
using CodeGenerator.Stub;
using System.IO;
using CodeGenerator;
using CodeGenerator.TestDriver.GoogleTest;
using System.Diagnostics;
using CountrySideEngineer.ProgressWindow.Model;
using CountrySideEngineer.ProgressWindow.Model.Interface;
using System.Threading;

namespace StubDriverPlugin.GTestStubDriver
{
	public class GTestStubDriver : IStubDriverPlugin, IAsyncTask<ProgressInfo>
	{
		PluginInput pluginInput;
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
		/// Create stub and driver code for test.
		/// </summary>
		/// <param name="data">Plugin input data</param>
		/// <returns>Result of plugin.</returns>
		public virtual PluginOutput _Execute(PluginInput data)
		{
			Debug.Assert(null != data, $"{nameof(GTestStubDriver)}.{nameof(Execute)}, {nameof(data)}");

			IEnumerable<Test> tests = this.ParseExecute(data);
			DirectoryInfo rootDirInfo = new DirectoryInfo(data.OutputDirPath);
			CodeConfiguration config = this.Input2CodeConfigForStub(data);

			string outputAbout = "Google test";
			try
			{
				foreach (var testItem in tests)
				{
					this.CreateCode(testItem, rootDirInfo, config);
				}

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
			return pluginOutput;
		}

		/// <summary>
		/// Create code.
		/// </summary>
		/// <param name="test">Test data for the code.</param>
		/// <param name="rootDirInfo">Directory information for output.</param>
		/// <param name="config"><para>CodeConfiguration</para> object.</param>
		protected virtual void CreateCode(Test test, DirectoryInfo rootDirInfo, CodeConfiguration config)
		{
			try
			{
				var writeData = new WriteData()
				{
					Test = test,
					CodeConfig = config
				};
				this.CreateStubCode(rootDirInfo, writeData);
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
		/// Convert plugin input data into to <para>CodeConfiguration</para> object to set CodeGenerator interface.
		/// </summary>
		/// <param name="input">Plugin input </param>
		/// <returns>WriteData object for S</returns>
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
