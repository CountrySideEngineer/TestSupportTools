using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gtest2html
{
	partial class IndexHtmlTemplate
	{
		public IEnumerable<TestSuites> TestSuitesList { get; set; }

		public IndexHtmlTemplate(IEnumerable<TestSuites> testSuites)
		{
			this.TestSuitesList = testSuites;
		}
	}
}
