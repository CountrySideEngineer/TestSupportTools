using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gtest2html
{
	partial class TestSuiteHtmlTemplate
	{
		public TestSuites TestSuites { get; set; }

		public TestSuiteHtmlTemplate(TestSuites testSuites)
		{
			this.TestSuites = testSuites;
		}
	}
}
