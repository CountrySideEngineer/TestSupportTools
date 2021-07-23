using CSEngineer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.ViewModel
{
	public class MultiLineInputViewModel : ViewModelBase
	{
		/// <summary>
		/// Field of input item title.
		/// </summary>
		protected string _InputItemTitle;

		/// <summary>
		/// Field of input items.
		/// </summary>
		protected string _InputItem;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public MultiLineInputViewModel() : this(string.Empty, string.Empty) { }

		/// <summary>
		/// Constructor with arguments.
		/// </summary>
		/// <param name="inputItemTitle">Input item title.</param>
		/// <param name="inputItem">Default input item.</param>
		public MultiLineInputViewModel(string inputItemTitle, string inputItem)
		{
			this.InputItemTitle = inputItemTitle;
			this.InputItem = inputItem;
		}

		/// <summary>
		/// Property of input item title.
		/// </summary>
		public string InputItemTitle
		{
			get
			{
				return this._InputItemTitle;
			}
			protected set
			{
				this._InputItemTitle = value;
				this.RaisePropertyChanged(nameof(InputItemTitle));
			}
		}

		/// <summary>
		/// Property input items.
		/// </summary>
		public string InputItem
		{
			get
			{
				return this._InputItem;
			}
			set
			{
				this._InputItem = value;
				this.RaisePropertyChanged(nameof(InputItem));
			}
		}

		/// <summary>
		/// Returns InputItem property in IEnumerable object.
		/// </summary>
		/// <returns>InputItem in IEnumerable.</returns>
		public IEnumerable<string> InputItemInIenumerable()
		{
			var enumerableValue = this.InputItem.Replace("\r\n", "\n").Split(new[] { '\n', '\r' });
			return enumerableValue;
		}

		public virtual string EnumerableToMultilineString(IEnumerable<string> src)
		{
			string multilineContent = string.Empty;
			bool isTop = true;
			foreach (var srcItem in src)
			{
				if ((string.IsNullOrEmpty(srcItem)) || (string.IsNullOrWhiteSpace(srcItem))) {

				}


				if (!isTop)
				{
					multilineContent += "\r\n";
				}
				isTop = false;
				multilineContent += srcItem;

			}
			return multilineContent;
		}
	}
}
