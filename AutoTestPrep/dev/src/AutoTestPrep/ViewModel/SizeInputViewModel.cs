using CSEngineer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.ViewModel
{
	/// <summary>
	/// View model class of SizeInputView.
	/// </summary>
	public class SizeInputViewModel : ViewModelBase
	{
		/// <summary>
		/// Title of input data.
		/// </summary>
		protected string _ItemTitle;

		/// <summary>
		/// Value of input.
		/// </summary>
		protected long _ItemValue;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public SizeInputViewModel() : this(string.Empty, 0) { }

		/// <summary>
		/// Constructor with arguments.
		/// </summary>
		/// <param name="itemTitle">Title of input data.</param>
		/// <param name="itemValue">Initial size value.</param>
		public SizeInputViewModel(string itemTitle, long itemValue)
		{
			this.ItemTitle = itemTitle;
			this.ItemValue = itemValue;
		}

		/// <summary>
		/// Property of input data title.
		/// </summary>
		public string ItemTitle
		{
			get
			{
				return this._ItemTitle;
			}
			protected set
			{
				this._ItemTitle = value;
				this.RaisePropertyChanged(nameof(ItemTitle));
			}
		}

		/// <summary>
		/// Property of input value.
		/// </summary>
		public long ItemValue
		{
			get
			{
				return this._ItemValue;
			}
			set
			{
				this._ItemValue = value;
				this.RaisePropertyChanged(nameof(ItemValue));
			}
		}
	}
}
