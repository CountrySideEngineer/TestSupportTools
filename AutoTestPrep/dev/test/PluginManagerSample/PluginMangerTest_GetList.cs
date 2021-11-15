using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plugin;
using Plugin.TestStubDriver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PluginManagerSample
{
	[TestClass]
	public class PluginMangerTest_GetList
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
		public void GetList_test_001()
		{
			string filePath = @"./db/GetList_test_001.plg";
			string tableName = "GetList_test_001";
			var manager = new PluginManager(filePath, tableName);
			IEnumerable<PluginInfo> plugins = manager.GetList();

			Assert.AreEqual(0, plugins.Count());
		}

		[TestMethod]
		public void GetList_test_002()
		{
			string filePath = @"./db/GetList_test_002.plgin";
			string tableName = "GetList_test_002";
			var manager = new PluginManager(filePath, tableName);

			PluginInfo pluginInfo = new PluginInfo()
			{
				Name = "GetList_test_002_001.Name",
				FileName = "GetList_test_002_001.Name.dll"
			};
			manager.Regist(pluginInfo);

			IEnumerable<PluginInfo> plugins = manager.GetList();
			Assert.AreEqual(1, plugins.Count());
			Assert.AreEqual("GetList_test_002_001.Name", plugins.ElementAt(0).Name);
			Assert.AreEqual("GetList_test_002_001.Name.dll", plugins.ElementAt(0).FileName);
		}

		[TestMethod]
		public void GetList_test_003()
		{
			string filePath = @"./db/GetList_test_003.plgin";
			string tableName = "GetList_test_003";
			var manager = new PluginManager(filePath, tableName);

			PluginInfo pluginInfo1 = new PluginInfo()
			{
				Name = "GetList_test_003_001.Name",
				FileName = "GetList_test_003_001.Name.dll"
			};
			manager.Regist(pluginInfo1);

			PluginInfo pluginInfo2 = new PluginInfo()
			{
				Name = "GetList_test_003_002.Name",
				FileName = "GetList_test_003_002.Name.dll"
			};
			manager.Regist(pluginInfo2);

			IEnumerable<PluginInfo> plugins = manager.GetList();
			Assert.AreEqual(2, plugins.Count());
			Assert.AreEqual("GetList_test_003_001.Name", plugins.ElementAt(0).Name);
			Assert.AreEqual("GetList_test_003_001.Name.dll", plugins.ElementAt(0).FileName);
			Assert.AreEqual("GetList_test_003_002.Name", plugins.ElementAt(1).Name);
			Assert.AreEqual("GetList_test_003_002.Name.dll", plugins.ElementAt(1).FileName);
		}
	}
}
