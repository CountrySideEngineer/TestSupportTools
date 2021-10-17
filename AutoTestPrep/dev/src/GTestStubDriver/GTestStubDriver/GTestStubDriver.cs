using StubDriverPlugin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Parser;
using TestParser.Data;
using CodeGenerator.Data;
using CodeGenerator.Stub;
using System.IO;
using CodeGenerator;

namespace StubDriverPlugin.GTestStubDriver
{
	public class GTestStubDriver : IStubDriverPlugin
	{
		/// <summary>
		/// Create stub and driver code for test.
		/// </summary>
		/// <param name="data">Plugin input data</param>
		/// <returns>Result of plugin.</returns>
		public PluginOutput Execute(PluginInput data)
		{
			IEnumerable<Test> tests = this.ParseExecute(data);
			DirectoryInfo rootDirInfo = new DirectoryInfo(data.OutputDirPath);
			CodeConfiguration config = this.Input2CodeConfigForStub(data);

			foreach (var testItem in tests)
			{
				this.CreateCode(testItem, rootDirInfo, config);
			}

			var output = new PluginOutput()
			{
				Message = "OK"
			};
			return output;
		}

		public void CreateCode(Test test, DirectoryInfo rootDirInfo, CodeConfiguration config)
		{
			var writeData = new WriteData()
			{
				Test = test,
				CodeConfig = config
			};
			this.CreateStubCode(rootDirInfo, writeData);
		}

		/// <summary>
		/// Create stub code.
		/// </summary>
		/// <param name="outputRootDirInfo">Directory information for output.</param>
		/// <param name="data">Write data.</param>
		protected void CreateStubCode(DirectoryInfo outputRootDirInfo, WriteData data)
		{
			//Create output directory.
			DirectoryInfo dirOutputInfo = new DirectoryInfo(outputRootDirInfo.FullName + @"\Stub");
			Directory.CreateDirectory(dirOutputInfo.FullName);

			//Create stub source file.
			ICodeGenerator codeGenerator = new StubSourceGenerator();
			string outputName = dirOutputInfo.FullName + $@"\{data.Test.Target.Name}_stub.c";
			FileInfo outputFileInfo = new FileInfo(outputName);
			this.CreateCode(data, codeGenerator, outputFileInfo);

			//Create stub header file.
			codeGenerator = new StubHeaderGenerator();
			outputName = dirOutputInfo.FullName + $@"\{data.Test.Target.Name}_stub.h";
			outputFileInfo = new FileInfo(outputName);
			this.CreateCode(data, codeGenerator, outputFileInfo);
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

		protected IEnumerable<Test> ParseExecute(TestParser.IParser parser, PluginInput input)
		{
			string path = input.InputFilePath;
			IEnumerable<Test> tests = (IEnumerable<Test>)parser.Parse(path);
			return tests;
		}

		protected WriteData CreateWriteDataForStub(Test test, PluginInput input)
		{
			CodeConfiguration config = this.Input2CodeConfigForStub(input);
			var writeData = new WriteData()
			{
				Test = test,
				CodeConfig = config
			};
			return writeData;
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
	}
}
