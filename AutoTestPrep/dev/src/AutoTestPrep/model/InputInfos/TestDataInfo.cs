using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.InputInfos
{
	/// <summary>
	/// Model class of data data.
	/// </summary>
	public class TestDataInfo
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public TestDataInfo()
		{
			this.TestDataFilePath = string.Empty;
			this.OutputDirectoryPath = string.Empty;
			this.StubBufferSize1 = 0;
			this.StubBufferSize2 = 0;

			this.DriverIncludeStandardHeaderFiles = null;
			this.DriverIncludeUserHeaderFiles = null;
		}

		/// <summary>
		/// Path to test design file.
		/// </summary>
		public string TestDataFilePath { get; set; }

		/// <summary>
		/// Path to directoty to output.
		/// </summary>
		public string OutputDirectoryPath { get; set; }

		/// <summary>
		/// Stub Buffer size
		/// </summary>
		public long StubBufferSize1 { get; set; }

		/// <summary>
		/// Stub buffer size
		/// </summary>
		public long StubBufferSize2 { get; set; }

		/// <summary>
		/// Collection of standard header files.
		/// </summary>
		public IEnumerable<string> DriverIncludeStandardHeaderFiles;

		/// <summary>
		/// Collection of user header files.
		/// </summary>
		public IEnumerable<string> DriverIncludeUserHeaderFiles;
	}
}
