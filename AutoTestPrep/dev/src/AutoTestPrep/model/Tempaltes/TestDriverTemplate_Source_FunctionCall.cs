using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Tempaltes
{
	partial class TestDriverTemplate_Source_FunctionCall
	{
		/// <summary>
		/// Test data
		/// </summary>
		public Test Test { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="test">Test data.</param>
		public TestDriverTemplate_Source_FunctionCall(Test test)
		{
			this.Test = test;
		}

		public string TransformText()
		{
			var lowerDataType = this.Test.Target.DataType.ToLower();

			string functionCall = string.Empty;
			if (!(("void").Equals(lowerDataType, StringComparison.Ordinal)))
			{
				functionCall += this.Test.Target.DataType;
				functionCall += " ";
				functionCall += "returnValue = ";
			}
			functionCall += this.Test.Target.Name;
			functionCall += "(";
			if (null != this.Test.Target.Arguments)
			{
				bool isTop = true;
				foreach (var argument in this.Test.Target.Arguments)
				{
					if (!isTop)
					{
						functionCall += ",";
						functionCall += " ";
					}
					functionCall += argument.Name;
					isTop = false;
				}
			}
			functionCall += ")";

			functionCall = "\t" + functionCall;
			return functionCall;
		}
	}
}
