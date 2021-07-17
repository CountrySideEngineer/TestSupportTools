using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Writer
{
	using AutoTestPrep.Model.InputInfos;
	using AutoTestPrep.Model.Tempaltes.Stub;
	using CSEngineer;
	using System.IO;
	using Tempaltes;

	public class StubSourceWriter : IWriter
	{
		/// <summary>
		/// Write stub source code.
		/// </summary>
		/// <param name="path">Path to directory to output code.</param>
		/// <param name="parameters">
		/// Parameters for stub code.
		/// 1st object in array is Test object.
		/// </param>
		public void Write(string path, object[] parameters)
		{
			Test testParameter = null;
			TestDataInfo testDataInfo = null;
			try
			{
				testParameter = (Test)parameters[0];
				testDataInfo = (TestDataInfo)parameters[1];

				Logger.INFO($"Start generating stub source code of {testParameter.Target.Name}");

				string ext = System.IO.Path.GetExtension(testParameter.SourcePath);
				Function testFunction = testParameter.Target;
				IEnumerable<Function> subFunction = testFunction.SubFunctions;
				foreach (var subFunctionItem in subFunction)
				{
					try
					{
						this.Write(path, testFunction, subFunctionItem, testDataInfo, ext);
					}
					catch (Exception ex)
					when ((ex is PathTooLongException) || (ex is IOException))
					{
						Logger.ERROR($"\t\t-\tAn error occurred while generating stub source of method {subFunctionItem.Name}.");
						Logger.ERROR("\t\t-\tSkip the generating stub source.");
					}
				}
			}
			catch (NullReferenceException)
			{
				Logger.WARN($"\t\t-\tThe function \"{testParameter.Target.Name}\" has no sub function.");
				Logger.WARN($"\t\t\tSkip generating stub source file.");
			}
			catch (Exception ex)
			when ((ex is InvalidCastException) || (ex is IndexOutOfRangeException))
			{
				Logger.FATAL("\t\t-\tThe parameters to generate stub soruce cdeo are invalid.");
				throw;
			}
		}

		/// <summary>
		/// Generate stub source code.
		/// </summary>
		/// <param name="path">Path to directory to outptu the code.</param>
		/// <param name="functionItem">Parameter for stub code.</param>
		/// <param name="ext">Extention of stub code.</param>
		/// <exception cref="PathTooLongException">Path to output code is too long to generate.</exception>
		/// <exception cref="IOException">The output file path can not access.</exception>
		protected void Write(string path, Function functionItem, Function subFunction, TestDataInfo testDataInfo, string ext)
		{
			string stubFilePath = string.Empty;
			try
			{
				stubFilePath = path + @"\" + subFunction.Name + "_test_stub" + ext;

				Logger.INFO($"\t\t-\tStub source file path : {stubFilePath}");

				var template = new TestStubTemplate_Source(functionItem, subFunction, testDataInfo);
				//var template = new CFunctionStubTemplate(functionItem, testDataInfo);
				using (var stream = new StreamWriter(stubFilePath, false, Encoding.Unicode))
				{
					stream.Write(template.TransformText());
				}
			}
			catch (PathTooLongException)
			{
				Logger.WARN($"\t\t-\tStub code file path is TOO LONG. : {stubFilePath}");
				throw;
			}
			catch (IOException)
			{
				Logger.WARN($"\t\t-\tFile {stubFilePath} can not access.");
				throw;
			}
		}
	}
}
