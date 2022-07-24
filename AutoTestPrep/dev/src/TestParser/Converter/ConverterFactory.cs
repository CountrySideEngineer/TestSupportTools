using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Config;

namespace TestParser.Converter
{
	public class ConverterFactory
	{
		public FunctionConfig TestFunctionConfig { get; set; }
		public FunctionConfig SubFunctionConfig { get; set; }
		public GlobalVariableConfig GlobalVariableConfig { get; set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ConverterFactory()
		{
			TestFunctionConfig = null;
			SubFunctionConfig = null;
			GlobalVariableConfig = null;
		}

		public ConverterFactory(
			FunctionConfig testFunctionConfig, 
			FunctionConfig subFunctionConfig,
			GlobalVariableConfig globalVariableConfig
			)
		{
			TestFunctionConfig = testFunctionConfig;
			SubFunctionConfig = subFunctionConfig;
			GlobalVariableConfig = globalVariableConfig;
		}

		public ConverterFactory(TargetFunctionConfig targetFunctionConfig)
		{
			TestFunctionConfig = targetFunctionConfig.TestFunctionConfig;
			SubFunctionConfig = targetFunctionConfig.SubFunctionConfig;
			GlobalVariableConfig = targetFunctionConfig.GlobalVariableConfig;
		}

		public virtual AFunctionTableItemConverter Create(IEnumerable<string> items)
		{
			AFunctionTableItemConverter converter = null;
			string typeTag = items.ElementAt(0);
			string contentTag = items.ElementAt(1);
			if (TestFunctionConfig.Type.Equals(typeTag))
			{
				if (TestFunctionConfig.Body.Equals(contentTag))
				{
					converter = new TargetFunctionConverter();
				}
				else if (TestFunctionConfig.Argument.Equals(contentTag))
				{
					converter = new TargetFunctionArgumentConverter();
				}
				else
				{
					throw new ArgumentException();
				}
			}
			else if (SubFunctionConfig.Type.Equals(typeTag))
			{
				if (SubFunctionConfig.Body.Equals(contentTag))
				{
					converter = new SubFunctionConverter();
				}
				else if (SubFunctionConfig.Argument.Equals(contentTag))
				{
					converter = new SubFunctionArgumentConverter();
				}
				else
				{
					throw new ArgumentException();
				}
			}
			else if (GlobalVariableConfig.Type.Equals(typeTag))
			{
				if (GlobalVariableConfig.Internal.Equals(contentTag))
				{
					converter = new InternalVariableConverter();
				}
				else if (GlobalVariableConfig.External.Equals(contentTag))
				{
					converter = new ExternalVariableConverter();
				}
				else
				{
					throw new ArgumentException();
				}
			}
			else
			{
				throw new ArgumentException();
			}
			return converter;
		}
	}
}
