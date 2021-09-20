using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSEngineer;
using TestParser.Reader;
using TestParser.Parser.Exception;
using TestParser.Target;

namespace TestParser.Parser
{
	/// <summary>
	/// Parse function info in a test design file.
	/// </summary>
	public class FunctionListParser : AParser
	{
		/// <summary>
		/// Parse function information in file <para>srcPath</para>.
		/// </summary>
		/// <param name="srcPath">Path to input file.</param>
		/// <returns></returns>
		/// <exception cref="ParseDataNotFoundException"></exception>
		public override object Parse(string srcPath)
		{
			using (var stream = new FileStream(srcPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				try
				{
					IEnumerable<ParameterInfo> functionInfoList = this.ReadFunctionList(stream);
					return functionInfoList;
				}
				catch (ParseDataNotFoundException)
				{
					throw;
				}
			}
		}

		/// <summary>
		/// Parse function information from stream <para>stream</para>.
		/// </summary>
		/// <param name="stream">FileStream of function information.</param>
		/// <returns>Function information list.</returns>
		/// <exception cref="ParseDataNotFoundException"></exception>
		public override object Parse(Stream stream)
		{
			try
			{
				return this.ReadFunctionList(stream);
			}
			catch (ParseDataNotFoundException)
			{
				throw;
			}
		}

		/// <summary>
		/// Read function information from stream, FileStream object.
		/// </summary>
		/// <param name="stream">Stream object from file.</param>
		/// <returns>List of ParameterInfo object.</returns>
		/// <exception cref="ParseDataNotFoundException">The sheet to get target functions has not been set.</exception>
		protected IEnumerable<ParameterInfo> ReadFunctionList(Stream stream)
		{
			string targetSheetName = string.Empty;
			if (string.IsNullOrEmpty(this.Target) || (string.IsNullOrWhiteSpace(this.Target)))
			{
				Logger.ERROR($"The sheet name target functin and sheet name has not been set.");
				throw new ParseDataNotFoundException("Target sheet has not been set.");
			}
			var reader = new ExcelReader(stream)
			{
				SheetName = this.Target
			};
			IEnumerable<ParameterInfo> parameterInfolist = this.ReadFunctionInfo(reader);
			return parameterInfolist;
		}

		/// <summary>
		/// Read function informations from 
		/// </summary>
		/// <param name="reader">Object to read test data information from excel file.</param>
		/// <returns>List of function information.</returns>
		protected IEnumerable<ParameterInfo> ReadFunctionInfo(ExcelReader reader)
		{
			Logger.INFO("Start reading target function info, function and sheet name.");

			Range tableItemRange = new Range();
			reader.GetRowRange(ref tableItemRange);
			reader.GetColumnRange(ref tableItemRange);
			tableItemRange.StartRow++;
			tableItemRange.ColumnCount = 4;

			Logger.DEBUG($"\t\t-\tTable range:");
			Logger.DEBUG($"\t\t\t\t--\tStart row = {tableItemRange.StartRow}, start column = {tableItemRange.StartColumn}");
			Logger.DEBUG($"\t\t\t\t\tRow count = {tableItemRange.RowCount}, row column = {tableItemRange.ColumnCount}");

			var parameterInfoList = new List<ParameterInfo>();
			for (int index = 0; index < tableItemRange.RowCount; index++)
			{
				try
				{
					ParameterInfo parameterInfo = this.ReadFunctionInfo(reader, tableItemRange);
					parameterInfoList.Add(parameterInfo);
				}
				catch (ParseException)
				{
					Logger.WARN($"\t\t-\tSkip ({tableItemRange.StartRow}, {tableItemRange.StartColumn}) because empty cell found.");
				}
				catch (ParseDataNotFoundException)
				{
					Logger.WARN($"\t\t-\tSkip ({tableItemRange.StartRow}, {tableItemRange.StartColumn}) because invalid data found.");
				}
				catch (FormatException)
				{
					Logger.WARN($"\t\t-\tSkip ({tableItemRange.StartRow}, {tableItemRange.StartColumn}) because invalid format.");
				}
				tableItemRange.StartRow++;
			}
			return parameterInfoList;
		}

		/// <summary>
		/// Read function information 
		/// </summary>
		/// <param name="reader">Object to read test data information from excel file.</param>
		/// <param name="range">Range to read.</param>
		/// <returns><para>ParameterInfo</para> object read from excel.</returns>
		/// <exception cref="ParseException">One of cell in the <para>range</para> is empty.</exception>
		/// <exception cref="ParseDataNotFoundException">The range has no data. </exception>
		/// <exception cref="FormatException">The index can not convert into "int" type.</exception>
		protected ParameterInfo ReadFunctionInfo(ExcelReader reader, Range range)
		{
			IEnumerable<string> rowItem = reader.ReadRow(range);
			if (0 == rowItem.Count())
			{
				Logger.WARN($"\t\t-\tNo item found in row ({range.StartRow}.");
				throw new ParseDataNotFoundException(range);
			}

			try
			{
				foreach (var item in rowItem)
				{
					if ((string.IsNullOrWhiteSpace(item)) || (string.IsNullOrEmpty(item)))
					{
						throw new ParseException("Data is invalid");
					}
				}
				ParameterInfo parameterInfo = new ParameterInfo();
				parameterInfo.Index = Convert.ToInt32(rowItem.ElementAt(0));
				parameterInfo.Name = rowItem.ElementAt(1);
				parameterInfo.InfoName = rowItem.ElementAt(2);
				parameterInfo.FileName = rowItem.ElementAt(3);

				return parameterInfo;
			}
			catch (FormatException)
			{
				throw;
			}
		}
	}
}
