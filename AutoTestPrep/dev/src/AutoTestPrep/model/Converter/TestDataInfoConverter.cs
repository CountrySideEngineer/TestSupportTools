using AutoTestPrep.Model.InputInfos;
using StubDriverPlugin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Converter
{
	public class TestDataInfoConverter
	{
		public PluginInput ToPluginInput(TestDataInfo testDataInfo)
		{
			PluginInput pluginInput = new PluginInput()
			{
				InputFilePath = testDataInfo.TestDataFilePath,
				OutputDirPath = testDataInfo.OutputDirectoryPath,
				StubBufferSize1 = (ulong)testDataInfo.StubBufferSize1,
				StubBufferSize2 = (ulong)testDataInfo.StubBufferSize2,
				StubIncludeStandardHeaderFiles = new List<string>(testDataInfo.StubIncludeStandardHeaderFiles),
				StubIncludeUserHeaderFiles = new List<string>(testDataInfo.StubIncludeUserHeaderFiles),
				DriverIncludeStandardHeaderFiles = new List<string>(testDataInfo.DriverIncludeStandardHeaderFiles),
				DriverIncludeUserHeaderFiles = new List<string>(testDataInfo.DriverIncludeUserHeaderFiles)
			};
			return pluginInput;
		}
	}
}
