using CodeGenerator.Data;
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
	public partial class MinUnitSourceTemplate
	{
		/// <summary>
		/// Test target function data.
		/// </summary>
		public Function TargetFunction { get; set; }

		/// <summary>
		/// Test data/
		/// </summary>
		public Test Test { get; set; }

		/// <summary>
		/// Code configuration.
		/// </summary>
		public CodeConfiguration Config { get; set; }

		/// <summary>
		/// Stub header file name the test driver source code includes.
		/// </summary>
		public string StubHeaderFileName { get; set; }

		/// <summary>
		/// Create code to setup test.
		/// </summary>
		/// <param name="function">Test target function.</param>
		/// <returns>Setup test code.</returns>
		public virtual string CreateSetUpCode(Function function)
		{
			var template = new MinUnitSourceSetUpTemplate(function);

			try
			{
				var setupCode = template.TransformText();
				return setupCode;
			}
			catch (NullReferenceException ex)
			{
				Debug.WriteLine(ex.StackTrace);

				throw;
			}
		}

		/// <summary>
		/// Create code of the test case.
		/// </summary>
		/// <param name="function">Test target function.</param>
		/// <param name="test">Test data.</param>
		/// <returns>Test case code.</returns>
		public virtual string CreateTestCaseCode(Function function, Test test)
		{
			string testCaseCode = string.Empty;
			foreach (var testCase in test.TestCases)
			{
				testCaseCode += this.CreateTestCaseCode(function, testCase);
			}
			return testCaseCode;
		}

		/// <summary>
		/// Create code of the test case code.
		/// </summary>
		/// <param name="function">Test target function.</param>
		/// <param name="testCase">Test case.</param>
		/// <returns></returns>
		public virtual string CreateTestCaseCode(Function function, TestCase testCase)
		{
			var template = new MinUnitSourceTestCaseTemplate(function, testCase);
			var testCode = template.TransformText();
			return testCode;
		}

		/// <summary>
		/// Create code to setup 
		/// </summary>
		/// <param name="function">Test target function.</param>
		/// <param name="test">Test data.</param>
		/// <returns>Code to run all test case method.</returns>
		public virtual string CreateCallTestCaseCode(Function function, Test test)
		{
			var template = new MinUnitSourceCallTestCaseTemplate(function, test);
			var testCode = template.TransformText();
			return testCode;
		}

		/// <summary>
		/// Generates codes to declare global variables.
		/// </summary>
		/// <param name="function">Target function data.</param>
		/// <returns>Codes to deaclare global variables.</returns>
		public virtual string CreateDecalreGlobalVariable(Function function)
		{
			string code = string.Empty;
			code += CreateDeclareExternalGlobalVariable(function.ExternalVariables);
			code += CreateDeclareInternalGlobalVariable(function.InternalVariables);

			if ((string.IsNullOrEmpty(code)) || (string.IsNullOrWhiteSpace(code)))
			{
				code = $"//No global variables are refered by function {function.Name}.";
			}
			return code;
		}

		/// <summary>
		/// Generates codes to declare internal global variables.
		/// </summary>
		/// <param name="variables">Collection of global variable information.</param>
		/// <returns>Codes to declare internal global variables.</returns>
		protected virtual string CreateDeclareInternalGlobalVariable(IEnumerable<Parameter> variables)
		{
			try
			{
				string codes = string.Empty;
				foreach (var variable in variables)
				{
					string code = CreateDeclareInternalGlobalVariable(variable);
					if ((!string.IsNullOrEmpty(code)) && (!string.IsNullOrWhiteSpace(code)))
					{
						codes += code;
						codes += Environment.NewLine;
					}
				}
				return codes;
			}
			catch (NullReferenceException)
			{
				string message = "//There is no internal global variable.";
				return message;
			}
		}

		/// <summary>
		/// Generates code to declare internal global variable.
		/// </summary>
		/// <param name="variable">Variable data.</param>
		/// <returns>Code to declare internal global variable.</returns>
		protected virtual string CreateDeclareInternalGlobalVariable(Parameter variable)
		{
			try
			{
				string code = $"extern {variable.ToString()};";
				return code;
			}
			catch (NullReferenceException)
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// Generates code to declare external global variables.
		/// </summary>
		/// <param name="variables">Collection of global variables.</param>
		/// <returns>Codes to declare global variables.</returns>
		protected virtual string CreateDeclareExternalGlobalVariable(IEnumerable<Parameter> variables)
		{
			try
			{
				string codes = string.Empty;
				foreach (var variable in variables)
				{
					string code = CreateDeclareExternalGlobalVariable(variable);
					if ((!string.IsNullOrEmpty(code)) && (!string.IsNullOrWhiteSpace(code)))
					{
						codes += code;
						codes += Environment.NewLine;
					}
				}
				return codes;
			}
			catch (NullReferenceException)
			{
				string message = "//The is no external global variable.";
				return message;
			}
		}

		/// <summary>
		/// Generates code to declare external global variable.
		/// </summary>
		/// <param name="variable">Variable data.</param>
		/// <returns>Code to declare external global variable.</returns>
		protected virtual string CreateDeclareExternalGlobalVariable(Parameter variable)
		{
			try
			{
				string code = $"{variable.ToString()};";
				return code;
			}
			catch (NullReferenceException)
			{
				return string.Empty;
			}
		}
	}
}
