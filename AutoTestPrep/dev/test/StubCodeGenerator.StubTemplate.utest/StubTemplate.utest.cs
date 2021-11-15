using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestParser.Target;
using CodeGenerator.Stub.Template;

namespace StubCodeGenerator.Stub.Template.utest
{
	[TestClass]
	public class StubTemplate_utest
	{
		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateFunctionCalledBufferName_test_001()
		{
			var function = new Function()
			{
				Name = "SampleFunction"
			};
			var template = new StubTemplate();
			var templatePrivate = new PrivateObject(template);
			var buffName = (string)templatePrivate.Invoke("CreateFunctionCalledBufferName", function);

			Assert.AreEqual("SampleFunction_called_count", buffName);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateArgumentBufferName_test_001()
		{
			var function = new Function()
			{
				Name = "SampleFunction"
			};
			var argument = new Parameter()
			{
				Name = "SampleArgument"
			};
			var template = new StubTemplate();
			var templatePrivate = new PrivateObject(template);
			var buffName = (string)templatePrivate.Invoke("CreateArgumentBufferName", function, argument);

			Assert.AreEqual("SampleFunction_SampleArgument", buffName);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateFunctionReturnBufferName_test_001()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "NotVoid"
			};
			var template = new StubTemplate();
			var templatePrivate = new PrivateObject(template);
			var buffName = (string)templatePrivate.Invoke("CreateFunctionReturnBufferName", function);

			Assert.AreEqual("SampleFunction_return_value", buffName);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateFunctionReturnBufferName_test_002()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "void"
			};
			var template = new StubTemplate();
			var templatePrivate = new PrivateObject(template);
			var buffName = (string)templatePrivate.Invoke("CreateFunctionReturnBufferName", function);

			Assert.IsTrue(string.IsNullOrEmpty(buffName));
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateFunctionReturnBufferName_test_003()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "VOID"
			};
			var template = new StubTemplate();
			var templatePrivate = new PrivateObject(template);
			var buffName = (string)templatePrivate.Invoke("CreateFunctionReturnBufferName", function);

			Assert.IsTrue(string.IsNullOrEmpty(buffName));
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateFunctionReturnBufferName_test_004()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "oid"
			};
			var template = new StubTemplate();
			var templatePrivate = new PrivateObject(template);
			var buffName = (string)templatePrivate.Invoke("CreateFunctionReturnBufferName", function);

			Assert.AreEqual("SampleFunction_return_value", buffName);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateFunctionReturnBufferName_test_005()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "voi"
			};
			var template = new StubTemplate();
			var templatePrivate = new PrivateObject(template);
			var buffName = (string)templatePrivate.Invoke("CreateFunctionReturnBufferName", function);

			Assert.AreEqual("SampleFunction_return_value", buffName);
		}
	}
}
