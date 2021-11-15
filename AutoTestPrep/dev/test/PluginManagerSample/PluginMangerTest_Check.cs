using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Plugin;
using Plugin.TestStubDriver;
using System.Linq;
using System.IO;

namespace PluginManagerSample
{
	/// <summary>
	/// PluginMangerTest_Check の概要の説明
	/// </summary>
	[TestClass]
	public class PluginMangerTest_Check
	{
		[ClassInitialize]
		public static void ClassSetUp(TestContext testContext)
		{
			//Check whether the output directory exists.
			bool isExists = Directory.Exists(@"./db/");
			if (!isExists)
			{
				Directory.CreateDirectory(@"./db");
			}
		}

		[TestMethod]
		public void Check_test_001()
		{
			string filePath = @"./db/Check_test_001.plgin";
			string tableName = "Check_test_001";
			var manager = new PluginManager(filePath, tableName);

			PluginInfo pluginInfo1 = new PluginInfo()
			{
				Name = "Check_test_001_001.Name",
				FileName = "Check_test_001_001.Name.dll"
			};
			manager.Regist(pluginInfo1);

			bool check = manager.Check(0);
			Assert.IsTrue(check);
		}

		[TestMethod]
		public void Check_test_002()
		{
			string filePath = @"./db/Check_test_002.plgin";
			string tableName = "Check_test_002";
			var manager = new PluginManager(filePath, tableName);

			PluginInfo pluginInfo1 = new PluginInfo()
			{
				Name = "Check_test_002_001.Name",
				FileName = "Check_test_002_001.Name.dll"
			};
			manager.Regist(pluginInfo1);

			bool check = manager.Check(1);
			Assert.IsFalse(check);
		}

		[TestMethod]
		public void Check_test_003()
		{
			string filePath = @"./db/Check_test_003.plgin";
			string tableName = "Check_test_003";
			var manager = new PluginManager(filePath, tableName);

			bool check = manager.Check(0);
			Assert.IsFalse(check);
		}

		[TestMethod]
		public void Check_test_004()
		{
			string filePath = @"./db/Check_test_004.plgin";
			string tableName = "Check_test_004";
			var manager = new PluginManager(filePath, tableName);

			PluginInfo pluginInfo1 = new PluginInfo()
			{
				Name = "Check_test_004_001.Name",
				FileName = "Check_test_004_001.Name.dll"
			};
			manager.Regist(pluginInfo1);

			PluginInfo pluginInfo2 = new PluginInfo()
			{
				Name = "Check_test_004_002.Name",
				FileName = "Check_test_004_002.Name.dll"
			};
			manager.Regist(pluginInfo2);

			bool check = manager.Check(0);
			Assert.IsTrue(check);
			check = manager.Check(1);
			Assert.IsTrue(check);
			check = manager.Check(2);
			Assert.IsFalse(check);
		}
	}
}
