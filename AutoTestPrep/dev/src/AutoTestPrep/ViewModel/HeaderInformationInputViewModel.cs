using CSEngineer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.ViewModel
{
	/// <summary>
	/// View model class of HeaderInforamtionView
	/// </summary>
	public class HeaderInformationInputViewModel : AutoTestPrepViewModelBase
	{
		/// <summary>
		/// Field of standard header view model.
		/// </summary>
		protected MultiLineInputViewModel _StandardHeaderVM;

		/// <summary>
		/// Field of user header view model.
		/// </summary>
		protected MultiLineInputViewModel _UserHeaderVM;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public HeaderInformationInputViewModel() : this(-1) { }

		public HeaderInformationInputViewModel(int index) : base(index)
		{
			this.StandartHeaderVM = new MultiLineInputViewModel("標準ヘッダ：", string.Empty);
			this.UserHeaderVM = new MultiLineInputViewModel("ユーザヘッダ", string.Empty);
		}



		/// <summary>
		/// Property of standar header view model.
		/// </summary>
		public MultiLineInputViewModel StandartHeaderVM
		{
			get
			{
				return this._StandardHeaderVM;
			}
			set
			{
				this._StandardHeaderVM = value;
				this.RaisePropertyChanged(nameof(StandartHeaderVM));
			}
		}

		/// <summary>
		/// Property of user header view model.
		/// </summary>
		public MultiLineInputViewModel UserHeaderVM
		{
			get
			{
				return this._UserHeaderVM;
			}
			set
			{
				this._UserHeaderVM = value;
				this.RaisePropertyChanged(nameof(UserHeaderVM));
			}
		}

		/// <summary>
		/// Standart headers.
		/// </summary>
		public string StandartHeader
		{
			get
			{
				return this.StandartHeaderVM.InputItem;
			}
			set
			{
				this.StandartHeaderVM.InputItem = value;
			}
		}

		/// <summary>
		/// User headers.
		/// </summary>
		public string UserHeader
		{
			get
			{
				return this.UserHeaderVM.InputItem;
			}
			set
			{
				this.UserHeaderVM.InputItem = value;
			}
		}
	}
}
