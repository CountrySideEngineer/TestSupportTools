using CodeWriter.Template.Stub;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestParser.Target;

namespace StubHeaderTemplate_test
{
	[TestClass]
	public class StubHeaderTemplate_test
	{
		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateFunctoinCalledCountBufferDeclare_test_001()
		{
			var function = new Function()
			{
				Name = "SampleFunction"
			};
			StubHeaderTemplate template = new StubHeaderTemplate();
			PrivateObject privateTemplate = new PrivateObject(template);
			string buffName = (string)privateTemplate.Invoke("CreateFunctoinCalledCountBufferDeclare", function);

			Assert.AreEqual("extern int SampleFunction_called_count;", buffName);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateArgumentBufferDeclare_test_001()
		{
			var function = new Function()
			{
				Name = "SampleFunction"
			};
			var argument = new Parameter()
			{
				Name = "SampleArgument",
				DataType = "short"
			};
			StubHeaderTemplate template = new StubHeaderTemplate();
			PrivateObject privateTemplate = new PrivateObject(template);
			string buffName = (string)privateTemplate.Invoke("CreateArgumentBufferDeclare", function, argument);

			Assert.AreEqual("extern short SampleFunction_SampleArgument[];", buffName);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateFunctionReturnBufferDeclare_test_001()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "short"
			};
			StubHeaderTemplate template = new StubHeaderTemplate();
			PrivateObject privateTemplate = new PrivateObject(template);
			string buffName = (string)privateTemplate.Invoke("CreateFunctionReturnBufferDeclare", function);

			Assert.AreEqual("extern short SampleFunction_return_value[];", buffName);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateFunctionReturnBufferDeclare_test_002()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "void"
			};
			StubHeaderTemplate template = new StubHeaderTemplate();
			PrivateObject privateTemplate = new PrivateObject(template);
			string buffName = (string)privateTemplate.Invoke("CreateFunctionReturnBufferDeclare", function);

			Assert.IsTrue(string.IsNullOrEmpty(buffName));
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateFunctionReturnBufferDeclare_test_003()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "VOID"
			};
			StubHeaderTemplate template = new StubHeaderTemplate();
			PrivateObject privateTemplate = new PrivateObject(template);
			string buffName = (string)privateTemplate.Invoke("CreateFunctionReturnBufferDeclare", function);

			Assert.IsTrue(string.IsNullOrEmpty(buffName));
		}
	}
}
