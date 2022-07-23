using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestParser.Config
{
	/// <summary>
	/// Configuration of table.
	/// </summary>
	public class TableConfig
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public TableConfig()
		{
			Name = string.Empty;
			TableTopRowOffset = 0;
			TableTopColOffset = 0;
		}

		/// <summary>
		/// Table name.
		/// </summary>
		[XmlElement("Name")]
		public string Name { get; set; }

		/// <summary>
		/// Offset of table top in row from "name" cell.
		/// </summary>
		[XmlElement("TableRowOffset")]
		public int TableTopRowOffset { get; set; }

		/// <summary>
		/// Offset of table top in column from "name" cell.
		/// </summary>
		[XmlElement("TableTopColOffset")]
		public int TableTopColOffset { get; set; }

		/// <summary>
		/// Offset to data in column from table top.
		/// </summary>
		[XmlElement("RowDataOffset")]
		public int RowDataOffset { get; set; }

		/// <summary>
		/// Offset to data in row from table top.
		/// </summary>
		[XmlElement("ColDataOffset")]
		public int ColDataOffset { get; set; }
	}
}
