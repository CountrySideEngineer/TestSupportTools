using CSEngineer.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.ViewModel
{
	public class MainWindowsViewModel : ViewModelBase
	{
		protected ObservableCollection<string> _testConfigurationItems;
		protected int _selectedConfigurationItemIndex;

		protected TestInformationInputViewModel _TestInformationInputVM;

		protected BufferSizeViewModel _BufferSizeVM;

		protected HeaderInformationInputViewModel _HeaderInformationVM;

		public MainWindowsViewModel()
		{
			this.TestConfigurationItems = new ObservableCollection<string>
			{
				"テスト情報",
				"スタブ情報",
				"ヘッダ情報"
			};
			this.SelectedConfigurationItemIndex = 0;

			this.TestInformationInputVM = new TestInformationInputViewModel();
			this.BufferSizeVM = new BufferSizeViewModel();
			this.HeaderInformationVM = new HeaderInformationInputViewModel();
		}

		public ObservableCollection<string> TestConfigurationItems
		{
			get
			{
				return this._testConfigurationItems;
			}
			protected set
			{
				this._testConfigurationItems = value;
				this.RaisePropertyChanged(nameof(TestConfigurationItems));
			}
		}
		public int SelectedConfigurationItemIndex
		{
			get
			{
				return this._selectedConfigurationItemIndex;
			}
			set
			{
				this._selectedConfigurationItemIndex = value;
				this.RaisePropertyChanged(nameof(_selectedConfigurationItemIndex));
			}
		}

		/// <summary>
		/// Property of view model object about test information input.
		/// </summary>
		public TestInformationInputViewModel TestInformationInputVM
		{
			get
			{
				return this._TestInformationInputVM;
			}
			set
			{
				this._TestInformationInputVM = value;
				this.RaisePropertyChanged(nameof(TestInformationInputVM));
			}
		}

		/// <summary>
		/// Property of view model object about buffer size input.
		/// </summary>
		public BufferSizeViewModel BufferSizeVM
		{
			get
			{
				return this._BufferSizeVM;
			}
			set
			{
				this._BufferSizeVM = value;
				this.RaisePropertyChanged(nameof(BufferSizeVM));
			}
		}

		/// <summary>
		/// Property of view model of header files.
		/// </summary>
		public HeaderInformationInputViewModel HeaderInformationVM
		{
			get
			{
				return this._HeaderInformationVM;
			}
			set
			{
				this._HeaderInformationVM = value;
				this.RaisePropertyChanged(nameof(HeaderInformationVM));
			}
		}
	}
}
