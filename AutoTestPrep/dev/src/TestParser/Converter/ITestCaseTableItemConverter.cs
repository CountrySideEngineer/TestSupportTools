using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Data;

namespace TestParser.Converter
{
	interface ITestCaseTableItemConverter
	{
		void Convert(ref TestData testData);
	}
}
