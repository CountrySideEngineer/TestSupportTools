using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Target;
using CodeWriter.Data;

namespace CodeWriter.Template.Stub
{
	public partial class StubHeaderTemplate
	{
		/// <summary>
		/// Function data the (stub) function will call.
		/// </summary>
		public Function ParentFunction { get; set; }

		/// <summary>
		/// Base function of the stub.
		/// </summary>
		public Function TargetFunction { get; set; }

		/// <summary>
		/// Configuration about stub.
		/// </summary>
		public CodeConfiguration Config { get; set; }

		/// <summary>
		/// Create code to declare external variable to store the count a method is called.
		/// </summary>
		/// <param name="function">Target function information.</param>
		/// <returns>External buffer declare</returns>
		protected override string CreateFunctoinCalledCountBufferDeclare(Function function)
		{
			string bufferDeclare = $"extern {base.CreateFunctoinCalledCountBufferDeclare(function)};";
			return bufferDeclare;
		}

		/// <summary>
		/// Create code declaring variable to store an argument value.
		/// </summary>
		/// <param name="function">Function the argument has.</param>
		/// <param name="argument">Argument data.</param>
		/// <returns>Code declare buffer to store argument value.</returns>
		protected override string CreateArgumentBufferDeclare(Function function, Parameter argument)
		{
			string bufferDeclare = base.CreateArgumentBufferDeclare(function, argument);
			bufferDeclare = $"extern {bufferDeclare}[];";
			return bufferDeclare;
		}

		/// <summary>
		/// Create code to declare external variable to store a value a method(stub) should return.
		/// </summary>
		/// <param name="function">Function data.</param>
		/// <returns>External code declaring buffer to store a method should return.</returns>
		protected override string CreateFunctionReturnBufferDeclare(Function function)
		{
			string bufferDecalre = string.Empty;
			if (function.HasReturn())
			{
				bufferDecalre = $"extern {base.CreateFunctionReturnBufferDeclare(function)}[];";
			}
			return bufferDecalre;
		}

		/// <summary>
		/// Create code to declare initialize method.
		/// </summary>
		/// <param name="function">Function to initialize</param>
		/// <returns>Code to declare initialize method.</returns>
		protected override string CreateInitializeFunctionDeclare(Function function)
		{
			string entryPoint = base.CreateInitializeFunctionDeclare(function);
			entryPoint = $"{entryPoint}();";
			return entryPoint;
		}
	}
}
