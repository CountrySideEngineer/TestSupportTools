using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model
{
	/// <summary>
	/// Parameter class
	/// </summary>
	public class Parameter
	{
		/// <summary>
		/// Definition of access mode.
		/// </summary>
		public enum AccessMode {
			In,     //Input
			Out,    //Output
			Both,   //Input and Output
			None,   //No access, error mode.
		}

		public static AccessMode ToMode(string mode)
		{
			var accessMode = AccessMode.None;
			if (0 == string.Compare(mode, "in", true))
			{
				accessMode = AccessMode.In;
			}
			else if (0 == string.Compare(mode, "out", true))
			{
				accessMode = AccessMode.Out;
			}
			else if (0 == string.Compare(mode, "in/out", true))
			{
				accessMode = AccessMode.Both;
			}
			else
			{
				throw new ArgumentException();
			}
			return accessMode;
		}

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Parameter()
		{
			this.Name = string.Empty;
			this.DataType = string.Empty;
			this.Prefix = string.Empty;
			this.Postifx = string.Empty;
			this.PointerNum = 0;
			this.Mode = AccessMode.None;
			this.Overview = string.Empty;
			this.Description = string.Empty;
			this.Parameters = null;
		}

		/// <summary>
		/// Copy constructor of Parameter object.
		/// </summary>
		/// <param name="src">Source Parameter object.</param>
		public Parameter(Parameter src)
		{
			this.Name = src.Name;
			this.DataType = src.DataType;
			this.Prefix = src.Prefix;
			this.Postifx = src.Postifx;
			this.PointerNum = src.PointerNum;
			this.Mode = src.Mode;
			this.Overview = src.Overview;
			this.Description = src.Description;
			try
			{
				var subParamList = new List<Parameter>(src.Parameters.Count());
				foreach (var srcParam in src.Parameters)
				{
					var subParam = new Parameter(srcParam);
					subParamList.Add(subParam);
				}
				this.Parameters = subParamList;
			}
			catch (NullReferenceException)
			{
				//The src parameter does not have argument.
			}

			try
			{
				var children = new List<Parameter>(src.Children.Count());
				foreach (var srcChild in src.Children)
				{
					var child = new Parameter(srcChild);
					children.Add(child);
				}
				this.Children = children;
			}
			catch (NullReferenceException)
			{
				//The src parameter does not have children.
			}
		}

		/// <summary>
		/// String data of access mode input.
		/// </summary>
		protected static string ModeInput = "input";

		/// <summary>
		/// String data of access mode output.
		/// </summary>
		protected static string ModeOutput = "output";

		/// <summary>
		/// String data of access mode both, input and output.
		/// </summary>
		protected static string ModeBoth = "in/out";

		/// <summary>
		/// Name of parameter
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Name of data type.
		/// </summary>
		public string DataType { get; set; }

		/// <summary>
		/// Actual data type.
		/// </summary>
		public string ActualDataType
		{
			get
			{
				string actualDataType = string.Empty;
				if (!(string.IsNullOrEmpty(this.Prefix)) &&
					(!(string.IsNullOrWhiteSpace(this.Prefix))))
				{
					actualDataType += this.Prefix;
					actualDataType += " ";
				}
				actualDataType += this.DataType;
				for (int index = 0; index < this.PointerNum; index++)
				{
					actualDataType += "*";
				}
				actualDataType += " ";
				actualDataType += this.Name;
				if (!(string.IsNullOrEmpty(this.Postifx)) &&
					(!(string.IsNullOrWhiteSpace(this.Postifx))))
				{
					actualDataType += " ";
					actualDataType += this.Postifx;
				}
				return actualDataType;
			}
		}

		/// <summary>
		/// Prefix of data type.
		/// </summary>
		public string Prefix { get; set; }

		/// <summary>
		/// Postfix of data type.
		/// </summary>
		public string Postifx { get; set; }

		/// <summary>
		/// Number of pointer.
		/// </summary>
		public int PointerNum { get; set; }

		/// <summary>
		/// Access mode, input, output, or both.
		/// </summary>
		public AccessMode Mode { get; set; }

		/// <summary>
		/// Overview of this parameter.
		/// </summary>
		public string Overview { get; set; }

		/// <summary>
		/// Description of this 
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Sub parameters
		/// </summary>
		public IEnumerable<Parameter> Parameters { get; set; }

		/// <summary>
		/// Children parameter.
		/// </summary>
		public IEnumerable<Parameter> Children { get; set; }

		/// <summary>
		/// Returns parameter object in string.
		/// </summary>
		/// <returns>Parameter object in strign data.</returns>
		public override string ToString()
		{
			string subParameter = string.Empty;
			try
			{
				foreach (var subParam in this.Parameters)
				{
					if (!(string.IsNullOrEmpty(subParameter)))
					{
						subParameter += ", ";
					}
					subParameter += subParam.ToString();
				}
			}
			catch (NullReferenceException)
			{
				//Skip parameter sequence.
			}
			string toString = this.ActualDataType;
			if (!(string.IsNullOrEmpty(subParameter)))
			{
				toString += "(" + subParameter + ")";
			}

			return toString;
		}
	}
}
