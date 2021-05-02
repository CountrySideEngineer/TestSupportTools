using AutoTestPrep.Model.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Tempaltes
{
	partial class TestDriverTemplate_Source_gtest
	{
		/// <summary>
		/// Test information.
		/// </summary>
		public Test Test { get; set; }

		/// <summary>
		/// Option data about test.
		/// </summary>
		public Options Options { get; set; }

		public TestDriverTemplate_Source_gtest(Test test, Options options)
		{
			this.Test = test;
			this.Options = options;
		}

		/// <summary>
		/// Content of code including standard header files.
		/// </summary>
		public string StdHeaders
		{
			get
			{
				var template = new CFunctionTemplateIncludeStdHeader(this.Options);
				var content = template.TransformText();

				return content;
			}
		}

		/// <summary>
		/// Content of code including user header files.
		/// </summary>
		public string UserHeaders
		{
			get
			{
				var template = new CFunctionTemplateIncludeUserHeader(this.Options);
				var content = template.TransformText();

				return content;
			}
		}

		/// <summary>
		/// Content of function code to initialize stubs.
		/// </summary>
		public string InitStub
		{
			get
			{
				var template = new TestDriverTemplate_Source_gtest_InitStub(this.Test);
				var content = template.TransformText();
				return content;
			}
		}

		public string TestCase
		{
			get
			{
				var template = new TestDriverTemplate_Source_gtest_TestCase(this.Test);
				var content = template.TransformText();
				return content;
			}
		}
	}
}
