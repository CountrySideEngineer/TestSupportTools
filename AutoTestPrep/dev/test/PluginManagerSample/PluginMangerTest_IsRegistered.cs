using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plugin;
using Plugin.TestStubDriver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PluginManagerSample
{
	/// <summary>
	/// PluginMangerTest_IsRegistered の概要の説明
	/// </summary>
	[TestClass]
	public class PluginMangerTest_IsRegistered
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
		public void IsRegistered_test_001()
		{
			string filePath = @"./db/IsRegistered_test_001.plgin";
			string tableName = "IsRegistered_test_001";
			var manager = new PluginManager(filePath, tableName);

			PluginInfo pluginInfo1 = new PluginInfo()
			{
				Name = "IsRegistered_test_001_001.Name",
				FileName = "IsRegistered_test_001_001.Name.dll"
			};

			PluginInfo pluginToCheck1 = new PluginInfo()
			{
				Name = "IsRegistered_test_001_001.Name",
				FileName = "IsRegistered_test_001_001.Name.dll"
			};

			manager.Regist(pluginInfo1);

			bool isRegistered = manager.IsRegistered(pluginToCheck1);
			Assert.IsTrue(isRegistered);
		}

		[TestMethod]
		public void IsRegistered_test_002()
		{
			string filePath = @"./db/IsRegistered_test_002.plgin";
			string tableName = "IsRegistered_test_002";
			var manager = new PluginManager(filePath, tableName);

			PluginInfo pluginInfo1 = new PluginInfo()
			{
				Name = "IsRegistered_test_002_001.Name",
				FileName = "IsRegistered_test_002_001.Name.dll"
			};
			manager.Regist(pluginInfo1);

			PluginInfo pluginToCheck1 = new PluginInfo()
			{
				Name = "IsRegistered_test_002_002.Name",
				FileName = "IsRegistered_test_002_002.Name.dll"
			};
			bool isRegistered = manager.IsRegistered(pluginToCheck1);
			Assert.IsFalse(isRegistered);
		}
	}
}
