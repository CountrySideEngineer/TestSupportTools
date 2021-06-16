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

		public MainWindowsViewModel()
		{
			this.TestConfigurationItems = new ObservableCollection<string>
			{
				"テスト情報",
				"スタブ情報",
				"ヘッダ情報"
			};
			this.SelectedConfigurationItemIndex = 1;
			
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
	}
}
