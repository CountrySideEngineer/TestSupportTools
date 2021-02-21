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
		private static void XmlToHtml(string xmlFilePath)
		{
			var xmlFileName = Path.GetFileName(xmlFilePath);
			var htmlFileName = Path.ChangeExtension(xmlFileName, "html");

			using (var xmlStream = new FileStream(xmlFilePath, FileMode.Open))
			{
				var serializer = new XmlSerializer(typeof(TestSuites));
				var testSuites = (TestSuites)serializer.Deserialize(xmlStream);
				var htmlTemplate = new TestSuiteHtmlTemplate(testSuites);
				var content = htmlTemplate.TransformText();
				using (var textStream = new StreamWriter(htmlFileName, false, Encoding.GetEncoding("UTF-8")))
				{
					textStream.Write(content);
				}
			}
		}

		private static void CreateIndexHtml(IEnumerable<string> xmlFiles)
		{
			var testSuiteList = new List<TestSuites>();
			foreach (var xmlFilePath in xmlFiles)
			{
				using (var xmlStream = new FileStream(xmlFilePath, FileMode.Open))
				{
					var serializer = new XmlSerializer(typeof(TestSuites));
					var testSuites = (TestSuites)serializer.Deserialize(xmlStream);
					testSuites.TestName = Path.GetFileNameWithoutExtension(xmlFilePath);
					testSuiteList.Add(testSuites);
				}
			}
			var indexTemplate = new IndexHtmlTemplate(testSuiteList);
			var content = indexTemplate.TransformText();

			using (var textStream = new StreamWriter(@".\index.html", false, Encoding.GetEncoding("UTF-8")))
			{
				textStream.Write(content);
			}

		}

		static void Main(string[] args)
		{
			/*
			 *	Judge the arguement means file or directory.
			 *	If directory, read xml file path from it, otherwise if file, file convert into html.
			 */
			if (args.Count() < 1)
			{
				Console.WriteLine("Invalid argument.");

				return;
			}

			var filePathList = new List<string>();
			if (File.Exists(args[0]))
			{
				filePathList.Add(args[0]);
			}
			else if (Directory.Exists(args[0]))
			{
				var directoryInfo = new DirectoryInfo(args[0]);
				FileInfo[] files = directoryInfo.GetFiles("*.xml");
				foreach (var file in files)
				{
					filePathList.Add(file.FullName);
				}
			}

			foreach (var xmlFile in filePathList)
			{
				XmlToHtml(xmlFile);
			}
			CreateIndexHtml(filePathList);
		}
	}
}
