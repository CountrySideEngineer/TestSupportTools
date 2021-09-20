using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestParser.Parameter
{
	public class ParameterInfo
	{
		/// <summary>
		/// Index of parameter in list.
		/// </summary>
		public int Index { get; set; }

		/// <summary>
		/// Name of parameter.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Information about parameter.
		/// (A "function" case, this means name of sheet the datas are defined.
		/// </summary>
		public string InfoName { get; set; }

		/// <summary>
		/// Name of file.
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ParameterInfo()
		{
			this.Index = 0;
			this.Name = string.Empty;
			this.InfoName = string.Empty;
			this.FileName = string.Empty;
		}
	}
}
