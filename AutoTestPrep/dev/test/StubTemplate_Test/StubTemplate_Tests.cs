using CodeWriter.Template.Stub;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestParser.Target;

namespace StubTemplate_Test
{
	[TestClass]
	public class StubTemplate_Tests
	{
		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateFunctionCalledBufferName_Test_001()
		{
			var function = new Function()
			{
				Name = "sample_function"
			};
			StubTemplate template = new StubTemplate();
			PrivateObject privateTemplate = new PrivateObject(template);
			string buffName = (string)privateTemplate.Invoke("CreateFunctionCalledBufferName", function);

			Assert.AreEqual("sample_function_called_count", buffName);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateArgumentBufferName_Test_001()
		{
			var function = new Function()
			{
				Name = "SampleFunction"
			};
			var argument = new Parameter()
			{
				Name = "SampleArgument"
			};
			StubTemplate template = new StubTemplate();
			PrivateObject privateTemplate = new PrivateObject(template);
			string buffName = (string)privateTemplate.Invoke("CreateArgumentBufferName", function, argument);

			Assert.AreEqual("SampleFunction_SampleArgument", buffName);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateFunctionReturnBufferName_Test_001()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "int"
			};
			StubTemplate template = new StubTemplate();
			PrivateObject privateTemplate = new PrivateObject(template);
			string buffName = (string)privateTemplate.Invoke("CreateFunctionReturnBufferName", function);

			Assert.AreEqual("SampleFunction_return_value", buffName);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateFunctionReturnBufferName_Test_002()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "void"
			};
			StubTemplate template = new StubTemplate();
			PrivateObject privateTemplate = new PrivateObject(template);
			string buffName = (string)privateTemplate.Invoke("CreateFunctionReturnBufferName", function);

			Assert.IsTrue(string.IsNullOrEmpty(buffName));
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateFunctionReturnBufferName_Test_003()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "VOID"
			};
			StubTemplate template = new StubTemplate();
			PrivateObject privateTemplate = new PrivateObject(template);
			string buffName = (string)privateTemplate.Invoke("CreateFunctionReturnBufferName", function);

			Assert.IsTrue(string.IsNullOrEmpty(buffName));
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateOutputBufferName_Test_001()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "VOID"
			};
			var argument = new Parameter()
			{
				Name = "SampleArgument"
			};
			StubTemplate template = new StubTemplate();
			PrivateObject privateTemplate = new PrivateObject(template);
			string buffName = (string)privateTemplate.Invoke("CreateOutputBufferName", function, argument);

			Assert.AreEqual("SampleFunction_SampleArgument_value", buffName);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateFunctoinCalledCountBufferDeclare_Test_001()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "VOID"
			};
			StubTemplate template = new StubTemplate();
			PrivateObject privateTemplate = new PrivateObject(template);
			string buffName = (string)privateTemplate.Invoke("CreateFunctoinCalledCountBufferDeclare", function);

			Assert.AreEqual("int SampleFunction_called_count", buffName);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateArgumentBufferDeclare_Test_001()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "int"
			};
			StubTemplate template = new StubTemplate();
			PrivateObject privateTemplate = new PrivateObject(template);
			string buffName = (string)privateTemplate.Invoke("CreateFunctionReturnBufferDeclare", function);

			Assert.AreEqual("int SampleFunction_return_value", buffName);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateArgumentBufferDeclare_Test_002()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "void"
			};
			StubTemplate template = new StubTemplate();
			PrivateObject privateTemplate = new PrivateObject(template);
			string buffName = (string)privateTemplate.Invoke("CreateFunctionReturnBufferDeclare", function);

			Assert.IsTrue(string.IsNullOrEmpty(buffName));
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateArgumentBufferDeclare_Test_003()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "VOID"
			};
			StubTemplate template = new StubTemplate();
			PrivateObject privateTemplate = new PrivateObject(template);
			string buffName = (string)privateTemplate.Invoke("CreateFunctionReturnBufferDeclare", function);

			Assert.IsTrue(string.IsNullOrEmpty(buffName));
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateOutputBufferDeclare_Test_001()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "int",
			};
			var argument = new Parameter()
			{
				Name = "SampleArgument",
				PointerNum = 1,
				Mode = Parameter.AccessMode.Out,
				DataType = "SHORT"
			};
			StubTemplate template = new StubTemplate();
			PrivateObject privateTemplate = new PrivateObject(template);
			string buffName = (string)privateTemplate.Invoke("CreateOutputBufferDeclare", function, argument);

			Assert.AreEqual("SHORT SampleFunction_SampleArgument_value", buffName);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateOutputBufferDeclare_Test_002()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "int",
			};
			var argument = new Parameter()
			{
				Name = "SampleArgument",
				PointerNum = 2,
				Mode = Parameter.AccessMode.Out,
				DataType = "SHORT"
			};
			StubTemplate template = new StubTemplate();
			PrivateObject privateTemplate = new PrivateObject(template);
			string buffName = (string)privateTemplate.Invoke("CreateOutputBufferDeclare", function, argument);

			Assert.AreEqual("SHORT SampleFunction_SampleArgument_value", buffName);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateOutputBufferDeclare_Test_003()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "int",
			};
			var argument = new Parameter()
			{
				Name = "SampleArgument",
				PointerNum = 2,
				Mode = Parameter.AccessMode.Both,
				DataType = "SHORT"
			};
			StubTemplate template = new StubTemplate();
			PrivateObject privateTemplate = new PrivateObject(template);
			string buffName = (string)privateTemplate.Invoke("CreateOutputBufferDeclare", function, argument);

			Assert.AreEqual("SHORT SampleFunction_SampleArgument_value", buffName);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateOutputBufferDeclare_Test_004()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "int",
			};
			var argument = new Parameter()
			{
				Name = "SampleArgument",
				PointerNum = 2,
				Mode = Parameter.AccessMode.In,
				DataType = "SHORT"
			};
			StubTemplate template = new StubTemplate();
			PrivateObject privateTemplate = new PrivateObject(template);
			string buffName = (string)privateTemplate.Invoke("CreateOutputBufferDeclare", function, argument);

			Assert.IsTrue(string.IsNullOrEmpty(buffName));
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateOutputBufferDeclare_Test_005()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "int",
			};
			var argument = new Parameter()
			{
				Name = "SampleArgument",
				PointerNum = 0,
				Mode = Parameter.AccessMode.In,
				DataType = "SHORT"
			};
			StubTemplate template = new StubTemplate();
			PrivateObject privateTemplate = new PrivateObject(template);
			string buffName = (string)privateTemplate.Invoke("CreateOutputBufferDeclare", function, argument);

			Assert.IsTrue(string.IsNullOrEmpty(buffName));
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateInitializeFunctionName_Test_001()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "int",
			};
			StubTemplate template = new StubTemplate();
			PrivateObject privateTemplate = new PrivateObject(template);
			string buffName = (string)privateTemplate.Invoke("CreateInitializeFunctionName", function);

			Assert.AreEqual("SampleFunction_init", buffName);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateInitializeFunctionDeclare_Test_001()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "int",
			};
			StubTemplate template = new StubTemplate();
			PrivateObject privateTemplate = new PrivateObject(template);
			string buffName = (string)privateTemplate.Invoke("CreateInitializeFunctionDeclare", function);

			Assert.AreEqual("void SampleFunction_init", buffName);
		}
	}
}
