using CSEngineer.Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Target;

namespace CodeGenerator.Stub.Template
{
	public partial class StubTemplate
	{
		/// <summary>
		/// Create code to include standard header files defined by user.
		/// </summary>
		/// <param name="headerFiles">Collection of standard header files.</param>
		/// <returns>Code to include standard header files.</returns>
		public virtual string CreateStdHeaderInclude(IEnumerable<string> headerFiles)
		{
			string includeHeaderFiles = string.Empty;
			includeHeaderFiles = this.CreateHeaderInclude(headerFiles, "<", ">");
			return includeHeaderFiles;
		}

		/// <summary>
		/// Create code to include user header files defined by user.
		/// </summary>
		/// <param name="headerFiles">Collection of user header files.</param>
		/// <returns>Code to include user header files.</returns>
		public virtual string CreateUserHeaderInclude(IEnumerable<string> headerFiles)
		{
			string includeHeaderFiles = string.Empty;
			includeHeaderFiles = this.CreateHeaderInclude(headerFiles, "\"", "\"");
			return includeHeaderFiles;
		}

		/// <summary>
		/// Create code to include header files defined by usre.
		/// </summary>
		/// <param name="headerFiles">Collection of header files.</param>
		/// <param name="openTag">Opening parentheses of include code.</param>
		/// <param name="closeTag">Closing parenthese of include code.</param>
		/// <returns>Code to include header files.</returns>
		protected virtual string CreateHeaderInclude(IEnumerable<string> headerFiles, string openTag, string closeTag)
		{
			string includeHeaderFiles = string.Empty;
			foreach (var headerFileItem in headerFiles)
			{
				if ((string.IsNullOrEmpty(headerFileItem)) ||
					(string.IsNullOrWhiteSpace(headerFileItem)))
				{
					Log.GetInstance().WARN("SKIP : Empty header file name has been detected.");
					continue;
				}
				includeHeaderFiles += $"#include {openTag}{headerFileItem}{closeTag}";
			}
			return includeHeaderFiles;
		}


		/// <summary>
		/// Create buffer to store the count a method is called.
		/// </summary>
		/// <param name="function">Target function information.</param>
		/// <returns>Buffer name to store the count of the method called count</returns>
		/// <exception cref="ArgumentException">Name property of argument "function" is empty or white space.</exception>
		/// <exception cref="ArgumentNullException">Argument function has been null.</exception>
		public virtual string CreateFunctionCalledBufferName(Function function)
		{
			try
			{
				if ((string.IsNullOrEmpty(function.Name)) || (string.IsNullOrWhiteSpace(function.Name)))
				{
					Log.GetInstance().ERROR("The function name has not been set.");

					throw new ArgumentException();
				}
				string bufferName = $"{function.Name}_called_count";
				return bufferName;
			}
			catch (NullReferenceException)
			{
				Log.GetInstance().FATAL("NULL data has been referred in creating function called count buffer name");

				throw new ArgumentNullException();
			}
		}

		/// <summary>
		/// Create buffer name to store the argument value.
		/// </summary>
		/// <param name="function">Functoin data.</param>
		/// <param name="argument">Argument data.</param>
		/// <returns>Buffer name to store the argument value.</returns>
		/// <exception cref="ArgumentException">
		/// Name property of argument "function" or "argument" is empty or whitespace.
		/// </exception>
		/// <exception cref="ArgumentNullException">One of "function" or "argument" or both are NULL.</exception>
		public virtual string CreateArgumentBufferName(Function function, Parameter argument)
		{
			try
			{
				if (((string.IsNullOrEmpty(function.Name)) || (string.IsNullOrWhiteSpace(function.Name)))
					|| ((string.IsNullOrEmpty(argument.Name)) || (string.IsNullOrWhiteSpace(argument.Name))))
				{
					Log.GetInstance().ERROR("The function or argument name has not been set.");

					throw new ArgumentException();
				}

				string bufferName = $"{function.Name}_{argument.Name}";
				return bufferName;
			}
			catch (NullReferenceException)
			{
				Log.GetInstance().FATAL("NULL data has been referred in creating argument value buffer name.");

				throw new ArgumentNullException();
			}
		}

		/// <summary>
		/// Create buffer name to store the value a function will return.
		/// </summary>
		/// <param name="function">Target function.</param>
		/// <returns>Buffer name to store value to return.</returns>
		/// <exception cref="ArgumentException">"Name" property of function is empty or whitespace.</exception>
		/// <exception cref="ArgumentNullException">Argument function is null.</exception>
		public virtual string CreateFunctionReturnBufferName(Function function)
		{
			try
			{
				if ((string.IsNullOrEmpty(function.Name)) || (string.IsNullOrWhiteSpace(function.Name)))
				{
					Log.GetInstance().ERROR("The function name has not been set.");

					throw new ArgumentException();
				}
				string bufferName = string.Empty;
				if (!("void".Equals(function.DataType.ToLower())))
				{
					bufferName = $"{function.Name}_return_value";
				}
				return bufferName;
			}
			catch (ArgumentException)
			{
				Log.GetInstance().DEBUG("An error occurred while creating fuction return value buffer name.");

				throw;
			}
			catch (NullReferenceException)
			{
				Log.GetInstance().FATAL("NULL data has been referred in creating function return value buffer name.");

				throw new ArgumentNullException();
			}
		}

		/// <summary>
		/// Create buffer name to store value to return via argument
		/// </summary>
		/// <param name="argument">Argument</param>
		/// <returns>Buffer name to store value to return via argument.</returns>
		/// <exception cref="ArgumentException">
		/// Name property of argument "function" or "argument" is empty or whitespace.
		/// </exception>
		/// <exception cref="ArgumentNullException">One of "function" or "argument" or both are NULL.</exception>
		public virtual string CreateOutputBufferName(Function function, Parameter argument)
		{
			try
			{
				string bufferName = string.Empty;
				bufferName = $"{CreateArgumentBufferName(function, argument)}_value";
				return bufferName;
			}
			catch (Exception ex)
			when ((ex is ArgumentNullException) || (ex is ArgumentException))
			{
				Log.GetInstance().DEBUG("An error occurred while creating output value buffer name.");

				throw ex;
			}
			catch (Exception ex)
			{
				Log.GetInstance().FATAL("An exception has been referred in creating function return value buffer name.");

				throw ex;
			}
		}

		/// <summary>
		/// Create code declaring variable to store the count a method is called.
		/// </summary>
		/// <param name="function">Target function information</param>
		/// <returns>Buffer declaration.</returns>
		/// <exception cref="ArgumentException">
		/// Name property of argument "function" or "argument" is empty or whitespace.
		/// </exception>
		/// <exception cref="ArgumentNullException">One of "function" or "argument" or both are NULL.</exception>
		protected virtual string CreateFunctoinCalledCountBufferDeclare(Function function)
		{
			try
			{
				string bufferDeclare = string.Empty;
				bufferDeclare = $"int {this.CreateFunctionCalledBufferName(function)}";

				return bufferDeclare;
			}
			catch (Exception ex)
			when ((ex is ArgumentNullException) || (ex is ArgumentException))
			{
				Debug.WriteLine(ex.StackTrace);

				throw ex;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.StackTrace);

				throw ex;
			}
		}

		/// <summary>
		/// Create code declaring variable to store an argument value.
		/// </summary>
		/// <param name="function">Function the argument has.</param>
		/// <param name="argument">Argument data.</param>
		/// <returns>Code declare buffer to store argument value.</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException">
		/// -The data type of <para>argument</para> is void and not pointer.
		/// -The data type of <para>argument</para> is empty or white space.
		/// </exception>
		protected virtual string CreateArgumentBufferDeclare(Function function, Parameter argument)
		{
			try
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
			catch (Exception ex)
			when ((ex is ArgumentNullException) || (ex is ArgumentException))
			{
				Debug.WriteLine(ex.StackTrace);

				throw;
			}
			catch (NullReferenceException ex)
			{
				Debug.WriteLine(ex.StackTrace);

				throw new ArgumentNullException();
			}
		}

		/// <summary>
		/// Create code declaring variable to store a value a method(stub) should return.
		/// </summary>
		/// <param name="function">Function data.</param>
		/// <returns>Code declaring buffer to store a method returns.</returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		protected virtual string CreateFunctionReturnBufferDeclare(Function function)
		{
			try
			{
				string bufferDeclare = string.Empty;
				if (function.HasReturn())
				{
					bufferDeclare = $"{function.ActualDataType()} {this.CreateFunctionReturnBufferName(function)}";
				}
				return bufferDeclare;
			}
			catch (Exception ex)
			when ((ex is ArgumentNullException) || (ex is ArgumentException))
			{
				string message = "An error occurred while creating code to declare function return value buffer.";
				Log.GetInstance().ERROR(message);

				throw;
			}
			catch (NullReferenceException)
			{
				string message = "An null reference error occurred while creating code to declare function return value buffer.";
				Log.GetInstance().ERROR(message);

				throw new ArgumentNullException();
			}
		}

		/// <summary>
		/// Create buffer name to store value a function will return via pointer.
		/// </summary>
		/// <param name="function">Function data.</param>
		/// <param name="argument">Argument data.</param>
		/// <returns>Buffer name to store value a functoin return via pointer.</returns>
		/// <exception cref="ArgumentNullException"></exception>
		protected virtual string CreateOutputBufferDeclare(Function function, Parameter argument)
		{
			try
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
			catch (Exception ex)
			when ((ex is ArgumentNullException) || (ex is ArgumentException))
			{
				string message = "An error occurred while creating code to declare output value buffer.";
				Log.GetInstance().DEBUG(message);

				throw;
			}
			catch (NullReferenceException)
			{
				string message = "An error occurred while creating code to declare output value buffer.";
				Log.GetInstance().DEBUG(message);

				throw new ArgumentNullException();
			}
		}

		/// <summary>
		/// Create function name to initialize variable.
		/// </summary>
		/// <param name="function">Function to initialize</param>
		/// <returns>Initialize function name.</returns>
		/// <exception cref="ArgumentException">The name of function is invalid, or NULL.</exception>
		/// <exception cref="ArgumentNullException"></exception>
		public virtual string CreateInitializeFunctionName(Function function)
		{
			try
			{
				if ((string.IsNullOrEmpty(function.Name)) ||
					(string.IsNullOrWhiteSpace(function.Name)))
				{
					Log.GetInstance().ERROR("The function name has not been set.");

					throw new ArgumentException();
				}

				string functionName = string.Empty;
				functionName = $"{function.Name}_init";
				return functionName;
			}
			catch (Exception ex)
			when ((ex is ArgumentNullException) || (ex is ArgumentException))
			{
				string messag = "An error occurred while creating function name to initialize buffers.";
				Log.GetInstance().DEBUG(messag);

				throw;
			}
			catch (NullReferenceException)
			{
				string message = "An null value has been detected while creating function name to initialize buffers.";
				Log.GetInstance().DEBUG(message);

				throw new ArgumentNullException();
			}
		}

		/// <summary>
		/// Create code to declare initialize method.
		/// </summary>
		/// <param name="function">Function to initialize</param>
		/// <returns>Code to declare initialize method.</returns>
		/// <exception cref="ArgumentException">Argument function is invalid.</exception>
		/// <exception cref="ArgumentNullException">Argument "function" is NULL.</exception>
		protected virtual string CreateInitializeFunctionDeclare(Function function)
		{
			try
			{
				string entryPoint = string.Empty;
				entryPoint = $"void {this.CreateInitializeFunctionName(function)}";
				return entryPoint;
			}
			catch (Exception ex)
			when ((ex is ArgumentException) || (ex is ArgumentNullException))
			{
				string message = "An error occurred while creating code to declare function to initialize buffers.";
				Log.GetInstance().DEBUG(message);

				throw;
			}
		}
	}
}
