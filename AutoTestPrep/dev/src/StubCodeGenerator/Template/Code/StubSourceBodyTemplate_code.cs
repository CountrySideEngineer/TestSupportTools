﻿using CodeGenerator.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Target;
using CSEngineer;
using CSEngineer.Logger;

namespace CodeGenerator.Stub.Template
{
	public partial class StubSourceBodyTemplate
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
		public StubSourceBodyTemplate()
		{
			this.ParentFunction = new Function();
			this.TargetFunction = new Function();
			this.Config = new CodeConfiguration();
		}

		/// <summary>
		/// Create code declaring variable to store a value a method(stub) should return.
		/// </summary>
		/// <param name="function">Function data.</param>
		/// <returns>Code declaring buffer to store a method returns.</returns>
		protected override string CreateFunctionReturnBufferDeclare(Function function)
		{
			try
			{
				string bufferDeclare = string.Empty;
				if (function.HasReturn())
				{
					bufferDeclare = base.CreateFunctionReturnBufferDeclare(function);
					bufferDeclare = $"{bufferDeclare}[STUB_BUFFER_SIZE_1]";
				}
				return bufferDeclare;
			}
			catch (NullReferenceException)
			{
				string message = "An error occurred while creating code to declare function return buffer.";
				Log.GetInstance().ERROR(message);

				throw new ArgumentNullException();
			}
		}

		/// <summary>
		/// Create code declaring variable to store an argument value.
		/// </summary>
		/// <param name="function">Function the argument has.</param>
		/// <param name="argument">Argument data.</param>
		/// <returns>Code declare buffer to store argument value.</returns>
		protected override string CreateArgumentBufferDeclare(Function function, Parameter argument)
		{
			try
			{
				string bufferDeclare = base.CreateArgumentBufferDeclare(function, argument);
				bufferDeclare = $"{bufferDeclare}[STUB_BUFFER_SIZE_1];";
				return bufferDeclare;
			}
			catch (ArgumentException)
			{
				string message = "An error occurred while creating code to declare argument buffer.";
				Log.GetInstance().ERROR(message);

				throw;
			}
		}

		/// <summary>
		/// Create buffer name to store value a function will return via pointer.
		/// </summary>
		/// <param name="function">Function data.</param>
		/// <param name="argument">Argument data.</param>
		/// <returns>Buffer name to store value a functoin return via pointer.</returns>
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
					bufferDeclare = $"{bufferDeclare}[STUB_BUFFER_SIZE_1][STUB_BUFFER_SIZE_2];";
				}
				return bufferDeclare;
			}
			catch (Exception ex)
			when (ex is ArgumentNullException)
			{
				string message = "An error occurred while creating code to declare pointer output buffer.";
				Log.GetInstance().ERROR(message);

				throw;
			}
			catch (NullReferenceException)
			{
				string message = "Null value has been referred while creating code to declare pointer output buffer.";
				Log.GetInstance().ERROR(message);

				throw new ArgumentNullException();
			}
		}

		/// <summary>
		/// Create code to declare initialize method.
		/// </summary>
		/// <param name="function">Function to initialize</param>
		/// <returns>Code to declare initialize method.</returns>
		protected override string CreateInitializeFunctionDeclare(Function function)
		{
			string entryPoint = base.CreateInitializeFunctionDeclare(function);
			entryPoint = $"{entryPoint}()";
			return entryPoint;
		}

		/// <summary>
		/// Create code to initialize buffer to store value to return via pointer argument.
		/// </summary>
		/// <param name="function">Function information.</param>
		/// <param name="argument">Argument information</param>
		/// <returns>Code to initialize buffer storing value should be returned via pointer value.</returns>
		/// <remarks>If the argument is not return, this method will return comment.</remarks>
		protected virtual string CreateOutputArgumentInitialize(Function function, Parameter argument)
		{
			try
			{
				string outputArgInit = string.Empty;

				if (((1 == argument.PointerNum) ||
					(2 == argument.PointerNum)) &&                  //Condition1.Check whether the argument is pointer or not.
					((Parameter.AccessMode.Out == argument.Mode) ||
					(Parameter.AccessMode.Both == argument.Mode)))  //Condition2.Check whether the argument is output or not.
				{
					outputArgInit = $"{this.CreateOutputBufferName(function, argument)}[index][index2] = 0;";
				}
				else
				{
					outputArgInit = $"//{argument.Name} is not output.";
				}
				return outputArgInit;
			}
			catch (NullReferenceException)
			{
				string message = "Null value has been referred while creating code to initialize argument buffer.";
				Log.GetInstance().ERROR(message);

				throw new ArgumentNullException();
			}
		}

		/// <summary>
		/// Create code to latch a value the stub method shpuld return.
		/// </summary>
		/// <param name="function">Function information.</param>
		/// <returns>Code to latch the return buffer value.</returns>
		/// <remarks>If the function does not return any value, this method will return a comment.</remarks>
		protected virtual string CreateLatchReturnValueCode(Function function)
		{
			try
			{
				string returnLatchCode = string.Empty;
				if (function.HasReturn())
				{
					returnLatchCode = $"{function.ActualDataType()} " +
						$"latchReturn = " +
						$"{CreateFunctionReturnBufferName(function)}[{CreateFunctionCalledBufferName(function)}]";
				}
				else
				{
					returnLatchCode = $"//{function.Name} does not return value.";
				}

				return returnLatchCode;
			}
			catch (NullReferenceException)
			{
				string message = "Null value has been referred while creating code to latch return value.";
				Log.GetInstance().ERROR(message);

				throw new ArgumentNullException();
			}
		}

		/// <summary>
		/// Create code to return value.
		/// </summary>
		/// <param name="function">Function information</param>
		/// <returns>Code to return latched value.</returns>
		/// <remarks>If the function does not return any value, this method will return a comment.</remarks>
		protected virtual string CreateReturnLatchedValueCode(Function function)
		{
			try
			{
				string returnLatchCode = string.Empty;
				if (function.HasReturn())
				{
					returnLatchCode = $"return latchReturn";
				}
				else
				{
					returnLatchCode = $"//{function.Name} does not return value.";
				}

				return returnLatchCode;
			}
			catch (NullReferenceException)
			{
				string message = "Null value has been referred while creating code to return latched value.";
				Log.GetInstance().ERROR(message);

				throw new ArgumentNullException();
			}
		}

		/// <summary>
		/// Create code to set buffer value to pointer argument.
		/// </summary>
		/// <param name="function">Function information</param>
		/// <param name="argument">Argument information</param>
		/// <returns>Code to set value from buffer into pointer.</returns>
		protected virtual string CreateSetOutputBufferToArgument(Function function, Parameter argument)
		{
			string buffCode = string.Empty;
			if ((1 == argument.PointerNum) &&
				((Parameter.AccessMode.Out == argument.Mode) ||
				(Parameter.AccessMode.Both == argument.Mode)))
			{
				buffCode = $"{argument.Name}[0] = " +
					$"{CreateOutputBufferName(function, argument)}[{CreateFunctionCalledBufferName(function)}][0];";
			}
			else if ((2 == argument.PointerNum) &&
				((Parameter.AccessMode.Out == argument.Mode) ||
				(Parameter.AccessMode.Both == argument.Mode)))
			{
				buffCode = $"*{argument.Name} = " +
					$"{CreateOutputBufferName(function, argument)}[{CreateFunctionCalledBufferName(function)}];";
			}
			else
			{
				buffCode = $"//{argument.Name} is not output.";
			}
			return buffCode;
		}

		protected virtual string CreateCalledCountInitialize(Function function)
		{
			try
			{
				string calledCountBuffName = this.CreateFunctionCalledBufferName(function);
				string initializeCode = $"{calledCountBuffName} = 0;";
				return initializeCode;
			}
			catch (Exception ex)
			when ((ex is ArgumentException) || (ex is ArgumentNullException))
			{
				string message = "An error occurred while creating code to initialize function called count buffer.";
				Log.GetInstance().ERROR(message);

				throw;
			}
		}

		/// <summary>
		/// Create code to initialize buffer to store return value the stub method should return.
		/// </summary>
		/// <param name="function"></param>
		/// <returns></returns>
		protected virtual string CreateFunctionReturnBufferInitialize(Function function)
		{
			string bufferDeclare = string.Empty;
			if (function.HasReturn())
			{
				bufferDeclare = base.CreateFunctionReturnBufferName(function);
				bufferDeclare = $"{bufferDeclare}[index] = 0;";
			}
			else
			{
				bufferDeclare = $"{function.Name} does not return value.";
			}
			return bufferDeclare;
		}

		protected virtual string CreateArgumentBufferInitialize(Function function, Parameter argument)
		{
			string bufferInitialize = string.Empty;
			bufferInitialize = $"{CreateArgumentBufferName(function, argument)}[index] = 0;";
			return bufferInitialize;
		}
	}
}
