using CSEngineer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.ViewModel
{
	/// <summary>
	/// View model class of information input view.
	/// </summary>
	public class InformationInputViewModel : ViewModelBase
	{
		/// <summary>
		/// Title of iput item.
		/// </summary>
		protected string _InputItemTitle;

		/// <summary>
		/// Field of input item.
		/// </summary>
		protected string _InputItem;

		/// <summary>
		/// Default constructor
		/// </summary>
		public InformationInputViewModel() : this(string.Empty, string.Empty) { }

		/// <summary>
		/// Constructor with tilte and item.
		/// </summary>
		/// <param name="title">Title of input item.</param>
		/// <param name="item">Input item.</param>
		public InformationInputViewModel(string title, string item)
		{
			this.InputItemTitle = title;
			this.InputItem = item;
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
		/// Propert of input item.
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
	}
}
