using AutoTestPrep.Model.InputInfos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Writer
{
	public class WriterHelper
	{
		public void Write(TestDataInfo testDataInfo, Test test, IEnumerable<IWriter> writers)
		{
			//Create output directory.
			DirectoryInfo outputTopDirInfo = this.CreateOutputTopDirectory(testDataInfo);
			DirectoryInfo outputDirInfo = this.CreateOutputDirectory(outputTopDirInfo, test);
			foreach (var writerItem in writers)
			{
				this.Write(test, writerItem, outputDirInfo);
			}
		}

		public void Write(TestDataInfo testDataInfo, Test test, IWriter writer)
		{
			//Create output directory.
			DirectoryInfo outputTopDirInfo = this.CreateOutputTopDirectory(testDataInfo);
			DirectoryInfo outputDirInfo = this.CreateOutputDirectory(outputTopDirInfo, test);
			this.Write(test, writer, outputDirInfo);
		}

		protected DirectoryInfo CreateOutputTopDirectory(TestDataInfo testDataInfo)
		{
			var outputRootDirInfo = new DirectoryInfo(testDataInfo.OutputDirectoryPath);
			var testDataFlieName = Path.GetFileNameWithoutExtension(testDataInfo.TestDataFilePath);
			var outputTopDirPath = outputRootDirInfo + @"\" + testDataFlieName;
			DirectoryInfo outputTopDirInfo = Directory.CreateDirectory(outputTopDirPath);

			return outputTopDirInfo;
		}

		protected DirectoryInfo CreateOutputDirectory(DirectoryInfo rootDirectory, Test test)
		{
			string rootDirectoryPath = rootDirectory.FullName;
			string outputDirectoryPath = rootDirectoryPath + @"\" + test.Name + "_test";
			DirectoryInfo outputDirectoryInfo = Directory.CreateDirectory(outputDirectoryPath);

			return outputDirectoryInfo;
		}

		protected void Write(Test test, IWriter writer, DirectoryInfo outputDirInfo)
		{
			string outputDirPath = outputDirInfo.FullName;
			writer.Write(outputDirPath, test);
		}
	}
}
