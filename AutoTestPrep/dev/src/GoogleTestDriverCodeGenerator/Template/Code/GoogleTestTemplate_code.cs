using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Data;
using TestParser.Target;

namespace CodeGenerator.TestDriver.Template
{
    public partial class GoogleTestTemplate
    {
        /// <summary>
        /// Create a unit test class name.
        /// </summary>
        /// <param name="function">Target function data</param>
        /// <returns>Unite test class name.</returns>
        protected virtual string TestClassName(Function function)
		{
            string testClassName = string.Empty;
            testClassName = $"{function.Name}_utest";

            return testClassName;
		}

        /// <summary>
        /// Create test function name for unit test.
        /// </summary>
        /// <param name="caseNumber">Test case number.</param>
        /// <param name="function">Target function data.</param>
        /// <returns>Test function name for unit test.</returns>
        protected virtual string TestCaseMethodName(int caseNumber, Function function)
		{
            string testCaseMethodName = string.Empty;
            testCaseMethodName = $"{function.Name}_utest_{caseNumber.ToString("D3")}";
            return testCaseMethodName;
		}

        /// <summary>
        /// Create test function name for unit test.
        /// </summary>
        /// <param name="function">Target function data</param>
        /// <param name="testCase">Test case data.</param>
        /// <returns>Test function name for unit test.</returns>
        protected virtual string TestCaseMethodName(Function function, TestCase testCase)
		{
            string testCaseMethodName = string.Empty;
            testCaseMethodName = $"{function.Name}_utest_{testCase.Id}";
            return testCaseMethodName;
        }
    }
}
