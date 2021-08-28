using AutoTestPrep.Model.InputInfos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSEngineer;

namespace AutoTestPrep.Model.Writer
{
	public class WriterHelper
	{
		/// <summary>
		/// Write test date and its test information using write inherits IWriter object.
		/// </summary>
		/// <param name="testDataInfo">Test data information</param>
		/// <param name="tests">Collection of test.</param>
		/// <param name="writers">Collection of writer to write code using test and test information.</param>
		public void Write(TestDataInfo testDataInfo, IEnumerable<Test> tests, IEnumerable<IWriter> writers)
		{
			foreach (var testItem in tests)
			{
				this.Write(testDataInfo, testItem, writers);
			}
		}

		public void Write(TestDataInfo testDataInfo, Test test, IEnumerable<IWriter> writers)
		{
			//Create output directory.
			DirectoryInfo outputTopDirInfo = this.CreateOutputTopDirectory(testDataInfo);
			DirectoryInfo outputDirInfo = this.CreateOutputDirectory(outputTopDirInfo, test);
			foreach (var writerItem in writers)
			{
				this.Write(testDataInfo, test, writerItem, outputDirInfo);
			}
		}

		public void Write(TestDataInfo testDataInfo, Test test, IWriter writer)
		{
			//Create output directory.
			DirectoryInfo outputTopDirInfo = this.CreateOutputTopDirectory(testDataInfo);
			DirectoryInfo outputDirInfo = this.CreateOutputDirectory(outputTopDirInfo, test);
			this.Write(testDataInfo, test, writer, outputDirInfo);
		}

		protected DirectoryInfo CreateOutputTopDirectory(TestDataInfo testDataInfo)
		{
			var outputRootDirInfo = new DirectoryInfo(testDataInfo.OutputDirectoryPath);
			var testDataFlieName = Path.GetFileNameWithoutExtension(testDataInfo.TestDataFilePath);
			var outputTopDirPath = outputRootDirInfo + @"\" + testDataFlieName;
			DirectoryInfo outputTopDirInfo = Directory.CreateDirectory(outputTopDirPath);

			Logger.INFO($"Create directory \"{outputTopDirInfo.FullName}\".");

			return outputTopDirInfo;
		}

		protected DirectoryInfo CreateOutputDirectory(DirectoryInfo rootDirectory, Test test)
		{
			string rootDirectoryPath = rootDirectory.FullName;
			string outputDirectoryPath = rootDirectoryPath + @"\" + test.Name + "_test";
			DirectoryInfo outputDirectoryInfo = Directory.CreateDirectory(outputDirectoryPath);

			Logger.INFO($"Create directory \"{outputDirectoryInfo.FullName}\".");

			return outputDirectoryInfo;
		}

		protected void Write(TestDataInfo testDataInfo, Test test, IWriter writer, DirectoryInfo outputDirInfo)
		{
			object parameters = new object[2]
			{
				(object)test, (object)testDataInfo
			};
			string outputDirPath = outputDirInfo.FullName;
			writer.Write(outputDirPath, (object[])parameters);
		}
	}
}
