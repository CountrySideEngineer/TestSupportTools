using AutoTestPrep.Model.InputInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Tempaltes.Driver.min_unit
{
	public partial class TestDriverTemplate_min_unit_Source : TestDriverTemplate_min_unit_Base
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		protected TestDriverTemplate_min_unit_Source() : base() { }

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="test">Test data</param>
		/// <param name="testDataInfo">Test data information.</param>
		public TestDriverTemplate_min_unit_Source(Test test, TestDataInfo testDataInfo)
			: base(test, testDataInfo)
		{ }

		/// <summary>
		/// Create test name.
		/// </summary>
		/// <returns>Common test name.</returns>
		protected virtual string CreateTestMethodName()
		{
			string testMethodName = string.Empty;
			testMethodName = $"{this.TargetFunction.Name}_test";
			return testMethodName;
		}

		/// <summary>
		///	Create entry point of method to setup test.
		///	</summary>
		///	<return>Entry point of method to setup test.</return>
		protected virtual string CreateSetupMethodEntryPointCode()
		{
			string setupMethodEntryPointCode = string.Empty;
			setupMethodEntryPointCode = $"static void {CreateTestMethodName()}_SetUp()";
			return setupMethodEntryPointCode;
		}

		/// <summary>
		///	Create code to call stub initialize method.
		///	</summary>
		/// <return>Code to call stub initialize method.</return>
		/// <remarks>Move to common base class.</remarks>
		protected virtual string CreateInitializeStubMethodCall(Function subFunction)
		{
			string stubInitMethodCall = string.Empty;
			stubInitMethodCall = $"{subFunction.Name}_init();";
			return stubInitMethodCall;
		}

		/// <summary>
		/// Create test case name.
		/// </summary>
		///	<return>test case name.</return>
		protected virtual string CreateTestCaseMethodName(TestCase testCase)
		{
			string testCaseMethodName = string.Empty;
			testCaseMethodName = $"{this.TargetFunction.Name}_{testCase.Id}";
			return testCaseMethodName;
		}

		///	<summary>
		///	Creat code to declare argument.
		///	</summary>
		///	<return>Code to declare argument.</return>
		/// <remarks>Move to common base class.</remarks>
		protected virtual string CreateCodeToDeclareArgument(Parameter argument)
		{
			string declareArgumentCode = string.Empty;
			declareArgumentCode = argument.ToString();
			return declareArgumentCode;
		}

		///	<summary>
		///	Create code to set value to argument variable.
		///	</summary>
		///	<return>Code to set value to argument variable.</return>
		/// <remarks>Move to common base class.</remarks>
		protected virtual string CreateCodeToSetUpTestParameter(TestData testData)
		{
			string codeToSetupTestParameter = string.Empty;
			codeToSetupTestParameter = $"{testData.Name} = {testData.Value}";
			return codeToSetupTestParameter;
		}

		///	<summary>
		///	Create code to call test target function.
		///	</summary>
		///	<return>Code to call test target function.</return>
		/// <remarks>Move to common base class.</remarks>
		protected virtual string CreateCodeToCallTargetFunction()
		{
			string codeToCallTargetFunction = string.Empty;

			if (this.TargetFunction.HasReturn())
			{
				codeToCallTargetFunction = this.TargetFunction.DataType;
				codeToCallTargetFunction += " ";
				codeToCallTargetFunction += "returnValue = ";
			}
			codeToCallTargetFunction += this.TargetFunction.Name;
			codeToCallTargetFunction += "(";
			if (null != this.TargetFunction.Arguments)
			{
				bool isTop = true;
				foreach (var argumentItem in this.TargetFunction.Arguments)
				{
					if (!isTop)
					{
						codeToCallTargetFunction += ",";
						codeToCallTargetFunction += " ";
					}
					codeToCallTargetFunction += argumentItem.Name;
					isTop = false;
				}
			}
			codeToCallTargetFunction += ")";

			return codeToCallTargetFunction;
		}

		///	<summary>
		///	Create code to check output value whether it equals to expect.
		///	</summary>
		///	<return>Code to check output value whether it equals to expect.</return>
		/// <remarks>Declare as an abstract method in base class.</remarks>
		protected virtual string CreateCodeToCheckOutputAndExpect(TestData expect)
		{
			string codeToCheckExpect = string.Empty;

			codeToCheckExpect = "mu_assert(";
			if (("return").Equals(expect.Name))
			{
				codeToCheckExpect = $"{codeToCheckExpect}returnValue";
			}
			else
			{
				codeToCheckExpect = $"{codeToCheckExpect}{expect.Name}";
			}
			codeToCheckExpect = $"{codeToCheckExpect}, {expect.Value})";
			return codeToCheckExpect;
		}

		/// <summary>
		/// Create entry point name to run test.
		/// </summary>
		/// <returns>Entry point name to run test.</returns>
		protected virtual string CreateRunTestMethodEntryPoint()
		{
			string runTestMethodEntryPoint = string.Empty;
			runTestMethodEntryPoint = $"run_{this.TargetFunction.Name}_test";

			return runTestMethodEntryPoint;
		}

		/// <summary>
		/// Create code to call method to run test using min_unit test framework method.
		/// </summary>
		/// <param name="testCase">Test case data to test.</param>
		/// <returns>Code to call method to run test.</returns>
		protected virtual string CreateCodeToCallTestRunMethod(TestCase testCase)
		{
			string codeToCallTestRunMethod = string.Empty;
			codeToCallTestRunMethod = $"mu_run_test(" +
				$"\"{this.CreateTestCaseMethodName(testCase)}\", " +
				$"{this.CreateTestCaseMethodName(testCase)})";
			return codeToCallTestRunMethod;
		}

		/// <summary>
		/// Create code of stub header to include.
		/// </summary>
		/// <param name="subFunction">Stub function information.</param>
		/// <returns>Stub function header name to be included.</returns>
		protected virtual string CreateStubHeaderIncludeCode(Function subFunction)
		{
			string stubHeaderInclude = string.Empty;
			stubHeaderInclude = $"{subFunction.Name}_test_stub.h";

			return stubHeaderInclude;
		}

		protected virtual string CreateBufferInitializeFunctionName(Function subFunction)
		{
			string bufferInitializeFuncName = string.Empty;
			bufferInitializeFuncName = subFunction.Name;
			bufferInitializeFuncName += "_init()";

			return bufferInitializeFuncName;
		}
	}
}
