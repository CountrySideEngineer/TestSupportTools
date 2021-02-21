using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gtest2html
{
	partial class TestSuiteTemplate
	{
		public TestSuites TestSuites { get; set; }

		public TestSuiteTemplate(TestSuites testSuites)
		{
			this.TestSuites = testSuites;
		}
	}
}
