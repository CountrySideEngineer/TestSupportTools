using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gtest2html
{
	partial class TestMessageTemplate
	{
		public string parentPage { get; set; }
		public Failure Failure { get; set; }

		public TestMessageTemplate(string parentPage, Failure failure)
		{
			this.parentPage = parentPage;
			this.Failure = failure;
		}
	}
}
