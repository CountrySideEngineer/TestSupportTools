using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plugin;
using Plugin.TestStubDriver;
using StubDriverPlugin;
using StubDriverPlugin.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PluginManagerSample
{
	/// <summary>
	/// PluginMangerTest_Load の概要の説明
	/// </summary>
	[TestClass]
	public class PluginMangerTest_Load
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
		public void Load_test_001()
		{
			string filePath = @"./db/Load_test_001.plgin";
			string tableName = "Load_test_001";
			var manager = new PluginManager(filePath, tableName);

			PluginInfo pluginInfo1 = new PluginInfo()
			{
				Name = "Load_test_001",
				FileName = @"PluginSampleA.dll"
			};

			manager.Regist(pluginInfo1);
			IStubDriverPlugin plugin = manager.Load(0);
			PluginOutput output = plugin.Execute(null);

			Assert.AreEqual("PluginSampleDLL_A", output.Message);
		}

		[TestMethod]
		public void Load_test_002()
		{
			string filePath = @"./db/Load_test_002.plgin";
			string tableName = "Load_test_002";
			var manager = new PluginManager(filePath, tableName);

			PluginInfo pluginInfo1 = new PluginInfo()
			{
				Name = "Load_test_002_001",
				FileName = @"PluginSampleA.dll"
			};
			manager.Regist(pluginInfo1);

			PluginInfo pluginInfo2 = new PluginInfo()
			{
				Name = "Load_test_002_002",
				FileName = @"PluginSampleB.dll"
			};
			manager.Regist(pluginInfo2);

			IStubDriverPlugin plugin = manager.Load(0);
			PluginOutput output = plugin.Execute(null);
			Assert.AreEqual("PluginSampleDLL_A", output.Message);

			plugin = manager.Load(1);
			output = plugin.Execute(null);
			Assert.AreEqual("PluginSampleDLL_B", output.Message);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Load_test_003()
		{
			string filePath = @"./db/Load_test_003.plgin";
			string tableName = "Load_test_003";
			var manager = new PluginManager(filePath, tableName);

			manager.Load(0);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Load_test_004()
		{
			string filePath = @"./db/Load_test_004.plgin";
			string tableName = "Load_test_004";
			var manager = new PluginManager(filePath, tableName);

			PluginInfo pluginInfo1 = new PluginInfo()
			{
				Name = "Load_test_004",
				FileName = @"PluginSampleA.dll"
			};

			manager.Regist(pluginInfo1);
			manager.Load(1);
		}

		[TestMethod]
		[ExpectedException(typeof(FileNotFoundException))]
		public void Load_test_005()
		{
			string filePath = @"./db/Load_test_005.plgin";
			string tableName = "Load_test_005";
			var manager = new PluginManager(filePath, tableName);

			PluginInfo pluginInfo1 = new PluginInfo()
			{
				Name = "Load_test_005",
				FileName = @"unknown.dll"
			};

			manager.Regist(pluginInfo1);
			manager.Load(0);
		}
	}
}
