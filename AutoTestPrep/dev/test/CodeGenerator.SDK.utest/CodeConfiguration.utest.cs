using CodeGenerator.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeGenerator.SDK.utest
{
	[TestClass]
	public class CodeConfiguration_utest
	{
		[TestMethod]
		[TestCategory("UnitTest")]
		public void CodeConfiguration_Copy_001()
		{
			var src = new CodeConfiguration()
			{
				OutputDirPath = "SampleDir",
				BufferSize1 = 1,
				BufferSize2 = 2,
				StandardHeaderFiles = new List<string>()
				{
					"StandardHeader_001",
					"StandardHeader_002",
					"StandardHeader_003",
				},
				UserHeaderFiles = new List<string>()
				{
					"UserHeader_001",
					"UserHeader_002",
					"UserHeader_003",
				}
			};
			var dst = new CodeConfiguration(src);

			Assert.AreEqual("SampleDir", dst.OutputDirPath);
			Assert.AreEqual(1, dst.BufferSize1);
			Assert.AreEqual(2, dst.BufferSize2);
			Assert.AreEqual(3, dst.StandardHeaderFiles.Count());
			Assert.AreEqual("StandardHeader_001", dst.StandardHeaderFiles.ElementAt(0));
			Assert.AreEqual("StandardHeader_002", dst.StandardHeaderFiles.ElementAt(1));
			Assert.AreEqual("StandardHeader_003", dst.StandardHeaderFiles.ElementAt(2));
			Assert.AreEqual("UserHeader_001", dst.UserHeaderFiles.ElementAt(0));
			Assert.AreEqual("UserHeader_002", dst.UserHeaderFiles.ElementAt(1));
			Assert.AreEqual("UserHeader_003", dst.UserHeaderFiles.ElementAt(2));
		}
	}
}
