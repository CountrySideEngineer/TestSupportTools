using AutoTestPrep.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Function_test
{
	[TestClass]
	public class Function_test
	{
		[TestMethod]
		[TestCategory("UnitTest")]
		[TestCategory("OK_Case")]
		[Description("Constructor")]
		public void Functoin_ToString_001()
		{
			var function = new Function
			{
				Name = "FuncName",
				DataType = "int"
			};

			string toString = function.ToString();

			Assert.AreEqual("int FuncName()", toString);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		[TestCategory("OK_Case")]
		[Description("Constructor")]
		public void Functoin_ToString_002()
		{
			var arguments = new List<Parameter>
			{
				new Parameter
				{
					Name = "Arg1",
					DataType = "int",
					PointerNum = 0
				},
			};
			var function = new Function
			{
				Name = "FuncName",
				DataType = "int",
				Arguments = arguments
			};

			string toString = function.ToString();

			Assert.AreEqual("int FuncName(int Arg1)", toString);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		[TestCategory("OK_Case")]
		[Description("Constructor")]
		public void Functoin_ToString_003()
		{
			var arguments = new List<Parameter>
			{
				new Parameter
				{
					Name = "Arg1",
					DataType = "int",
					PointerNum = 0
				},
				new Parameter
				{
					Name = "Arg2",
					DataType = "int",
					PointerNum = 1
				},
			};
			var function = new Function
			{
				Name = "FuncName",
				DataType = "int",
				Arguments = arguments
			};
			string toString = function.ToString();

			Assert.AreEqual("int FuncName(int Arg1, int* Arg2)", toString);
		}
	}
}
