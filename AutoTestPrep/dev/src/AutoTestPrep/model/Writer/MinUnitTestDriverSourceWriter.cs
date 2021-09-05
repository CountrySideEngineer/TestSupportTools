using AutoTestPrep.Model.InputInfos;
using AutoTestPrep.Model.Tempaltes.Driver.min_unit;
using CSEngineer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AutoTestPrep.Model.Writer
{
	public class MinUnitTestDriverSourceWriter : IWriter
	{
		/// <summary>
		/// Write test driver source file using min unit test framework.
		/// </summary>
		/// <param name="path">Path to directory to output code.</param>
		/// <param name="parameters">Parameters fot test diver.
		/// 1st object in the array is Test obejct.
		/// 2nd is TestDataInfo object.</param>
		public virtual void Write(string path, object[] parameters)
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

		protected virtual void Write(string path, IEnumerable<Test> tests, TestDataInfo testDataInfo)
		{
			foreach (var test in tests)
			{
				this.Write(path, test, testDataInfo);
			}
		}

		protected virtual void Write(string path, Test test, TestDataInfo testDataInfo)
		{
			string driverFilePath = string.Empty;
			try
			{
				string ext = System.IO.Path.GetExtension(test.SourcePath);
				string driverFileName = $"{test.Target.Name}_test{ext}";
				driverFilePath = $@"{path}\{driverFileName}";

				using (var stream = new StreamWriter(driverFilePath, false, Encoding.UTF8))
				{
					var template = new TestDriverTemplate_min_unit_Source(test, testDataInfo);
					stream.Write(template.TransformText());
				}
			}
			catch (PathTooLongException)
			{
				Logger.WARN($"\t\t-\tDriver source code file paht is TOO LONG. : {driverFilePath}");
			}
			catch (IOException)
			{
				Logger.WARN($"\t\t-\tFile {driverFilePath} can not access.");
			}
		}
	}
}
