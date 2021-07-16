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
		public void Write(string path, object[] parameters)
		{
			Test testParameter = null;
			TestDataInfo testDataInfo = null;
			try
			{
				testParameter = (Test)parameters[0];
				testDataInfo = (TestDataInfo)parameters[1];

				Logger.INFO($"Start generating stub header code of {testParameter.Target.Name}");

				string ext = ".h";
				Function testFunction = testParameter.Target;
				try
				{
					this.Write(path, testFunction, testDataInfo, ext);
				}
				catch (Exception ex)
				when ((ex is PathTooLongException) || (ex is IOException))
				{
					Logger.ERROR($"\t\t-\tAn error occurred while generating stub header of method {testFunction.Name}.");
					Logger.ERROR("\t\t\tSkip the generating stub header.");
				}
			}
			catch (NullReferenceException)
			{
				Logger.WARN($"\t\t-\tThe function \"{testParameter.Target.Name}\" has no sub function.");
				Logger.WARN($"\t\t\tSkip generating stub header file.");

				throw;
			}
			catch (Exception ex)
			when ((ex is InvalidCastException) || (ex is IndexOutOfRangeException))
			{
				Logger.FATAL("\t\t-\tThe parameters to generate stub soruce cdeo are invalid.");
				throw;
			}
		}

		/// <summary>
		/// Write code of stub header into a file path.
		/// </summary>
		/// <param name="path">Path to directory to output the stub header code.</param>
		/// <param name="functionItem">Function information.</param>
		/// <param name="ext">Extention of output file.</param>
		protected void Write(string path, Function functionItem, TestDataInfo testDataInfo, string ext)
		{
			string stubFilePath = string.Empty;
			try
			{
				stubFilePath = path + @"\" + functionItem.Name + "_test_stub" + ext;
				Logger.INFO($"\t\t-\tStub header file path : {stubFilePath}");

				var template = new TestStubTemplate_Header(functionItem, testDataInfo);
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
