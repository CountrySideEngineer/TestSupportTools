using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Target;

namespace TestParser.Conveter
{
	interface IFunctionTableItemConverter
	{
		void Convert(IEnumerable<string> src, ref Parameter dst);
	}
}
