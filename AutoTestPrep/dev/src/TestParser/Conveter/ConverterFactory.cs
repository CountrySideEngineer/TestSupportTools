using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParser.Config;

namespace TestParser.Conveter
{
	public class ConverterFactory
	{
		public FunctionTableTagConfig Config { get; set; }

		public ConverterFactory()
		{
			Config = new FunctionTableTagConfig()
			{
				TargetFunctionTag = "テスト対象関数",
				SubFunctionTag = "子関数",
				GlobalVariableTag = "グローバル変数",
				BodyContentTag = "本体",
				ArgumentTag = "引数",
				InternalTag = "内部",
				ExternalTag = "外部",
			};
		}

		public virtual AFunctionTableItemConverter Create(IEnumerable<string> items)
		{
			AFunctionTableItemConverter converter = null;
			string typeTag = items.ElementAt(0);
			string contentTag = items.ElementAt(1);
			if ((Config.TargetFunctionTag.Equals(typeTag)) && (Config.BodyContentTag.Equals(contentTag)))
			{
				converter = new TargetFunctionConverter();
			}
			else if ((Config.TargetFunctionTag.Equals(typeTag)) && (Config.ArgumentTag.Equals(contentTag)))
			{
				converter = new TargetFunctionArgumentConverter();
			}
			else if ((Config.SubFunctionTag.Equals(typeTag)) && (Config.BodyContentTag.Equals(contentTag)))
			{
				converter = new SubFunctionConverter();
			}
			else if ((Config.SubFunctionTag.Equals(typeTag)) && (Config.ArgumentTag.Equals(contentTag)))
			{
				converter = new SubFunctionArgumentConverter();
			}
			else if ((Config.GlobalVariableTag.Equals(typeTag)) && (Config.InternalTag.Equals(contentTag)))
			{
				converter = new InternalVariableConverter();
			}
			else if ((Config.GlobalVariableTag.Equals(typeTag)) && (Config.ExternalTag.Equals(contentTag)))
			{
				converter = new ExternalVariableConverter();
			}
			else
			{
				throw new ArgumentException();
			}
			return converter;
		}
	}
}
