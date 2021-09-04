using AutoTestPrep.Model.InputInfos;
using AutoTestPrep.Model.Tempaltes.Driver.gtest;
using CSEngineer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Writer
{
	public class TestDriverSourceWriter : IWriter
	{
		/// <summary>
		/// Write test driver source file usgin google test.
		/// </summary>
		/// <param name="path">Path to directory to output code.</param>
		/// <param name="parameters">Parameters fot test diver.
		/// 1st object in the array is Test obejct.
		/// 2nd is TestDataInfo object.
		/// </param>
		public void Write(string path, object[] parameters)
		{
			string outputPath = string.Empty;
			Test test = null;
			TestDataInfo testDataInfo = null;
			try
			{
				test = (Test)parameters[0];
				testDataInfo = (TestDataInfo)parameters[1];

				Logger.INFO($"Start generating test driver source code of {test.Target.Name}.");

				string ext = System.IO.Path.GetExtension(test.SourcePath);
				string fileName = $"{test.Target.Name}_test{ext}";
				outputPath = $@"{path}\{fileName}";

				Logger.INFO($"\t\t-\tDriver soruce file path : {outputPath}.");

				using (var stream = new StreamWriter(outputPath, false, Encoding.UTF8))
				{
					var template = new TestDriverTemplate_gtest_Source(test, testDataInfo);
					template.DriverHeaderFileName = $"{test.Target.Name}_test.h";
					stream.Write(template.TransformText());
				}
			}
			catch (Exception ex)
			when ((ex is InvalidCastException) || (ex is IndexOutOfRangeException))
			{
				Logger.FATAL("Parameteras to generated tes dirver codes are invalid.");
			}
			catch (PathTooLongException)
			{
				Logger.WARN($"\t\t-\tDriver source code file paht is TOO LONG. : {outputPath}");
			}
			catch (IOException)
			{
				Logger.WARN($"\t\t-\tFile {outputPath} can not access.");
			}
		}
	}
}
