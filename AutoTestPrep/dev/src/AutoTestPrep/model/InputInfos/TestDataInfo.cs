using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AutoTestPrep.Model.InputInfos
{
	/// <summary>
	/// Model class of data data.
	/// </summary>
	[XmlRoot("TestDataInfo")]
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

			this.DriverIncludeStandardHeaderFiles = new List<string>(0);
			this.DriverIncludeUserHeaderFiles = new List<string>(0);
			this.StubIncludeStandardHeaderFiles = new List<string>(0);
			this.StubIncludeUserHeaderFiles = new List<string>(0);
			this.IncludeDirectoryPath = new List<string>(0);
			this.LibraryNames = new List<string>(0);
			this.LibraryDirectoryPath = new List<string>(0);
			this.DefineMacros = new List<string>(0);
			this.FrameworkTye = TestFramework.Framework.Invalid;

		}

		/// <summary>
		///	Copy constructor.
		/// </summary>
		/// <param name="src">Source TestDataInfo object</param>
		public TestDataInfo(TestDataInfo src)
		{
			this.TestDataFilePath = src.TestDataFilePath;
			this.OutputDirectoryPath = src.OutputDirectoryPath;
			this.StubBufferSize1 = src.StubBufferSize1;
			this.StubBufferSize2 = src.StubBufferSize2;

			this.DriverIncludeStandardHeaderFiles = new List<string>(src.DriverIncludeStandardHeaderFiles);
			this.DriverIncludeUserHeaderFiles = new List<string>(src.DriverIncludeUserHeaderFiles);
			this.StubIncludeStandardHeaderFiles = new List<string>(src.StubIncludeStandardHeaderFiles);
			this.StubIncludeUserHeaderFiles = new List<string>(src.StubIncludeUserHeaderFiles);
			this.IncludeDirectoryPath = new List<string>(src.IncludeDirectoryPath);
			this.LibraryNames = new List<string>(src.LibraryNames);
			this.LibraryDirectoryPath = new List<string>(src.LibraryNames);
			this.DefineMacros = new List<string>(src.DefineMacros);
			this.FrameworkTye = src.FrameworkTye;
		}

		/// <summary>
		/// Path to test design file.
		/// </summary>
		[XmlElement(nameof(TestDataFilePath))]
		public string TestDataFilePath { get; set; }

		/// <summary>
		/// Path to directoty to output.
		/// </summary>
		[XmlElement(nameof(OutputDirectoryPath))]
		public string OutputDirectoryPath { get; set; }

		/// <summary>
		/// Stub Buffer size
		/// </summary>
		[XmlElement(nameof(StubBufferSize1))]
		public long StubBufferSize1 { get; set; }

		/// <summary>
		/// Stub buffer size
		/// </summary>
		[XmlElement(nameof(StubBufferSize2))]
		public long StubBufferSize2 { get; set; }

		/// <summary>
		/// Collection of standard header files.
		/// </summary>
		[XmlIgnore]
		public IEnumerable<string> DriverIncludeStandardHeaderFiles;

		[XmlArray(nameof(DriverIncludeStandardHeaderFiles))]
		[XmlArrayItem(nameof(DriverIncludeStandardHeaderFiles) +"Item", typeof(string))]
		public string[] DriverIncludeStandardHeaderFilesSurrogate
		{
			get
			{
				return this.DriverIncludeStandardHeaderFiles.ToArray();
			}
			set
			{
				this.DriverIncludeStandardHeaderFiles = value;
			}
		}

		/// <summary>
		/// Collection of user header files.
		/// </summary>
		[XmlIgnore]
		public IEnumerable<string> DriverIncludeUserHeaderFiles;

		[XmlArray(nameof(DriverIncludeUserHeaderFiles))]
		[XmlArrayItem(nameof(DriverIncludeUserHeaderFiles) + "Item", typeof(string))]
		public string[] DriverIncludeUserHeaderFilesSurrogate
		{
			get
			{
				return this.DriverIncludeUserHeaderFiles.ToArray();
			}
			set
			{
				this.DriverIncludeUserHeaderFiles = value;
			}
		}

		/// <summary>
		/// Colelction of standard header file names being included in stub source file.
		/// </summary>
		[XmlIgnore]
		public IEnumerable<string> StubIncludeStandardHeaderFiles;

		[XmlArray(nameof(StubIncludeStandardHeaderFiles))]
		[XmlArrayItem(nameof(StubIncludeStandardHeaderFiles) +"Item", typeof(string))]
		public string[] StubIncludeStandardHeaderFilesSurrogate
		{
			get
			{
				return this.StubIncludeStandardHeaderFiles.ToArray();
			}
			set
			{
				this.StubIncludeStandardHeaderFiles = value;
			}
		}
		/// <summary>
		/// Collection of user header file names being included in stub source file.
		/// </summary>
		[XmlIgnore]
		public IEnumerable<string> StubIncludeUserHeaderFiles;

		[XmlArray(nameof(StubIncludeUserHeaderFiles))]
		[XmlArrayItem(nameof(StubIncludeUserHeaderFiles) + "Item", typeof(string))]
		public string[] StubIncludeUserHeaderFilesSurrogate
		{
			get
			{
				return this.StubIncludeUserHeaderFiles.ToArray();
			}
			set
			{
				this.StubIncludeUserHeaderFiles = value;
			}
		}
		/// <summary>
		/// Collection of directories to scan when header file including.
		/// </summary>
		[XmlIgnore]
		public IEnumerable<string> IncludeDirectoryPath;

		[XmlArray(nameof(IncludeDirectoryPath))]
		[XmlArrayItem(nameof(IncludeDirectoryPath) + "Item", typeof(string))]
		public string[] IncludeDirectoryPathSurrogate
		{
			get
			{
				return this.IncludeDirectoryPath.ToArray();
			}
			set
			{
				this.IncludeDirectoryPath = value;
			}
		}


		/// <summary>
		/// Collection of library file names.
		/// </summary>
		[XmlIgnore]
		public IEnumerable<string> LibraryNames;

		[XmlArray(nameof(LibraryNames))]
		[XmlArrayItem(nameof(LibraryNames) + "Item", typeof(string))]
		public string[] LibraryNameSurrogate
		{
			get
			{
				return this.LibraryNames.ToArray();
			}
			set
			{
				this.LibraryNames = value;
			}
		}

		/// <summary>
		/// Collection of library scan directories.
		/// </summary>
		[XmlIgnore]
		public IEnumerable<string> LibraryDirectoryPath;

		[XmlArray(nameof(LibraryDirectoryPath))]
		[XmlArrayItem(nameof(LibraryDirectoryPath) + "Item", typeof(string))]
		public string[] LibraryDirectoryPathSurrogate
		{
			get
			{
				return this.LibraryDirectoryPath.ToArray();
			}
			set
			{
				this.LibraryDirectoryPath = value;
			}
		}

		/// <summary>
		/// Collection of define macro.
		/// </summary>
		[XmlIgnore]
		public IEnumerable<string> DefineMacros;

		[XmlArray(nameof(DefineMacros))]
		[XmlArrayItem(nameof(DefineMacros) + "Item", typeof(string))]
		public string[] DefineMacrosSurrogate
		{
			get
			{
				return this.DefineMacros.ToArray();
			}
			set
			{
				this.DefineMacros = value;
			}
		}

		/// <summary>
		/// Framwork of test user selected.
		/// </summary>
		[XmlIgnore]
		public TestFramework.Framework FrameworkTye;

		[XmlElement(nameof(FrameworkTye))]
		public int FrameworkTypeSurrogate
		{
			get
			{
				return (int)this.FrameworkTye;
			}
			set
			{
				this.FrameworkTye = TestFramework.ToFramework(value);
			}
		}

		public override bool Equals(object obj)
		{
			try
			{
				TestDataInfo target = (TestDataInfo)obj;
				if ((!(this.TestDataFilePath.Equals(target.TestDataFilePath))) ||
					(!(this.OutputDirectoryPath.Equals(target.OutputDirectoryPath))) ||
					(!(this.StubBufferSize1.Equals(target.StubBufferSize1))) ||
					(!(this.StubBufferSize2.Equals(target.StubBufferSize2))))
				{
					return false;
				}
				if (!(this.DriverIncludeStandardHeaderFiles.SequenceEqual(target.DriverIncludeStandardHeaderFiles)))
				{
					return false;
				}
				if (!(this.DriverIncludeUserHeaderFiles.SequenceEqual(target.DriverIncludeUserHeaderFiles)))
				{
					return false;
				}
				if (!(this.StubIncludeStandardHeaderFiles.SequenceEqual(target.StubIncludeStandardHeaderFiles)))
				{
					return false;
				}
				if (!(this.StubIncludeUserHeaderFiles.SequenceEqual(target.StubIncludeUserHeaderFiles)))
				{
					return false;
				}
				if (!(this.IncludeDirectoryPath.SequenceEqual(target.IncludeDirectoryPath)))
				{
					return false;
				}
				if (!(this.LibraryNames.SequenceEqual(target.LibraryNames)))
				{
					return false;
				}
				if (!(this.LibraryDirectoryPath.SequenceEqual(target.LibraryDirectoryPath)))
				{
					return false;
				}
				if (!(this.DefineMacros.SequenceEqual(target.DefineMacros)))
				{
					return false;
				}
				return true;
			}
			catch (InvalidCastException)
			{
				return false;
			}
		}
	}
}
