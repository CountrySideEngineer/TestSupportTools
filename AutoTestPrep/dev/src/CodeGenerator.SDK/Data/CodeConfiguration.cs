using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWriter.Data
{
	/// <summary>
	/// Configuration of code.
	/// </summary>
	public class CodeConfiguration
	{
		/// <summary>
		/// Path to directory to output code.
		/// </summary>
		public string OutputDirPath { get; set; }

		/// <summary>
		/// Buffer size for stub.
		/// </summary>
		public int BufferSize1 { get; set; }

		/// <summary>
		/// Buffer size for stub.
		/// </summary>
		public int BufferSize2 { get; set; }

		/// <summary>
		/// Collection of standard header file to include.
		/// </summary>
		public IEnumerable<string> StandardHeaderFiles;

		/// <summary>
		/// Collection of user header files to include.
		/// </summary>
		public IEnumerable<string> UserHeaderFiles;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public CodeConfiguration()
		{
			OutputDirPath = @"C:\";
			this.BufferSize1 = 100;
			this.BufferSize2 = 100;

			this.StandardHeaderFiles = new List<string>();
			this.UserHeaderFiles = new List<string>();
		}

		/// <summary>
		/// Copy constructor.
		/// </summary>
		/// <param name="src">Copy source</param>
		/// <exception cref="NullReferenceException"></exception>
		public CodeConfiguration(CodeConfiguration src)
		{
			try
			{
				this.OutputDirPath = src.OutputDirPath;
				this.BufferSize1 = src.BufferSize1;
				this.BufferSize2 = src.BufferSize2;
				this.StandardHeaderFiles = new List<string>(src.StandardHeaderFiles);
				this.UserHeaderFiles = new List<string>(src.UserHeaderFiles);
			}
			catch (NullReferenceException)
			{
				//A case that the "src", or list of header file name collection are null.
				throw;
			}
		}
	}
}
