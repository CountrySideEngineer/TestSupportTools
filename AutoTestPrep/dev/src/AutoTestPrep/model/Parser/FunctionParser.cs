using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSEngineer.TestSupport.Utility;

namespace AutoTestPrep.Model.Parser
{
	using Reader;

	public class FunctionParser : IParser
	{
		/// <summary>
		/// Target name to parse.
		/// </summary>
		public string Target { get; set; }

		/// <summary>
		/// Returns the function parameter data in 
		/// </summary>
		/// <param name="srcPath">Path to function data.</param>
		/// <returns>Function information data as Parameter object.</returns>
		public object Parse(string srcPath)
		{
			return this.Read(srcPath);
		}

		/// <summary>
		/// Read function information from a file specified by argument <para>srcPath</para>.
		/// </summary>
		/// <param name="srcPath">Path to file which contains the target function information.</param>
		/// <returns>Object of function data.</returns>
		protected object Read(string srcPath)
		{
			using (var stream = new FileStream(srcPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				Parameter parameter = this.ReadTargetFunction(stream);

				return parameter;
			}
		}

		public object Read(ExcelReader reader)
		{
			Parameter readFunction = GetFunctionInfo(reader);

			return readFunction;
		}

		/// <summary>
		/// Read target function information from stream.
		/// </summary>
		/// <param name="stream">Stream of file.</param>
		/// <returns>Parameter of target function.</returns>
		protected Parameter ReadTargetFunction(Stream stream)
		{
			var reader = new ExcelReader(stream)
			{
				SheetName = "test_data_001"
			};
			Parameter readFunction = GetFunctionInfo(reader);

			return readFunction;
		}

		/// <summary>
		/// Get function information from excel file using object <para>ExcelReader</para>.
		/// </summary>
		/// <param name="reader">Object to read function information from Excel.</param>
		/// <returns>Function information in Paramter object.</returns>
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

		/// <summary>
		/// Get function information in a area specified in range.
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="range"></param>
		/// <returns></returns>
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
			int pointerNum = 0;
			string dataTypeWithoutPointer = string.Empty;
			try
			{
				Range typeRange = reader.FindFirstItemInRow("データ型", range);
				List<string> items = reader.ReadRow(typeRange).ToList();
				var dataType = items[1];
				pointerNum = Util.GetPointerNum(dataType);
				dataTypeWithoutPointer = Util.RemovePointer(dataType);
			}
			catch (ArgumentOutOfRangeException)
			{
				pointerNum = 0;
				dataTypeWithoutPointer = string.Empty;
			}

			//Get arguments
			var arguments = this.GetArguments(reader, range);

			Parameter parameter = new Parameter
			{
				Description = description,
				Name = name,
				DataType = dataTypeWithoutPointer,
				Parameters = arguments,
				PointerNum = pointerNum
			};
			return parameter;
		}

		/// <summary>
		/// Get first information of argument in a range specified argument <para>range</para>.
		/// </summary>
		/// <param name="reader">Object to read data from Excel.</param>
		/// <param name="range">Range to read.</param>
		/// <returns>A list of argument in <para>Parameter</para> object.</returns>
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

				var dataTypeWithoutPointer = Util.RemovePointer(argInfos[2]);
				int poinuterNum = Util.GetPointerNum(argInfos[2]);
				var argInfo = new Parameter
				{
					Name = argInfos[1],
					DataType = dataTypeWithoutPointer,
					PointerNum = poinuterNum,
					Description = argInfos[3],
					Mode = Parameter.ToMode(argInfos[4])
				};
				arguments.Add(argInfo);
			}
			return arguments;
		}

		/// <summary>
		/// Get sub function information.
		/// </summary>
		/// <param name="reader">Object to read data from an excel file.</param>
		/// <returns></returns>
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

		/// <summary>
		/// Get sub function information in a range.
		/// </summary>
		/// <param name="reader">Object to read data from an excel file.</param>
		/// <param name="range">Range to read.</param>
		/// <returns>Read parameter</returns>
		protected Parameter GetSubfunction(ExcelReader reader, Range range)
		{
			Parameter subfunInfo = GetFunctionInfo(reader, range);

			return subfunInfo;
		}
	}
}
