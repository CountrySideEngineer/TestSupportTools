using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEngineer
{
	public class DirectoryCreator
	{
		protected string _targetDirPath;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public DirectoryCreator()
		{
			this._targetDirPath = @"C:\";
		}

		/// <summary>
		/// Constructor with argument
		/// </summary>
		/// <param name="rootDirectoryPath">Fullpath to root directory.</param>
		public DirectoryCreator(string targetDirPath)
		{
			this._targetDirPath = targetDirPath;
		}

		/// <summary>
		/// Create directory 
		/// </summary>
		/// <returns></returns>
		public DirectoryInfo Create()
		{
			return this.Create(this._targetDirPath);
		}

		/// <summary>
		/// Create directory under root directory passed by argument <para>rootDirPath</para>.
		/// </summary>
		/// <param name="rootDirPath">Path to root direcotyr.</param>
		/// <param name="subDirName">Direcotry name to create.</param>
		/// <returns>Created directory information.</returns>
		public virtual DirectoryInfo Create(string rootDirPath, string subDirName)
		{
			this._targetDirPath = rootDirPath;

			return this.Create(subDirName);
		}

		/// <summary>
		/// Create directory under root directory.
		/// </summary>
		/// <param name="subDirName">Name of directory</param>
		/// <returns>Created directory information.</returns>
		public virtual DirectoryInfo Create(string dirPath)
		{
			try
			{
				DirectoryInfo dirInfo = Directory.CreateDirectory(dirPath);

				return dirInfo;
			}
			catch (Exception ex) 
			{
				Debug.WriteLine(ex.Message);
				throw;
			}
		}
	}
}
