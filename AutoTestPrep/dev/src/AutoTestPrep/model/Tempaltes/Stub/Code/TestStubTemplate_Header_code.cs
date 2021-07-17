using AutoTestPrep.Model.InputInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Tempaltes.Stub
{
	public partial class TestStubTemplate_Header
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		protected TestStubTemplate_Header() : base() { }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="function">Function information data.</param>
		public TestStubTemplate_Header(Function function, TestDataInfo testDataInfo)
			: base(function, testDataInfo)
		{ }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="function">Function information data (Parent function data).</param>
		/// <param name="subFunction">Sub function information.</param>
		/// <param name="testDataInfo">TestDataInformation</param>
		public TestStubTemplate_Header(Function function, Function subFunction, TestDataInfo testDataInfo)
			:base(function, subFunction, testDataInfo)
		{ }

		protected override string CreateFunctionArgumentBufferDeclare(Function function, Parameter argument)
		{
			string argumentBufferDeclare = base.CreateFunctionArgumentBufferDeclare(function, argument);
			if (!(string.IsNullOrEmpty(argumentBufferDeclare)))
			{
				argumentBufferDeclare = "extern " + argumentBufferDeclare;
				argumentBufferDeclare += "[];";
			}
			else
			{
				argumentBufferDeclare = $"//Argument {argument.Name} does not need buffer.";
			}
			return argumentBufferDeclare;
		}

		protected override string CreateFunctionOutputBufferDeclare(Function function, Parameter argument)
		{
			string outputBufferDeclare = string.Empty;
			try
			{
				outputBufferDeclare = base.CreateFunctionOutputBufferDeclare(function, argument);
				if (!(string.IsNullOrEmpty(outputBufferDeclare)))
				{
					outputBufferDeclare = "extern " + outputBufferDeclare;
					outputBufferDeclare += "[]";
					if (2 == argument.PointerNum)
					{
						outputBufferDeclare += $"[{this.TestDataInfo.StubBufferSize2}]";
					}
				}
				else
				{
					outputBufferDeclare = $"//{argument.Name} does not need output buffer.";
				}
			}
			catch (ArgumentException)
			{
				outputBufferDeclare = $"//{argument.Name} does not need output buffer.";
			}
			catch (Exception)
			{
				outputBufferDeclare = "Argument does not need output buffer.";
			}
			return outputBufferDeclare;
		}
	}
}
