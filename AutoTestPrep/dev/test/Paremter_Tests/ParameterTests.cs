using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AutoTestPrep.model;
using System.Collections.Generic;

namespace Paremter_Tests
{
	[TestClass]
	public class ParameterTests
	{
		[TestMethod]
		[TestCategory("UnitTest")]
		[TestCategory("OK_Case")]
		[Description("Constructor")]
		public void Parameter_Constructor_001()
		{
			var parameter = new Parameter();

			Assert.IsTrue(string.IsNullOrEmpty(parameter.Postifx));
			Assert.IsTrue(string.IsNullOrEmpty(parameter.Name));
			Assert.IsTrue(string.IsNullOrEmpty(parameter.DataType));
			Assert.IsTrue(string.IsNullOrEmpty(parameter.Postifx));
			Assert.AreEqual(0, parameter.PointerNum);
			Assert.AreEqual(Parameter.AccessMode.None, parameter.Mode);
			Assert.IsTrue(string.IsNullOrEmpty(parameter.Overview));
			Assert.IsTrue(string.IsNullOrEmpty(parameter.Description));
			Assert.IsNull(parameter.SubParameters);
		}

		[TestMethod]
		[TestCategory("Parameter")]
		[TestCategory("UnitTest")]
		[TestCategory("OK_Case")]
		[Description("Copy constructor")]
		public void Parameter_Constructor_002()
		{
			var subParam1 = new Parameter
			{
				Name = "subParam1"
			};
			var subParam2 = new Parameter
			{
				Name = "subParam2"
			};

			var src = new Parameter
			{
				Prefix = "parameter_prefix",
				Name = "parameter_name",
				DataType = "parameter_datatype",
				Postifx = "parameter_postfix",
				PointerNum = 1,
				Mode = Parameter.AccessMode.In,
				Overview = "parameter_overview",
				Description = "parameter_description",
				SubParameters = new List<Parameter>()
				{
					subParam1, subParam2
				},
			};
			var copyParam = new Parameter(src);

			Assert.AreEqual(0, string.CompareOrdinal(src.Prefix, "parameter_prefix"));
			Assert.AreEqual(0, string.CompareOrdinal(src.Name, "parameter_name"));
			Assert.AreEqual(0, string.CompareOrdinal(src.DataType, "parameter_datatype"));
			Assert.AreEqual(0, string.CompareOrdinal(src.Postifx, "parameter_postfix"));
			Assert.AreEqual(1, src.PointerNum);
			Assert.AreEqual(Parameter.AccessMode.In, copyParam.Mode);
			Assert.AreEqual(0, string.CompareOrdinal(src.Overview, "parameter_overview"));
			Assert.AreEqual(0, string.CompareOrdinal(src.Description, "parameter_description"));
			Assert.AreEqual(0, string.CompareOrdinal(src.ActualDataType,
				"parameter_prefix " +
				"parameter_datatype* " +
				"parameter_name " +
				"parameter_postfix"));
		}

		[TestMethod]
		[TestCategory("Parameter")]
		[TestCategory("UnitTest")]
		[TestCategory("OK_Case")]
		[Description("ActualDataType")]
		public void Parameter_ActualDataType_001()
		{
			var parameter = new Parameter
			{
				Prefix = "parameter_prefix",
				Name = "parameter_name",
				DataType = "parameter_datatype",
				Postifx = "parameter_postfix",
				PointerNum = 1,
			};
			Assert.AreEqual(0, string.CompareOrdinal(parameter.ActualDataType,
				"parameter_prefix " +
				"parameter_datatype* " +
				"parameter_name " +
				"parameter_postfix"));
		}

		[TestMethod]
		[TestCategory("Parameter")]
		[TestCategory("UnitTest")]
		[TestCategory("OK_Case")]
		[Description("ActualDataType")]
		public void Parameter_ActualDataType_002()
		{
			var parameter = new Parameter
			{
				Prefix = "parameter_prefix",
				Name = "parameter_name",
				DataType = "parameter_datatype",
				Postifx = "parameter_postfix",
				PointerNum = 0,
			};
			Assert.AreEqual(0, string.CompareOrdinal(parameter.ActualDataType,
				"parameter_prefix " +
				"parameter_datatype " +
				"parameter_name " +
				"parameter_postfix"));
		}

		[TestMethod]
		[TestCategory("Parameter")]
		[TestCategory("UnitTest")]
		[TestCategory("OK_Case")]
		[Description("ActualDataType")]
		public void Parameter_ActualDataType_003()
		{
			var parameter = new Parameter
			{
				Name = "parameter_name",
				DataType = "parameter_datatype",
				Postifx = "parameter_postfix",
				PointerNum = 1,
			};
			Assert.AreEqual(0, string.CompareOrdinal(parameter.ActualDataType,
				"parameter_datatype* " +
				"parameter_name " +
				"parameter_postfix"));
		}

		[TestMethod]
		[TestCategory("Parameter")]
		[TestCategory("UnitTest")]
		[TestCategory("OK_Case")]
		[Description("ActualDataType")]
		public void Parameter_ActualDataType_004()
		{
			var parameter = new Parameter
			{
				Prefix = "parameter_prefix",
				Name = "parameter_name",
				DataType = "parameter_datatype",
				PointerNum = 0,
			};
			Assert.AreEqual(0, string.CompareOrdinal(parameter.ActualDataType,
				"parameter_prefix " +
				"parameter_datatype " +
				"parameter_name"));
		}

		[TestMethod]
		[TestCategory("Parameter")]
		[TestCategory("UnitTest")]
		[TestCategory("OK_Case")]
		[Description("ToString")]
		public void Parameter_ToString_001()
		{
			var subParam1 = new Parameter
			{
				DataType = "DataType1",
				Name = "Name1"
			};
			var parameter = new Parameter
			{
				DataType = "DataType",
				Name = "Name",
				SubParameters = new List<Parameter>()
				{
					subParam1
				},
			};
			var paramString = parameter.ToString();

			Assert.AreEqual("DataType Name(DataType1 Name1)", paramString);
		}
	}
}
