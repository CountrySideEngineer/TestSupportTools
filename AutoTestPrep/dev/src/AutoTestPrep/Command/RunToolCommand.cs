using AutoTestPrep.Model;
using AutoTestPrep.Model.InputInfos;
using AutoTestPrep.Model.Parser;
using AutoTestPrep.Model.Writer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
				var rootDirectory = new DirectoryInfo(inputInfos.OutputDirectoryPath);
				string outputDirName = System.IO.Path.GetFileNameWithoutExtension(inputInfos.TestDataFilePath);
				DirectoryInfo outputDirectoryInfo = this.CreateOutputDirectory(rootDirectory, outputDirName);
				var parser = new TestParser();
				var tests = (IEnumerable<Test>)parser.Parse(inputInfos.TestDataFilePath);

				//Create stub codes.
				foreach (var testItem in tests)
				{
					this.WriteStubCodes(testItem, outputDirectoryInfo);
					this.WriteDriverCode(testItem, outputDirectoryInfo);
				}
			}
			catch (InvalidCastException)
			{
				throw;
			}
		}

		protected DirectoryInfo CreateOutputDirectory(DirectoryInfo parent, string child)
		{
			string parentPath = parent.FullName;
			string targetPath = parentPath + @"\" + child;
			DirectoryInfo childDirectoryInfo = Directory.CreateDirectory(targetPath);

			return childDirectoryInfo;
		}

		protected void WriteStubCodes(Test test, DirectoryInfo outputTopDirectory)
		{
			DirectoryInfo outputDirectory = this.CreateOutputDirectory(outputTopDirectory, test.SourcePath);

			string extention = System.IO.Path.GetExtension(test.SourcePath);
			var writer = new StubWriter();
			foreach ( var subFunctionItem in test.Target.SubFunctions)
			{
				string stubFileName = subFunctionItem.Name;
				string stubFilePath = outputDirectory.FullName + @"\" + stubFileName + "_test_stub" + extention;
				writer.Write(stubFilePath, subFunctionItem);
			}
		}

		protected void WriteDriverCode(Test test, DirectoryInfo outputTopDirectory)
		{
			//Create driver codes.
			string extention = System.IO.Path.GetExtension(test.SourcePath);
			string driverFileName = test.Target.Name;
			string driverFilePath = outputTopDirectory.FullName + @"\" + driverFileName + "_test" + extention;
			var writer = new TestDriverWriter();
			try
			{
				writer.Write(driverFilePath, test);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}
	}
}
