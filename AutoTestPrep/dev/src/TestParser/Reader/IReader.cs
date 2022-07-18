using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestParse.Reader
{
	public interface IReader
	{
		object Read(string path);

		object Read(TextReader stream);
	}
}
