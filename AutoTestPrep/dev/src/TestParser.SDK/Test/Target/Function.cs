using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestParser.Target
{
	public class Function : Parameter
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public Function() : base()
		{
			this.Arguments = new List<Parameter>(0);
			this.SubFunctions = new List<Function>(0);
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="src"></param>
		public Function(Function src) : base(src)
		{
			if (src is Function)
			{
				this.SubFunctions = new List<Function>(src.SubFunctions);
				this.Arguments = new List<Parameter>(src.Arguments);
			}
		}

		/// <summary>
		/// List of sub functions.
		/// </summary>
		public IEnumerable<Function> SubFunctions { get; set; }

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

		/// <summary>
		/// Returns whether the function will return any value or not.
		/// </summary>
		/// <returns>
		/// Returns true if the function will return any value, 
		/// otherwise return false.
		/// </returns>
		public bool HasReturn()
		{
			bool hasReturn = false;
			if ((("void").Equals(this.DataType.ToLower(), StringComparison.Ordinal)) &&
				(PointerNum <= 0))
			{
				//A case that the data type without pointer means the function returns no value via return value.
				hasReturn = false;
			}
			else
			{
				hasReturn = true;
			}
			return hasReturn;
		}
	}
}
