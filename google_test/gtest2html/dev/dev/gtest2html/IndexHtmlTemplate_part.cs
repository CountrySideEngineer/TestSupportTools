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

		public int TestNum
		{
			get
			{
				int sum = 0;

				try
				{
					foreach (var testItem in TestSuitesList)
					{
						sum += testItem.Tests;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					sum = 0;
				}
				return sum;
			}
		}

		public int FailureNum
		{
			get
			{
				int sum = 0;

				try
				{
					foreach (var testItem in TestSuitesList)
					{
						sum += testItem.Failures;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					sum = 0;
				}
				return sum;
			}
		}

		public int DisableNum
		{
			get
			{
				int sum = 0;

				try
				{
					foreach (var testItem in TestSuitesList)
					{
						sum += testItem.Disabled;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					sum = 0;
				}
				return sum;
			}
		}

		public int ErrorNum
		{
			get
			{
				int sum = 0;

				try
				{
					foreach (var testItem in TestSuitesList)
					{
						sum += testItem.Errors;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					sum = 0;
				}
				return sum;
			}
		}

		public float TimeNum
		{
			get
			{
				float sum = 0;

				try
				{
					foreach (var testItem in TestSuitesList)
					{
						sum += testItem.Time;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					sum = 0;
				}
				return sum;
			}
		}
	}
}
