using AutoTestPrep.Model.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FunctoinListParser_Test
{
	[TestClass]
	public class FunctionListParserTest
	{
		[TestMethod]
		[TestCategory("UnitTest")]
		public void Parse_001()
		{
			string testDataPath = @".\..\FunctoinListParser_TestData_001.xlsx";
			var parser = new FunctionListParser();
			object functionList = parser.Parse(srcPath:testDataPath);

			Assert.IsTrue(functionList is IEnumerable<AutoTestPrep.Model.ParameterInfo>);
			var enumerableFunction = functionList as IEnumerable<AutoTestPrep.Model.ParameterInfo>;
			Assert.AreEqual(7, enumerableFunction.Count());
			Assert.AreEqual(1, enumerableFunction.ElementAt(0).Index);
			Assert.AreEqual("SampleTestData_1", enumerableFunction.ElementAt(0).Name);
			Assert.AreEqual("test_data_001", enumerableFunction.ElementAt(0).InfoName);
			Assert.AreEqual("test_data_001.cpp", enumerableFunction.ElementAt(0).FileName);
			Assert.AreEqual(7, enumerableFunction.ElementAt(6).Index);
			Assert.AreEqual("SampleTestData_7", enumerableFunction.ElementAt(6).Name);
			Assert.AreEqual("test_data_007", enumerableFunction.ElementAt(6).InfoName);
			Assert.AreEqual("test_data_007.cpp", enumerableFunction.ElementAt(6).FileName);
		}
	}
}
