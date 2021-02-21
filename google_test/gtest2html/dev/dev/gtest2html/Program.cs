using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace gtest2html
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Count() < 2)
			{
				Console.WriteLine("2 arguments are needed.");
				Console.WriteLine("1st : Path to output.");
				Console.WriteLine("2nd : Path to directory contains the test result xml files.");

				return;
			}

			string outputDirPath = args[0];
			string xmlDirPath = args[1];

			var outputDirInfo = CreateOutputDirInfo(outputDirPath);
			var xmlFileInfos = GetXmlFilePaths(xmlDirPath);
			var xmlFileList = GetXmlFilePaths(xmlDirPath);
			var toHtml = new XmlToHtml(outputDirInfo, xmlFileList);
			toHtml.Convert();

			return;
		}

		/// <summary>
		/// Returns the list of file information contained in direcotry specified by argument
		/// dirPath.
		/// </summary>
		/// <param name="dirPath">Path to directory contains result of test in XML format.</param>
		/// <returns>List of test result XML file information.</returns>
		private static IEnumerable<FileInfo> GetXmlFilePaths(string dirPath)
		{
			if (File.Exists(dirPath))
			{
				throw new ArgumentException("The path is not directory.");
			}
			else if (!Directory.Exists(dirPath))
			{
				throw new ArgumentException("The directory has not existed.");
			}
			else
			{
				var dirInfo = new DirectoryInfo(dirPath);
				var fileInfos = dirInfo.GetFiles("*.xml").ToList();

				return fileInfos;
			}
		}

		/// <summary>
		/// Create directory information object about output directory.
		/// </summary>
		/// <param name="dirPath">Path to output html result.</param>
		/// <returns>Directory information of output directory.</returns>
		private static DirectoryInfo CreateOutputDirInfo(string dirPath)
		{
			//If the path specified by argument dirPath mean file, it is invalid.
			if (File.Exists(dirPath)) {
				throw new ArgumentException("The path has already existed.");
			}

			if (!Directory.Exists(dirPath))
			{
				Directory.CreateDirectory(dirPath);
			}

			var dirInfo = new DirectoryInfo(dirPath);
			return dirInfo;
		}
	}
}
