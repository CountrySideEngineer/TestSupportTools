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
		public void Write(string path, object[] parameters)
		{
			Test testParameter = (Test)parameters[0];
			string ext = System.IO.Path.GetExtension(testParameter.SourcePath);
			Function testFunction = testParameter.Target;
			IEnumerable<Function> subFunction = testFunction.SubFunctions;
			foreach (var subFunctionItem in subFunction)
			{
				this.Write(path, subFunctionItem, ext);
			}
		}

		protected void Write(string path, Function functionItem, string ext)
		{
			string stubFilePath = path + @"\" + functionItem.Name + "_test_stub" + ext;
			var template = new CFunctionStubTemplate(functionItem);
			using (var stream = new StreamWriter(stubFilePath, false, Encoding.Unicode))
			{
				stream.Write(template.TransformText());
			}
		}
	}
}
