using AutoTestPrep.Model.InputInfos;
using AutoTestPrep.Model.Parser;
using AutoTestPrep.Model.Writer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.DriverStubCreator
{
	public class MidUnitDriverStubCreator : MinUnitDriverStubCreator
	{
		public override void Create(TestDataInfo dataInfo)
		{
			try
			{
				var parser = new TestParser();
				var tests = (IEnumerable<Test>)parser.Parse(dataInfo.TestDataFilePath);
				IEnumerable<IWriter> writers = new List<IWriter>
				{
					new StubWriter(),
					new MidUnitTestDriverSourceWriter(),
					new MidUnitTestDriverMainSourceWriter()
				};
				var helper = new MinUnitWriterHelper();
				helper.Write(dataInfo, tests, writers);
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
