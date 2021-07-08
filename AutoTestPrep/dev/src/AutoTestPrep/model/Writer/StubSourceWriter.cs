using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Writer
{
	using System.IO;
	using Tempaltes;

	public class StubSourceWriter : IWriter
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
