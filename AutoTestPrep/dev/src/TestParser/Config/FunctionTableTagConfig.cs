using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestParser.Config
{
	public class FunctionTableTagConfig
	{
		public string TargetFunctionTag { get; set; }
		public string SubFunctionTag { get; set; }
		public string GlobalVariableTag { get; set; }
		public string BodyContentTag { get; set; }
		public string ArgumentTag { get; set; }
		public string InternalTag { get; set; }
		public string ExternalTag { get; set; }
	}
}
