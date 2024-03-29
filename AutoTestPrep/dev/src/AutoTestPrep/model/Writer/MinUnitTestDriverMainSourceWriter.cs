﻿using AutoTestPrep.Model.InputInfos;
using AutoTestPrep.Model.Tempaltes.Driver.min_unit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Writer
{
	public class MinUnitTestDriverMainSourceWriter : IWriter
	{
		public virtual void Write(string path, object[] parameters)
		{
			var tests = (IEnumerable<Test>)parameters[0];
			var testDataInfo = (TestDataInfo)parameters[1];
			string ext = System.IO.Path.GetExtension(tests.ElementAt(0).SourcePath);
			string testInputFileNmae = System.IO.Path.GetFileNameWithoutExtension(testDataInfo.TestDataFilePath);
			string outputPath = $@"{path}\main.cpp";

			using (var stream = new StreamWriter(outputPath, false, Encoding.UTF8))
			{
				var template = new TestDriverTemplate_min_unit_main_Source(tests, testDataInfo);
				stream.Write(template.TransformText());
			}
		}
	}
}
