using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StubDriverPlugin.Data
{
	public class PluginInput
	{
		/// <summary>
		/// Input test data file path.
		/// </summary>
		public string InputFilePath { get; set; }

		/// <summary>
		/// Output directory path.
		/// </summary>
		public string OutputDirPath { get; set; }

		/// <summary>
		/// Buffer size for stub.
		/// </summary>
		public ulong StubBufferSize1 { get; set; }

		/// <summary>
		/// Buffer size for stub.
		/// </summary>
		public ulong StubBuferSize2 { get; set; }

		/// <summary>
		/// Collection of standard header file names a stub file should include.
		/// </summary>
		public IEnumerable<string> StubIncludeStandardHeaderFiles { get; set; }

		/// <summary>
		/// Collection of user header file names a stub file should include
		/// </summary>
		public IEnumerable<string> StubIncludeUserHeaderFiles { get; set; }

		/// <summary>
		/// Collection of standard header file names a test driver should include.
		/// </summary>
		public IEnumerable<string> DriverIncludeStandardHeaderFiles { get; set; }

		/// <summary>
		/// Collection of user header file names a test driver should include.
		/// </summary>
		public IEnumerable<string> DriverIncludeUserHeaderFiles { get; set; }
	}
}
