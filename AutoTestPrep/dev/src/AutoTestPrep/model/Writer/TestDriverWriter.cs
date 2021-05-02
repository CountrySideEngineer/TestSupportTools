using AutoTestPrep.Model.Tempaltes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Writer
{
	public class TestDriverWriter : IWriter
	{
		/// <summary>
		/// Write test driver code into file specified by <para>path</para>.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="parameter"></param>
		public void Write(string path, object parameter)
		{
			Test test = (Test)parameter;
			var template = new TestDriverTemplate_Source_gtest(test);
			using (var stream = new StreamWriter(path, false, Encoding.UTF8))
			{
				stream.Write(template.TransformText());
			}
		}
	}
}
