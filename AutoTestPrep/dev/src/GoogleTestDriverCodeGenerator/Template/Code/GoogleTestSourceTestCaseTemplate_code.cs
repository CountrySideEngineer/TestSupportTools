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
	public partial class GoogleTestSourceTestCaseTemplate
	{
		/// <summary>
		/// Target test function data.
		/// </summary>
		public Function TargetFunction { get; set; }

		/// <summary>
		/// Test case number.
		/// </summary>
		public int TestCaseNumber { get; set; }

		/// <summary>
		/// Test case data for a test.
		/// </summary>
		public TestCase TestCase { get; set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		protected GoogleTestSourceTestCaseTemplate()
		{
			this.TestCaseNumber = 0;
			this.TargetFunction = new Function();
		}

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="testCaseNumber">Test case number.</param>
		/// <param name="targetFunction">Target test function data.</param>
		public GoogleTestSourceTestCaseTemplate(int testCaseNumber, Function targetFunction)
		{
			this.TestCaseNumber = TestCaseNumber;
			this.TargetFunction = targetFunction;
		}

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="targetFunction">Test target function data.</param>
		/// <param name="testCase">Test case data.</param>
		public GoogleTestSourceTestCaseTemplate(Function targetFunction, TestCase testCase)
		{
			this.TargetFunction = targetFunction;
			this.TestCase = testCase;
		}

		/// <summary>
		/// Create code to setup test input data.
		/// </summary>
		/// <param name="testData"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		protected virtual string CreateInput(TestData testData)
		{
			try
			{
				string inputCode = string.Empty;

				if (((string.IsNullOrEmpty(testData.Name)) || (string.IsNullOrWhiteSpace(testData.Name)))
					|| ((string.IsNullOrEmpty(testData.Value)) || (string.IsNullOrWhiteSpace(testData.Value))))
				{
					throw new ArgumentException();
				}

				inputCode = $"{testData.Name} = {testData.Value}";
				return inputCode;
			}
			catch (NullReferenceException ex)
			{
				Debug.WriteLine(ex.StackTrace);

				throw new ArgumentNullException();
			}
			catch (ArgumentException ex)
			{
				Debug.WriteLine(ex.StackTrace);

				throw;
			}
		}

		/// <summary>
		/// Create code to call target function.
		/// </summary>
		/// <param name="targetFunction">Target functoin data.</param>
		/// <returns>Code to call target function.</returns>
		protected virtual string CreateTargetFunctionCall(Function targetFunction)
		{
			try
			{
				if ((string.IsNullOrEmpty(targetFunction.Name)) || (string.IsNullOrWhiteSpace(targetFunction.Name)))
				{
					throw new ArgumentException();
				}

				string targetFunctionCall = string.Empty;

				if (!("void".Equals(targetFunction.DataType.ToLower())))
				{
					targetFunctionCall = $"{targetFunction.ActualDataType()} returnValue = ";
				}
				targetFunctionCall += $"{targetFunction.Name}(";
				bool isTop = true;
				foreach (var argument in targetFunction.Arguments)
				{
					if (!isTop)
					{
						targetFunctionCall += ", ";
					}
					string argumentCode = string.Empty;
					if (0 == argument.PointerNum)
					{
						argumentCode = $"{argument.Name}";
					}
					else if ((1 == argument.PointerNum) || (2 == argument.PointerNum))
					{
						argumentCode = $"&{argument.Name}";
					}
					else
					{
						throw new ArgumentOutOfRangeException();
					}
					targetFunctionCall += argumentCode;
					isTop = false;
				}
				targetFunctionCall += ")";

				return targetFunctionCall;
			}
			catch (Exception ex)
			when ((ex is ArgumentException) || (ex is NullReferenceException))
			{
				Debug.WriteLine(ex.StackTrace);

				throw;
			}
		}
	}
}
