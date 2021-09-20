using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSEngineer;

namespace AutoTestPrep.Model.Writer
{
	using AutoTestPrep.Model.InputInfos;
	using AutoTestPrep.Model.Tempaltes.Stub;
	using System.IO;
	using Tempaltes;

	public class StubHeaderWriter : IWriter
	{
		/// <summary>
		/// Write stub header code into a file.
		/// </summary>
		/// <param name="path">Path to directory to generate header file.</param>
		/// <param name="parameters">Parameters for header code.</param>
		public void Write(string path, object[] parameters)
		{
			try
			{
				TestDataInfo testDataInfo = (TestDataInfo)parameters[1];
				if (parameters[0] is Test)
				{
					Test test = (Test)parameters[0];
					this.Write(path, test, testDataInfo);
				}
				else if (parameters[0] is IEnumerable<Test>)
				{
					IEnumerable<Test> tests = (IEnumerable<Test>)parameters[0];
					this.Write(path, tests, testDataInfo);
				}
				else
				{
					throw new ArgumentException();
				}
			}
			catch (NullReferenceException)
			{
				Logger.WARN($"\t\t-\tThe function has no sub function.");
				Logger.WARN($"\t\t\tSkip generating stub header file.");

				throw;
			}
			catch (Exception ex)
			when ((ex is InvalidCastException) || (ex is IndexOutOfRangeException))
			{
				Logger.FATAL("\t\t-\tThe parameters to generate stub soruce code are invalid.");
				throw;
			}
		}

		/// <summary>
		/// Write stub header code.
		/// </summary>
		/// <param name="path">Path to directory to generate header file.</param>
		/// <param name="test">Test data.</param>
		/// <param name="testDataInfo">Test information data.</param>
		protected void Write(string path, Test test, TestDataInfo testDataInfo)
		{
			Logger.INFO($"Start generating stub header code fo {test.Target.Name}");

			string ext = ".h";
			Function testFunction = test.Target;
			IEnumerable<Function> subFunctions = testFunction.SubFunctions;
			foreach (var subFunctionItem in subFunctions)
			{
				try
				{
					this.Write(path, testFunction, subFunctionItem, testDataInfo, ext);
				}
				catch (Exception ex)
				when ((ex is PathTooLongException) || (ex is IOException))
				{
					Logger.ERROR($"\t\t-\tAn error occurred while generating stub header of method {testFunction.Name}.");
					Logger.ERROR("\t\t\tSkip the generating stub header.");
				}
			}
		}

		/// <summary>
		/// Write stub header code.
		/// </summary>
		/// <param name="path">Path to directory to output header file.</param>
		/// <param name="tests">Test data.</param>
		/// <param name="testDataInfo">test data information.</param>
		protected void Write(string path, IEnumerable<Test> tests, TestDataInfo testDataInfo)
		{
			foreach (var testItem in tests)
			{
				this.Write(path, testItem, testDataInfo);
			}
		}

		/// <summary>
		/// Write code of stub header into a file path.
		/// </summary>
		/// <param name="path">Path to directory to output the stub header code.</param>
		/// <param name="functionItem">Function information.</param>
		/// <param name="ext">Extention of output file.</param>
		protected void Write(string path, Function functionItem, Function subFunction, TestDataInfo testDataInfo, string ext)
		{
			string stubFilePath = string.Empty;
			try
			{
				stubFilePath = path + @"\" + subFunction.Name + "_test_stub" + ext;
				Logger.INFO($"\t\t-\tStub header file path : {stubFilePath}");

				var template = new TestStubTemplate_Header(functionItem, subFunction, testDataInfo);
				using (var stream = new StreamWriter(stubFilePath, false, Encoding.Unicode))
				{
					stream.Write(template.TransformText());
				}
			}
			catch (PathTooLongException)
			{
				Logger.ERROR($"\t\t-\tStub code file path is TOO LONG. : {stubFilePath}");
			}
			catch (IOException)
			{
				Logger.ERROR($"\t\t-\tFile {stubFilePath} can not access.");
			}
		}
	}
}
