using AutoTestPrep.Model.InputInfos;
using AutoTestPrep.Model.Parser;
using AutoTestPrep.Model.Writer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.DriverStubCreator
{
	public class GTestDriverStubCreator : IDriverStubCreator
	{
		public void Create(TestDataInfo dataInfo)
		{
			try
			{
				var parser = new TestParser();
				var tests = (IEnumerable<Test>)parser.Parse(dataInfo.TestDataFilePath);
				IEnumerable<IWriter> writers = new List<IWriter>
				{
					new StubWriter(),
					new TestDriverWriter(),
				};

				var helper = new WriterHelper();
				helper.Write(dataInfo, tests, writers);
			}
			catch (InvalidCastException)
			{
				throw;
			}
			catch (IOException)
			{
				throw;
			}
			catch (System.Exception ex)
			{
			}
		}
	}
}
