using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        protected virtual string TestClassName(Function function)
		{
            Debug.Assert(null != function, $"{nameof(GoogleTestTemplate)}.{nameof(TestClassName)}");

            try
			{
                if ((string.IsNullOrEmpty(function.Name)) || (string.IsNullOrWhiteSpace(function.Name)))
                {
                    throw new ArgumentException();
                }
                string testClassName = string.Empty;
                testClassName = $"{function.Name}_utest";

                return testClassName;
            }
            catch (NullReferenceException ex)
			{
                Debug.WriteLine(ex.StackTrace);

                throw;
			}
            catch (ArgumentException ex)
			{
                Debug.WriteLine(ex.StackTrace);

                throw;
			}
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

        protected virtual string DeclareArgumentVariable(Parameter argument)
		{
            string code = string.Empty;
            if (0 == argument.PointerNum)
			{
                code = argument.DataType;
			}
            else if (1 == argument.PointerNum)
			{
                code = argument.DataType;
			}
			else if (2 == argument.PointerNum)
			{
                code = $"{argument.PointerNum}*";
			}
			else
			{
                throw new ArgumentException();
			}
            return code;
		}
    }
}
