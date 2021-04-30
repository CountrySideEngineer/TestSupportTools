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
		/// Interface to write data.
		/// </summary>
		/// <param name="path">Path to </param>
		/// <param name="parameter"></param>
		void Write(string path, object parameter);
	}
}
