using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Writer
{
	public interface IWriter
	{
		/// <summary>
		/// Interface to write datas
		/// </summary>
		/// <param name="path">Path to output.</param>
		/// <param name="parameters">Collection of parameters in array.</param>
		void Write(string path, object[] parameters);
	}
}
