using AutoTestPrep.Model.InputInfos;
using AutoTestPrep.Model.Tempaltes.Driver.mid_unit;
using CSEngineer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Writer
{
	public class MidUnitTestDriverSourceWriter : MinUnitTestDriverSourceWriter
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public MidUnitTestDriverSourceWriter() { }

		/// <summary>
		/// Write test code.
		/// </summary>
		/// <param name="path">Path to directory to output source code.</param>
		/// <param name="test">Test parameter.</param>
		/// <param name="testDataInfo">Test data information.</param>
		protected override void Write(string path, Test test, TestDataInfo testDataInfo)
		{
			string driverFilePath = string.Empty;
			try
			{
				string ext = System.IO.Path.GetExtension(test.SourcePath);
				string driverFileName = $"{test.Target.Name}_test{ext}";
				driverFilePath = $@"{path}\{driverFileName}";

				using (var stream = new StreamWriter(driverFilePath, false, Encoding.UTF8))
				{
					var template = new TestDriverTemplate_mid_unit_Source(test, testDataInfo);
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
