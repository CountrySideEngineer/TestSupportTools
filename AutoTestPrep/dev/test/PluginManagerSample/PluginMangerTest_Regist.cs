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
	/// PluginMangerTest_Regist の概要の説明
	/// </summary>
	[TestClass]
	public class PluginMangerTest_Regist
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
		public void Regist_test_001()
		{
			string filePath = @"./db/Regist_test_001.plgin";
			string tableName = "Regist_test_001";
			var manager = new PluginManager(filePath, tableName);

			PluginInfo pluginInfo1 = new PluginInfo()
			{
				Name = "Regist_test_001_001.Name",
				FileName = "Regist_test_001_001.Name.dll"
			};

			PluginInfo pluginToCheck1 = new PluginInfo()
			{
				Name = "Regist_test_001_001.Name",
				FileName = "Regist_test_001_001.Name.dll"
			};

			manager.Regist(pluginInfo1);

			bool isRegistered = manager.IsRegistered(pluginToCheck1);
			Assert.IsTrue(isRegistered);
		}

		[TestMethod]
		public void Regist_test_002()
		{
			string filePath = @"./db/Regist_test_002.plgin";
			string tableName = "Regist_test_002";
			var manager = new PluginManager(filePath, tableName);

			PluginInfo pluginInfo1 = new PluginInfo()
			{
				Name = "Regist_test_002_001.Name",
				FileName = "Regist_test_002_001.Name.dll"
			};

			PluginInfo pluginInfoOverlap = new PluginInfo()
			{
				Name = "Regist_test_002_001.Name",
				FileName = "Regist_test_002_001.Name.dll"
			};

			PluginInfo pluginToCheck1 = new PluginInfo()
			{
				Name = "Regist_test_002_001.Name",
				FileName = "Regist_test_002_001.Name.dll"
			};

			manager.Regist(pluginInfo1);
			manager.Regist(pluginInfoOverlap);

			bool isRegistered = manager.IsRegistered(pluginToCheck1);
			Assert.IsTrue(isRegistered);
		}
	}
}
