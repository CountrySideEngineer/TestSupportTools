using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Target;

namespace CodeGenerator.Stub.Template
{
	public class StubTemplate
	{
		protected virtual string CreateFunctionReturnBufferDeclare(Function function)
		{
			return nameof(CreateFunctionReturnBufferDeclare);
		}

		protected virtual string CreateArgumentBufferDeclare(Function function, Parameter argument)
		{
			return nameof(CreateArgumentBufferDeclare);
		}

		protected virtual string CreateOutputBufferDeclare(Function function, Parameter argument)
		{
			return function.Name;
		}

		protected virtual string CreateInitializeFunctionDeclare(Function function)
		{
			return string.Empty;
		}

		protected virtual string CreateOutputBufferName(Function function, Parameter argument)
		{
			return string.Empty;
		}

		protected virtual string CreateFunctionReturnBufferName(Function function)
		{
			return string.Empty;
		}

		protected virtual string CreateFunctionCalledBufferName(Function function)
		{
			return string.Empty;
		}
	}
}
