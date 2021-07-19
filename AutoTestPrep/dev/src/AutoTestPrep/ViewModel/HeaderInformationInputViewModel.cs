using AutoTestPrep.Model.InputInfos;
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
		/// Field of include directory view model.
		/// </summary>
		protected MultiLineInputViewModel _IncludeDirectoryVM;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public HeaderInformationInputViewModel() : this(-1) { }

		public HeaderInformationInputViewModel(int index) : base(index)
		{
			this.StandartHeaderVM = new MultiLineInputViewModel("標準ヘッダ：", string.Empty);
			this.UserHeaderVM = new MultiLineInputViewModel("ユーザヘッダ", string.Empty);
			this.IncludeDirectoryVM = new MultiLineInputViewModel("インクルードディレクトリ", string.Empty);
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
		/// Property of include directory view model.
		/// </summary>
		public MultiLineInputViewModel IncludeDirectoryVM
		{
			get
			{
				return this._IncludeDirectoryVM;
			}
			set
			{
				this._IncludeDirectoryVM = value;
				this.RaisePropertyChanged(nameof(IncludeDirectoryVM));
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

		/// <summary>
		/// Include directory path.
		/// </summary>
		public string IncludeDirectory
		{
			get
			{
				return this.IncludeDirectoryVM.InputItem;
			}
			set
			{
				this.IncludeDirectoryVM.InputItem = value;
			}
		}

		protected IEnumerable<string> ToEnumrable(string inputData)
		{
			var enumerableValue = inputData.Replace("\r\n", "\n").Split(new[] { '\n', '\r' });
			return enumerableValue;
		}
	}
}
