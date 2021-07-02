using AutoTestPrep.Model;
using AutoTestPrep.Model.InputInfos;
using AutoTestPrep.Model.Parser;
using AutoTestPrep.Model.Writer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Command
{
	/// <summary>
	/// Run main tool command.
	/// Read test design document and create test code 
	/// </summary>
	public class RunToolCommand
	{
		/// <summary>
		/// Run command.
		/// </summary>
		/// <param name="data">Parameters used when run command.</param>
		public void Run(object data)
		{
			try
			{
				var inputInfos = data as TestDataInfo;
				var parser = new TestParser();
				var tests = (IEnumerable<Test>)parser.Parse(inputInfos.TestDataFilePath);

				//Create stub codes.
				foreach (var testItem in tests)
				{
					string stubFileName = System.IO.Path.GetFileNameWithoutExtension(testItem.SourcePath);
					string stubFileExtention = System.IO.Path.GetExtension(testItem.SourcePath);
					string stubFilePath = inputInfos.OutputDirectoryPath + @"\" + stubFileName + "_stub" + stubFileExtention;

					foreach (var subFunctionItem in testItem.Target.SubFunctions)
					{
						var writer = new StubWriter();
						writer.Write(stubFilePath, subFunctionItem);
					}
				}

				//Craete driver codes.
			}
			catch (InvalidCastException)
			{
				throw;
			}
		}
	}
}
