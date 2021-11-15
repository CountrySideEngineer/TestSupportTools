using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestParser.Reader
{
	public class Range
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		public Range()
		{
			this.StartRow = 0;
			this.StartColumn = 0;
			this.RowCount = 0;
			this.ColumnCount = 0;
		}

		/// <summary>
		/// Copy constructor.
		/// </summary>
		/// <param name="src">Base Range object.</param>
		public Range(Range src)
		{
			this.StartRow = src.StartRow;
			this.StartColumn = src.StartColumn;
			this.RowCount = src.RowCount;
			this.ColumnCount = src.ColumnCount;
		}

		public int StartRow { get; set; }

		public int StartColumn { get; set; }

		public int RowCount { get; set; }

		public int ColumnCount { get; set; }
	}
}
