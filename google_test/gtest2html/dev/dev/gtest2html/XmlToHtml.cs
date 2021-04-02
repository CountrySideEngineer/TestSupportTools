using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace gtest2html
{
	public class XmlToHtml
	{
		#region Public Properties
		/// <summary>
		///	Output directory information.
		/// </summary>
		public DirectoryInfo OutputDirInfo;

		/// <summary>
		/// List of test resutl xml files.
		/// </summary>
		public IEnumerable<FileInfo> XmlFileInfos;
		#endregion

		#region Constructor and destructor(Finalizer)
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="outputDir">Path to output report.</param>
		/// <param name="xmlFilePathList">List of file path to result of test in xml format.</param>
		public XmlToHtml(DirectoryInfo outputDir, IEnumerable<FileInfo> xmlFileInfos)
		{
			this.OutputDirInfo = outputDir;
			this.XmlFileInfos = xmlFileInfos;
		}
		#endregion

		#region Other methods and private properties in calling order
		/// <summary>
		/// Run convert.
		/// </summary>
		public void Convert()
		{
			this.ConvertXmlToHtml();
			this.CreateIndexHtml();
		}

		/// <summary>
		/// Convert xml into html.
		/// </summary>
		protected void ConvertXmlToHtml()
		{
			foreach (var xmlFileInfoItem in this.XmlFileInfos)
			{
				this.ConvertXmlToHtml(xmlFileInfoItem);

			}
		}

		/// <summary>
		/// Convert xml specified by argumetn xmlFilePaht.
		/// </summary>
		/// <param name="xmlFilePath">Path to xml file to be converted into </param>
		protected void ConvertXmlToHtml(FileInfo fileInfo)
		{
			//入力ファイル名を元に、出力ファイルのパスを特定する。
			var inputFileName = Path.GetFileName(fileInfo.FullName);
			var outputFileName = Path.ChangeExtension(inputFileName, "html");
			var outputFileInfo = new FileInfo(this.OutputDirInfo.FullName + @"\" + outputFileName);

			using (var xmlReader = new StreamReader(fileInfo.FullName, Encoding.GetEncoding("UTF-8")))
			{
				var serializer = new XmlSerializer(typeof(TestSuites));
				var testSuites = (TestSuites)serializer.Deserialize(xmlReader);
				var htmlTemplate = new TestSuiteHtmlTemplate(testSuites);
				var content = htmlTemplate.TransformText();
				using (var htmlStream = new StreamWriter(outputFileInfo.FullName, false, Encoding.GetEncoding("UTF-8")))
				{
					htmlStream.Write(content);
				}
				foreach (var testSuite in testSuites.TestItems)
				{
					foreach (var testCase in testSuite.TestCases)
					{
						if (testCase.IsFail)
						{
							this.ConvertHmlToHtml(fileInfo, testSuite, testCase);
						}
					}
				}
			}
		}

		protected void ConvertHmlToHtml(FileInfo fileInfo, TestSuite testSuite, TestCase testCase)
		{
			//入力ファイルを元に、出力ファイルのパスを特定する。
			var outputFileName = testSuite.Name + "_" + testCase.Name + ".html";
			var outputFileInfo = new FileInfo(this.OutputDirInfo.FullName + @"\" + outputFileName);

			/*
			 * エラーメッセージに含まれている改行は、HTMLでは表示されない。
			 * そのため、事前に改行を「<br>」に置換する。
			 * 元のメッセージは加工したくないので、新規にFailureオブジェクトを生成して、
			 * そこに変換したメッセージをセットする。
			 */
			var failure = new Failure
			{
				Message = testCase.Failure.Message.Replace("\n", "<br>")
			};
			var htmlTempalte = new TestMessageTemplate(testCase, failure);
			var content = htmlTempalte.TransformText();
			using (var htmlStream = new StreamWriter(outputFileInfo.FullName, false, Encoding.GetEncoding("UTF-8")))
			{
				htmlStream.Write(content);
			}
		}

		/// <summary>
		/// Create index.html file.
		/// </summary>
		protected void CreateIndexHtml()
		{
			var testSuiteList = new List<TestSuites>();
			foreach (var xmlFileInfo in this.XmlFileInfos)
			{
				using (var xmlStream = new StreamReader(xmlFileInfo.FullName, Encoding.GetEncoding("UTF-8")))
				{
					var serializer = new XmlSerializer(typeof(TestSuites));
					var testSuites = (TestSuites)serializer.Deserialize(xmlStream);
					var testSuiteName = Path.GetFileNameWithoutExtension(xmlFileInfo.FullName);
					testSuites.TestName = testSuiteName;
					testSuiteList.Add(testSuites);
				}
			}
			var indexHtmlFilePath = this.OutputDirInfo.FullName + @"\index.html";
			var indexTemplate = new IndexHtmlTemplate(testSuiteList);
			var content = indexTemplate.TransformText();
			using (var htmlStream = new StreamWriter(indexHtmlFilePath, false, Encoding.GetEncoding("UTF-8")))
			{
				htmlStream.Write(content);
			}
		}
		#endregion
	}
}
