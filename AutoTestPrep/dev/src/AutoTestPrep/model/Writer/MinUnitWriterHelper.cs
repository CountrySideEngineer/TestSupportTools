using AutoTestPrep.Model.InputInfos;
using CSEngineer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Writer
{
	public class MinUnitWriterHelper : IWriterHelper
	{
		public virtual void Write(TestDataInfo testDataInfo, IEnumerable<Test> tests, IEnumerable<IWriter> writers)
		{
			DirectoryInfo outputDirInfo = this.CreateOutputDirectory(testDataInfo);
			foreach (var writer in writers)
			{
				object[] parameters = new object[2] { tests, testDataInfo };
				writer.Write(outputDirInfo.FullName, parameters);
			}
		}

		protected virtual DirectoryInfo CreateOutputDirectory(TestDataInfo testDataInfo)
		{
			string outputDirPath = testDataInfo.OutputDirectoryPath;
			var dirCreator = new DirectoryCreator(outputDirPath);
			DirectoryInfo dirInfo = dirCreator.Create();

			return dirInfo;
		}
	}
}
