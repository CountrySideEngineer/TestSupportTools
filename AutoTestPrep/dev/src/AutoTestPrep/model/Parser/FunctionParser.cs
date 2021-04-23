using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Parser
{
	using Reader;

	public class FunctionParser : IParser
	{
		/// <summary>
		/// Returns the function parameter data in 
		/// </summary>
		/// <param name="srcPath">Path to function data.</param>
		/// <returns>Function information data as Parameter object.</returns>
		public object Parse(string srcPath)
		{
			return this.Read(srcPath);
		}

		protected object Read(string srcPath)
		{
			using (var stream = new FileStream(srcPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				Parameter parameter = this.ReadTargetFunction(stream);

				return parameter;
			}
		}

		protected Parameter ReadTargetFunction(Stream stream)
		{
			var reader = new ExcelReader(stream);
			reader.SheetName = "test_data_001";
			Parameter readFunction = GetFunctionInfo(reader);

			return readFunction;
		}

		protected Parameter GetFunctionInfo(ExcelReader reader)
		{
			//「対象関数」のセルを取得する
			Range targetFuncRange = reader.FindFirstItem("対象関数");

			//取得したRangeを引数として、GetFunCtionInfo()を実行する
			Parameter functionInfo = this.GetFunctionInfo(reader, targetFuncRange);

			//取得したRangeを引数として、子関数情報を取得する。
			var subfunctions = this.GetSubfunctions(reader);
			functionInfo.Children = subfunctions;

			return functionInfo;
		}

		protected Parameter GetFunctionInfo(ExcelReader reader, Range range)
		{
			//Get description.
			string description = string.Empty;
			try
			{
				Range descRange = reader.FindFirstItemInRow("説明", range);
				List<string> items = reader.ReadRow(descRange).ToList();
				description = items[1];
			}
			catch (ArgumentOutOfRangeException)
			{
				description = string.Empty;
			}

			//Get name.
			string name = string.Empty;
			try
			{
				Range nameRange = reader.FindFirstItemInRow("関数名", range);
				List<string> items = reader.ReadRow(nameRange).ToList();
				name = items[1];
			}
			catch (ArgumentOutOfRangeException)
			{
				name = string.Empty;
			}

			//Get data type.
			string dataType = string.Empty;
			try
			{
				Range typeRange = reader.FindFirstItemInRow("データ型", range);
				List<string> items = reader.ReadRow(typeRange).ToList();
				dataType = items[1];
			}
			catch (ArgumentOutOfRangeException)
			{
				dataType = string.Empty;
			}

			//Get arguments
			var arguments = this.GetArguments(reader, range);

			Parameter parameter = new Parameter
			{
				Description = description,
				Name = name,
				DataType = dataType,
				Parameters = arguments
			};
			return parameter;
		}

		protected IEnumerable<Parameter> GetArguments(ExcelReader reader, Range range)
		{
			//Range argument Range
			Range argRange = reader.FindFirstItemInRow("引数情報", range);
			reader.GetTableRange(ref argRange);
			argRange.StartRow++;
			argRange.RowCount--;

			var arguments = new List<Parameter>();
			for (int index = 0; index < argRange.RowCount; index++)
			{
				var rowRange = new Range
				{
					StartRow = argRange.StartRow + index,
					StartColumn = argRange.StartColumn
				};
				List<string> argInfos = reader.ReadRow(rowRange).ToList();

				/*
				 * Check argument whether parameters without description have been set.
				 * If not, it means the parameters are invalid and this function should notify error
				 * by throwing eception.
				 */
				if (((string.IsNullOrEmpty(argInfos[1])) || (string.IsNullOrWhiteSpace(argInfos[1]))) ||
					((string.IsNullOrEmpty(argInfos[2])) || (string.IsNullOrWhiteSpace(argInfos[2]))))
				{
					throw new InvalidDataException();
				}

				var argInfo = new Parameter
				{
					Name = argInfos[1],
					DataType = argInfos[2],
					Description = argInfos[3],
					Mode = Parameter.ToMode(argInfos[4])
				};
				arguments.Add(argInfo);
			}
			return arguments;
		}

		protected IEnumerable<Parameter> GetSubfunctions(ExcelReader reader)
		{
			IEnumerable<Range> subfuncRanges = reader.FindItem("子関数");
			var parameters = new List<Parameter>();
			foreach (var rangeItem in subfuncRanges)
			{
				var subfuncParam = this.GetSubfunction(reader, rangeItem);
				parameters.Add(subfuncParam);
			}
			return parameters;
		}

		protected Parameter GetSubfunction(ExcelReader reader, Range range)
		{
			Parameter subfunInfo = GetFunctionInfo(reader, range);

			return subfunInfo;
		}
	}
}
