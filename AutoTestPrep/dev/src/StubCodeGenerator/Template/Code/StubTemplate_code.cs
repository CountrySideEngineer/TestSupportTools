using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Target;

namespace CodeGenerator.Stub.Template
{
	public partial class StubTemplate
	{
		/// <summary>
		/// Create buffer to store the count a method is called.
		/// </summary>
		/// <param name="function">Target function information.</param>
		/// <returns>Buffer name to store the count of the method called count</returns>
		public virtual string CreateFunctionCalledBufferName(Function function)
		{
			string bufferName = $"{function.Name}_called_count";
			return bufferName;
		}

		/// <summary>
		/// Create buffer name to store the argument value.
		/// </summary>
		/// <param name="function">Functoin data.</param>
		/// <param name="argument">Argument data.</param>
		/// <returns>Buffer name to store the argument value.</returns>
		public virtual string CreateArgumentBufferName(Function function, Parameter argument)
		{
			string bufferName = $"{function.Name}_{argument.Name}";
			return bufferName;
		}

		/// <summary>
		/// Create buffer name to store the value a function will return.
		/// </summary>
		/// <param name="function">Target function.</param>
		/// <returns>Buffer name to store value to return.</returns>
		public virtual string CreateFunctionReturnBufferName(Function function)
		{
			string bufferName = string.Empty;
			if (!("void".Equals(function.DataType.ToLower())))
			{
				bufferName = $"{function.Name}_return_value";
			}
			return bufferName;
		}

		/// <summary>
		/// Create buffer name to store value to return via argument
		/// </summary>
		/// <param name="argument">Argument</param>
		/// <returns>Buffer name to store value to return via argument.</returns>
		public virtual string CreateOutputBufferName(Function function, Parameter argument)
		{
			string bufferName = string.Empty;
			bufferName = $"{CreateArgumentBufferName(function, argument)}_value";
			return bufferName;
		}

		/// <summary>
		/// Create code declaring variable to store the count a method is called.
		/// </summary>
		/// <param name="function">Target function information</param>
		/// <returns>Buffer declaration.</returns>
		protected virtual string CreateFunctoinCalledCountBufferDeclare(Function function)
		{
			string bufferDeclare = string.Empty;
			bufferDeclare = $"int {this.CreateFunctionCalledBufferName(function)}";

			return bufferDeclare;
		}

		/// <summary>
		/// Create code declaring variable to store an argument value.
		/// </summary>
		/// <param name="function">Function the argument has.</param>
		/// <param name="argument">Argument data.</param>
		/// <returns>Code declare buffer to store argument value.</returns>
		/// <exception cref="ArgumentException">
		/// -The data type of <para>argument</para> is void and not pointer.
		/// -The data type of <para>argument</para> is empty or white space.
		/// </exception>
		protected virtual string CreateArgumentBufferDeclare(Function function, Parameter argument)
		{
			if ((("void".Equals(argument.DataType.ToLower())) && (argument.PointerNum <= 0)) ||
				((string.IsNullOrWhiteSpace(argument.DataType)) || (string.IsNullOrEmpty(argument.DataType))))
			{
				throw new ArgumentException();
			}
			else
			{
				string bufferDeclare = $"{argument.ActualDataType()} {CreateArgumentBufferName(function, argument)}";
				return bufferDeclare;
			}
		}

		/// <summary>
		/// Create code declaring variable to store a value a method(stub) should return.
		/// </summary>
		/// <param name="function">Function data.</param>
		/// <returns>Code declaring buffer to store a method returns.</returns>
		protected virtual string CreateFunctionReturnBufferDeclare(Function function)
		{
			string bufferDeclare = string.Empty;
			if (function.HasReturn())
			{
				bufferDeclare = $"{function.ActualDataType()} {this.CreateFunctionReturnBufferName(function)}";
			}
			return bufferDeclare;
		}

		/// <summary>
		/// Create buffer name to store value a function will return via pointer.
		/// </summary>
		/// <param name="function">Function data.</param>
		/// <param name="argument">Argument data.</param>
		/// <returns>Buffer name to store value a functoin return via pointer.</returns>
		protected virtual string CreateOutputBufferDeclare(Function function, Parameter argument)
		{
			string bufferDeclare = string.Empty;

			if ((1 == argument.PointerNum) ||
				(2 == argument.PointerNum))
			{
				if ((Parameter.AccessMode.Out == argument.Mode) ||
					(Parameter.AccessMode.Both == argument.Mode))
				{
					bufferDeclare = $"{argument.DataType} {this.CreateOutputBufferName(function, argument)}";
				}
			}
			return bufferDeclare;
		}

		/// <summary>
		/// Create function name to initialize variable.
		/// </summary>
		/// <param name="function">Function to initialize</param>
		/// <returns>Initialize function name.</returns>
		public virtual string CreateInitializeFunctionName(Function function)
		{
			string functionName = string.Empty;
			functionName = $"{function.Name}_init";
			return functionName;
		}

		/// <summary>
		/// Create code to declare initialize method.
		/// </summary>
		/// <param name="function">Function to initialize</param>
		/// <returns>Code to declare initialize method.</returns>
		protected virtual string CreateInitializeFunctionDeclare(Function function)
		{
			string entryPoint = string.Empty;
			entryPoint = $"void {this.CreateInitializeFunctionName(function)}";
			return entryPoint;
		}
	}
}
