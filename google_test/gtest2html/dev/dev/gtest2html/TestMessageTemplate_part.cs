using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gtest2html
{
	partial class TestMessageTemplate
	{
		public TestCase TestCase { get; set; }
		public Failure Failure { get; set; }

		public TestMessageTemplate(TestCase testCase, Failure failure)
		{
			this.TestCase = testCase;
			this.Failure = failure;
		}
	}
}
