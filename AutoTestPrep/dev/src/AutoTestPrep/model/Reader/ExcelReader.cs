﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML;
using ClosedXML.Excel;

namespace AutoTestPrep.Model.Reader
{
	/// <summary>
	/// Read read and get data from excel file.
	/// </summary>
	public class ExcelReader
	{
		/// <summary>
		/// Stream of excel file to read.
		/// </summary>
		protected Stream _excelStream;

		protected IXLWorkbook _currentWorkBook;

		/// <summary>
		/// Sheet name to read.
		/// </summary>
		public string SheetName { get; set; }

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="filePath">Path to file to read.</param>
		public ExcelReader(Stream stream)
		{
			this._excelStream = stream;
		}

		/// <summary>
		/// Get range of cell which contains a string specified by argument item.
		/// </summary>
		/// <param name="item">String a cell should contain.</param>
		/// <returns>A range which contains string.</returns>
		/// <exception cref="ArgumentOutOfRangeException">A cell can not be found.</exception>
		public Range FindFirstItem(string item)
		{
			try
			{
				var workBook = new XLWorkbook(this._excelStream);
				var workSheet = workBook.Worksheet(this.SheetName);
				var itemCell = workSheet.CellsUsed()
					.Where(_ => (0 == string.Compare(item, _.GetString())))
					.FirstOrDefault();
				var range = new Range
				{
					StartRow = itemCell.Address.RowNumber,
					StartColumn = itemCell.Address.ColumnNumber,
				};

				return range;
			}
			catch (NullReferenceException)
			{
				throw new FormatException();
			}
		}

		/// <summary>
		/// Get range of cell in range which contains a string specified by argument item.
		/// </summary>
		/// <param name="item">String a cell should contain.</param>
		/// <returns>A range which contains string.</returns>
		/// <exception cref="ArgumentOutOfRangeException">A cell can not be found.</exception>
		public Range FindFirstItem(string item, Range range)
		{
			try
			{
				var workBook = new XLWorkbook(this._excelStream);
				var workSheet = workBook.Worksheet(this.SheetName);
				var itemCell = workSheet.CellsUsed()
					.Where(_ => (0 == string.Compare(item, _.GetString())) &&
						(range.StartRow <= _.Address.RowNumber) &&
						(range.StartColumn <= _.Address.ColumnNumber))
					.FirstOrDefault();
				Range itemRange = new Range
				{
					StartColumn = itemCell.Address.ColumnNumber,
					StartRow = itemCell.Address.RowNumber,
				};

				return itemRange;
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new ArgumentOutOfRangeException();
			}
		}

		/// <summary>
		/// Get range of cell in a row which contains a string specified by argument item.
		/// </summary>
		/// <param name="item">String a cell should contain.</param>
		/// <returns>A range which contains string.</returns>
		/// <exception cref="FormatException">A cell can not be found.</exception>
		public Range FindFirstItemInRow(string item, Range range)
		{
			try
			{
				var workBook = new XLWorkbook(this._excelStream);
				var workSheet = workBook.Worksheet(this.SheetName);
				var itemCell = workSheet.CellsUsed()
					.Where(_ => (0 == string.Compare(item, _.GetString())) &&
						(range.StartRow <= _.Address.RowNumber) &&
						(range.StartColumn == _.Address.ColumnNumber))
					.FirstOrDefault();
				Range itemRange = new Range
				{
					StartColumn = itemCell.Address.ColumnNumber,
					StartRow = itemCell.Address.RowNumber,
				};

				return itemRange;
			}
			catch (NullReferenceException)
			{
				throw new FormatException($"No cell contains {item} in {this.SheetName}.");
			}
		}

		/// <summary>
		/// Get range of cell in a column which contains a string specified by argument item.
		/// </summary>
		/// <param name="item">String a cell should contain.</param>
		/// <returns>A range which contains string.</returns>
		/// <exception cref="ArgumentOutOfRangeException">A cell can not be found.</exception>
		public Range FindFirstItemInColumn(string item, Range range)
		{
			try
			{
				var workBook = new XLWorkbook(this._excelStream);
				var workSheet = workBook.Worksheet(this.SheetName);
				var itemCell = workSheet.CellsUsed()
					.Where(_ => (0 == string.Compare(item, _.GetString())) &&
						(range.StartRow == _.Address.RowNumber) &&
						(range.StartColumn <= _.Address.ColumnNumber))
					.FirstOrDefault();
				Range itemRange = new Range
				{
					StartColumn = itemCell.Address.ColumnNumber,
					StartRow = itemCell.Address.RowNumber,
				};

				return itemRange;
			}
			catch (NullReferenceException)
			{
				throw new ArgumentOutOfRangeException();
			}
		}

		/// <summary>
		/// Get range of cell which contains the string specified by argument item.
		/// </summary>
		/// <param name="item">String a cell should contain.</param>
		/// <returns>A range which contains string.</returns>
		/// <exception cref="FormatException">A cell can not be found.</exception>
		public IEnumerable<Range> FindItem(string item)
		{
			var workBook = new XLWorkbook(this._excelStream);
			var workSheet = workBook.Worksheet(this.SheetName);
			var itemCells = workSheet.CellsUsed()
				.Where(_ => (0 == string.Compare(item, _.GetString())));
			if (0 == itemCells.Count())
			{
				/*
				 * A case that any cell contains "item" can not found in a sheet, it means
				 * that the format is invalid.
				 */
				throw new FormatException($"No cell contains {item} can be found in {this.SheetName}.");
			}
			var ranges = new List<Range>();
			foreach (var itemCell in itemCells)
			{
				Range range = new Range
				{
					StartRow = itemCell.Address.RowNumber,
					StartColumn = itemCell.Address.ColumnNumber,
				};
				ranges.Add(range);
			}
			return ranges;
		}

		/// <summary>
		/// Read a row.
		/// </summary>
		/// <param name="range">Range to read.</param>
		/// <returns>Items in a row.</returns>
		public IEnumerable<string> ReadRow(Range range)
		{
			var workBook = new XLWorkbook(this._excelStream);
			var workSheet = workBook.Worksheet(this.SheetName);
			var cellsInRow = workSheet.Cells()
				.Where(_ =>
					(_.Address.RowNumber == range.StartRow) &&
					(range.StartColumn <= _.Address.ColumnNumber) &&
					(_.Address.ColumnNumber <= workSheet.LastColumn().ColumnNumber()));
			List<string> items = new List<string>();
			foreach (var cellInRow in cellsInRow)
			{
				items.Add(cellInRow.GetString());
			}

			return items;
		}

		public IEnumerable<string> ReadColumn(Range range)
		{
			var workBook = new XLWorkbook(this._excelStream);
			var workSheet = workBook.Worksheet(this.SheetName);
			var cellsInColumn = workSheet.Cells()
				.Where(_ =>
					(range.StartRow <= _.Address.RowNumber) &&
					(_.Address.RowNumber <= (workSheet.LastRowUsed().RowNumber())) &&
					(_.Address.ColumnNumber == range.StartColumn));
			List<string> items = new List<string>();
			foreach (var cellInColumn in cellsInColumn)
			{
				items.Add(cellInColumn.GetString());
			}

			return items;
		}

		/// <summary>
		/// Get merged cell range.
		/// </summary>
		/// <param name="range">Range of Merged cell.</param>
		public virtual void GetMergedCellRange(ref Range range)
		{
			var workBook = new XLWorkbook(this._excelStream);
			var workSheet = workBook.Worksheet(this.SheetName);

			//行ヘッダが結合されているか確認
			if (workSheet.Cell(range.StartRow, range.StartColumn).IsMerged())
			{
				/*
				 *	セルが結合されている
				 *		->	行ヘッダの範囲から、何行結合しているかを判定する。
				 */
				var mergedRange = workSheet.Cell(range.StartRow, range.StartColumn).MergedRange();
				var firstCell = mergedRange.FirstCell();
				var lastCell = mergedRange.LastCell();
				range.RowCount = lastCell.Address.RowNumber - firstCell.Address.RowNumber + 1;
				range.StartRow = firstCell.Address.RowNumber;
			}
			else
			{
				//セルが結合されていない場合
				range.RowCount = 1;
			}
		}

		/// <summary>
		/// Get range about row.
		/// </summary>
		/// <param name="range">Range object to set result.</param>
		public virtual void GetRowRange(ref Range range)
		{
			var workBook = new XLWorkbook(this._excelStream);
			var workSheet = workBook.Worksheet(this.SheetName);

			var firstUsedCell = workSheet.FirstRowUsed();
			range.StartRow = firstUsedCell.RowNumber();

			var lastUsedCell = workSheet.LastRowUsed();
			range.RowCount = lastUsedCell.RowNumber() - firstUsedCell.RowNumber() + 1;
		}

		/// <summary>
		/// Get range about column.
		/// </summary>
		/// <param name="range">Range object to set the result.</param>
		public virtual void GetColumnRange(ref Range range)
		{
			var workBook = new XLWorkbook(this._excelStream);
			var workSheet = workBook.Worksheet(this.SheetName);

			var firstUsedCell = workSheet.FirstColumnUsed();
			range.StartColumn = firstUsedCell.ColumnNumber();

			var lastUsedCell = workSheet.LastColumnUsed();
			range.ColumnCount = lastUsedCell.ColumnNumber() - firstUsedCell.ColumnNumber() + 1;
		}
	}
}
