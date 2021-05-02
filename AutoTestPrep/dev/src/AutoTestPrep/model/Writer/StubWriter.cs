using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AutoTestPrep.Model.Tempaltes;

namespace AutoTestPrep.Model.Writer
{
	/// <summary>
	/// Class to write code of function stub, or mock into text file.
	/// </summary>
	public class StubWriter : IWriter
	{
		public void Write(string path, object parameter)
		{
			Function function = (Function)parameter;
			var template = new CFunctionStubTemplate(function);

			using (var stream = new StreamWriter(path, false, Encoding.Unicode))
			{
				stream.Write(template.TransformText());
			}
		}
	}
}
