using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model
{
	public class Function : Parameter
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public Function() : base()
		{
			this.Arguments = null;
			this.SubFuntions = null;
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="src"></param>
		public Function(Function src) : base(src)
		{
			if (src is Function)
			{
				this.SubFuntions = new List<Function>(src.SubFuntions);
				this.Arguments = new List<Parameter>(src.Arguments);
			}
		}

		/// <summary>
		/// List of sub functions.
		/// </summary>
		public IEnumerable<Function> SubFuntions { get; set; }

		/// <summary>
		/// List of  arguments.
		/// </summary>
		public IEnumerable<Parameter> Arguments { get; set; }

		/// <summary>
		/// Create string of funtion definition.
		/// </summary>
		/// <returns>Function definition in string.</returns>
		public override string ToString()
		{
			var toString = base.ToString();
			toString += "(";
			try
			{
				bool isTop = true;
				foreach (var argument in this.Arguments)
				{
					if (!isTop)
					{
						toString += ", ";
					}
					toString += argument.ToString();
					isTop = false;
				}
			}
			catch (NullReferenceException)
			{
				//No argumetn -> Skip!
			}
			toString += ")";

			return toString;
		}
	}
}
