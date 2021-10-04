using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestParser.Target;

namespace StubCodeGenerator.StubSourceBodyTemplate.utest
{
	[TestClass]
	public class StubSourceBodyTemplate_utest
	{
		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateFunctionReturnBufferDeclare_utest_001()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "int"
			};
			var template = new CodeGenerator.Stub.Template.StubSourceBodyTemplate();
			var privateTemplate = new PrivateObject(template);
			string code = (string)privateTemplate.Invoke("CreateFunctionReturnBufferDeclare", function);

			Assert.AreEqual("int SampleFunction_return_value[STUB_BUFFER_SIZE_1]", code);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateFunctionReturnBufferDeclare_utest_002()
		{
			var function = new Function()
			{
				DataType = "void"
			};
			var template = new CodeGenerator.Stub.Template.StubSourceBodyTemplate();
			var privateTemplate = new PrivateObject(template);
			string code = (string)privateTemplate.Invoke("CreateFunctionReturnBufferDeclare", function);

			Assert.IsTrue(string.IsNullOrEmpty(code));
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateArgumentBufferDeclare_utest_001()
		{
			var function = new Function()
			{
				Name = "SampleFunction"
			};
			var argument = new Parameter()
			{
				Name = "SampleArg",
				DataType = "int"
			};

			var template = new CodeGenerator.Stub.Template.StubSourceBodyTemplate();
			var privateTemplate = new PrivateObject(template);
			string code = (string)privateTemplate.Invoke("CreateArgumentBufferDeclare", function, argument);

			Assert.AreEqual("int SampleFunction_SampleArg[STUB_BUFFER_SIZE_1]", code);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateOutputBufferDeclare_utest_001()
		{
			var function = new Function()
			{
				Name = "SampleFunction"
			};
			var argument = new Parameter()
			{
				Name = "SampleArg",
				DataType = "int",
				PointerNum = 1,
				Mode = Parameter.AccessMode.Out
			};

			var template = new CodeGenerator.Stub.Template.StubSourceBodyTemplate();
			var privateTemplate = new PrivateObject(template);
			string code = (string)privateTemplate.Invoke("CreateOutputBufferDeclare", function, argument);

			Assert.AreEqual("int SampleFunction_SampleArg_value[STUB_BUFFER_SIZE_1][STUB_BUFFER_SIZE_2]", code);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateOutputBufferDeclare_utest_002()
		{
			var function = new Function()
			{
				Name = string.Empty
			};
			var argument = new Parameter()
			{
				Name = "ArgName"
			};
			var template = new CodeGenerator.Stub.Template.StubSourceBodyTemplate();
			var privateTemplate = new PrivateObject(template);
			string code = (string)privateTemplate.Invoke("CreateOutputBufferDeclare", function, argument);

			Assert.AreEqual("//ArgName is not output.", code);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateOutputArgumentInitialize_utest_001()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "short"
			};
			var argument = new Parameter()
			{
				Name = "SampleArg",
				DataType = "int",
				PointerNum = 1,
				Mode = Parameter.AccessMode.Out
			};
			var template = new CodeGenerator.Stub.Template.StubSourceBodyTemplate();
			var privateTemplate = new PrivateObject(template);
			string code = (string)privateTemplate.Invoke("CreateOutputArgumentInitialize", function, argument);

			Assert.AreEqual("SampleFunction_SampleArg_value[index][index2]", code);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateOutputArgumentInitialize_utest_002()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "short"
			};
			var argument = new Parameter()
			{
				Name = "SampleArg",
				DataType = "int",
				PointerNum = 2,
				Mode = Parameter.AccessMode.Out
			};
			var template = new CodeGenerator.Stub.Template.StubSourceBodyTemplate();
			var privateTemplate = new PrivateObject(template);
			string code = (string)privateTemplate.Invoke("CreateOutputArgumentInitialize", function, argument);

			Assert.AreEqual("SampleFunction_SampleArg_value[index][index2]", code);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateOutputArgumentInitialize_utest_003()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "short"
			};
			var argument = new Parameter()
			{
				Name = "SampleArg",
				DataType = "int",
				PointerNum = 1,
				Mode = Parameter.AccessMode.Both
			};
			var template = new CodeGenerator.Stub.Template.StubSourceBodyTemplate();
			var privateTemplate = new PrivateObject(template);
			string code = (string)privateTemplate.Invoke("CreateOutputArgumentInitialize", function, argument);

			Assert.AreEqual("SampleFunction_SampleArg_value[index][index2]", code);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateOutputArgumentInitialize_utest_004()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "short"
			};
			var argument = new Parameter()
			{
				Name = "SampleArg",
				DataType = "int",
				PointerNum = 2,
				Mode = Parameter.AccessMode.Both
			};
			var template = new CodeGenerator.Stub.Template.StubSourceBodyTemplate();
			var privateTemplate = new PrivateObject(template);
			string code = (string)privateTemplate.Invoke("CreateOutputArgumentInitialize", function, argument);

			Assert.AreEqual("SampleFunction_SampleArg_value[index][index2]", code);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateOutputArgumentInitialize_utest_005()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "short"
			};
			var argument = new Parameter()
			{
				Name = "SampleArg",
				DataType = "int",
				PointerNum = 2,
				Mode = Parameter.AccessMode.In
			};
			var template = new CodeGenerator.Stub.Template.StubSourceBodyTemplate();
			var privateTemplate = new PrivateObject(template);
			string code = (string)privateTemplate.Invoke("CreateOutputArgumentInitialize", function, argument);

			Assert.AreEqual("//SampleArg is not output.", code);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateOutputArgumentInitialize_utest_006()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "short"
			};
			var argument = new Parameter()
			{
				Name = "SampleArg",
				DataType = "int",
				PointerNum = 0,
				Mode = Parameter.AccessMode.In
			};
			var template = new CodeGenerator.Stub.Template.StubSourceBodyTemplate();
			var privateTemplate = new PrivateObject(template);
			string code = (string)privateTemplate.Invoke("CreateOutputArgumentInitialize", function, argument);

			Assert.AreEqual("//SampleArg is not output.", code);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateLatchReturnValueCode_utest_001()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "short"
			};
			var template = new CodeGenerator.Stub.Template.StubSourceBodyTemplate();
			var privateTemplate = new PrivateObject(template);
			string code = (string)privateTemplate.Invoke("CreateLatchReturnValueCode", function);

			Assert.AreEqual("short latchReturn = SampleFunction_return_value[SampleFunction_called_count]", code);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateLatchReturnValueCode_utest_002()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "void"
			};
			var template = new CodeGenerator.Stub.Template.StubSourceBodyTemplate();
			var privateTemplate = new PrivateObject(template);
			string code = (string)privateTemplate.Invoke("CreateLatchReturnValueCode", function);

			Assert.AreEqual("//SampleFunction does not return value.", code);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateReturnLatchedValueCode_utest_001()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "short"
			};
			var template = new CodeGenerator.Stub.Template.StubSourceBodyTemplate();
			var privateTemplate = new PrivateObject(template);
			string code = (string)privateTemplate.Invoke("CreateReturnLatchedValueCode", function);

			Assert.AreEqual("return latchReturn", code);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void CreateReturnLatchedValueCode_utest_002()
		{
			var function = new Function()
			{
				Name = "SampleFunction",
				DataType = "void"
			};
			var template = new CodeGenerator.Stub.Template.StubSourceBodyTemplate();
			var privateTemplate = new PrivateObject(template);
			string code = (string)privateTemplate.Invoke("CreateReturnLatchedValueCode", function);

			Assert.AreEqual("//SampleFunction does not return value.", code);
		}
	}
}
