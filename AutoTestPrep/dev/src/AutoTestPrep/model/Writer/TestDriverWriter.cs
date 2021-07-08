using AutoTestPrep.Model.InputInfos;
using AutoTestPrep.Model.Option;
using AutoTestPrep.Model.Tempaltes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Writer
{
	public class TestDriverWriter : IWriter
	{
		/// <summary>
		/// Write test driver code into file specified by <para>path</para>.
		/// </summary>
		/// <param name="path">Path to file to output.</param>
		/// <param name="parameters">Collection of parameters used to genearate codes.</param>
		public void Write(string path, object[] parameters)
		{
			try
			{
				var test = (Test)parameters[0];
				var testDataInfo = (TestDataInfo)parameters[1];
				string ext = System.IO.Path.GetExtension(test.SourcePath);
				Function function = test.Target;
				string fileName = function.Name + "_test" + ext;
				string outputPath = path + @"\" + fileName;
				using (var stream = new StreamWriter(outputPath, false, Encoding.UTF8))
				{
					var options = new Options()
					{
						IncludeStdHeaderFiles = testDataInfo.DriverIncludeStandardHeaderFiles,
						IncludeUserHeaderFiles = testDataInfo.DriverIncludeUserHeaderFiles
					};
					var template = new TestDriverTemplate_Source_gtest(test, options);
					stream.Write(template.TransformText());
				}
			}
			catch (IndexOutOfRangeException ex)
			{
				Debug.WriteLine(ex.Message);
				throw ex;
			}
		}
	}
}
