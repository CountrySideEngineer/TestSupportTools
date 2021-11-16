using CodeGenerator.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Target;

namespace CodeGenerator.Stub.Template
{
	public partial class StubHeaderBodyTemplate
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
		/// Default constructor.
		/// </summary>
		public StubHeaderBodyTemplate()
		{
			this.ParentFunction = new Function();
			this.TargetFunction = new Function();
			this.Config = new CodeConfiguration();
		}

		/// <summary>
		/// Create code to declare external variable to store the count a method is called.
		/// </summary>
		/// <param name="function">Target function information.</param>
		/// <returns>External buffer declare</returns>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		protected override string CreateFunctoinCalledCountBufferDeclare(Function function)
		{
			try
			{
				string bufferDeclare = $"extern {base.CreateFunctoinCalledCountBufferDeclare(function)};";
				return bufferDeclare;
			}
			catch (Exception ex)
			when ((ex is ArgumentException) || (ex is ArgumentNullException))
			{
				Debug.WriteLine(ex.StackTrace);

				throw;
			}
		}

		/// <summary>
		/// Create code declaring variable to store an argument value.
		/// </summary>
		/// <param name="function">Function the argument has.</param>
		/// <param name="argument">Argument data.</param>
		/// <returns>Code declare buffer to store argument value.</returns>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		protected override string CreateArgumentBufferDeclare(Function function, Parameter argument)
		{
			try
			{
				string bufferDeclare = base.CreateArgumentBufferDeclare(function, argument);
				bufferDeclare = $"extern {bufferDeclare}[]";
				return bufferDeclare;
			}
			catch (Exception ex)
			when ((ex is ArgumentException) || (ex is ArgumentNullException))
			{
				Debug.WriteLine(ex.StackTrace);

				throw;
			}
		}

		/// <summary>
		/// Create buffer code for external declaration of buffer to store values to 
		/// </summary>
		/// <param name="function">Function data.</param>
		/// <param name="argument">Argument data.</param>
		/// <returns>Code to declare buffer externaly to store value the method should return via pointer arguments.</returns>
		protected override string CreateOutputBufferDeclare(Function function, Parameter argument)
		{
			try
			{
				string bufferDeclare = string.Empty;
				bufferDeclare = base.CreateOutputBufferDeclare(function, argument);
				if ((string.IsNullOrEmpty(bufferDeclare)) || (string.IsNullOrWhiteSpace(bufferDeclare)))
				{
					bufferDeclare = $"//{argument.Name} is not output.";
				}
				else
				{
					bufferDeclare = $"extern {bufferDeclare}[][STUB_BUFFER_SIZE_2];";
				}
				return bufferDeclare;
			}
			catch (ArgumentNullException ex)
			{
				Debug.WriteLine(ex.StackTrace);

				throw ex;
			}
		}

		/// <summary>
		/// Create code to declare external variable to store a value a method(stub) should return.
		/// </summary>
		/// <param name="function">Function data.</param>
		/// <returns>External code declaring buffer to store a method should return.</returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		protected override string CreateFunctionReturnBufferDeclare(Function function)
		{
			try
			{
				string bufferDeclare = string.Empty;
				if (function.HasReturn())
				{
					bufferDeclare = $"extern {base.CreateFunctionReturnBufferDeclare(function)}[];";
				}
				else
				{
					bufferDeclare = $"{function.Name} does not return value.";
				}

				return bufferDeclare;
			}
			catch (Exception ex)
			when ((ex is ArgumentException) || (ex is ArgumentNullException))
			{
				Debug.WriteLine(ex.StackTrace);

				throw;
			}
		}

		/// <summary>
		/// Create code to declare initialize method.
		/// </summary>
		/// <param name="function">Function to initialize</param>
		/// <returns>Code to declare initialize method.</returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		protected override string CreateInitializeFunctionDeclare(Function function)
		{
			try
			{
				string entryPoint = base.CreateInitializeFunctionDeclare(function);
				entryPoint = $"{entryPoint}();";
				return entryPoint;
			}
			catch (Exception ex)
			when ((ex is ArgumentException) || (ex is ArgumentNullException))
			{
				Debug.WriteLine(ex.StackTrace);

				throw;
			}
		}
	}
}
