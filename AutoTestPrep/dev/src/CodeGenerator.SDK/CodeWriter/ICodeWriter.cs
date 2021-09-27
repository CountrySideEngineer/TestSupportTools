using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeWriter.Data;

namespace CodeWriter
{
	public interface ICodeWriter
	{
		void Write(string path, WriteData data);
		void Write(Stream stream, WriteData data);
	}
}
