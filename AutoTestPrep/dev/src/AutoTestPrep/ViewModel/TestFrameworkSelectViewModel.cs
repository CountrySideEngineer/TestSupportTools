using AutoTestPrep.Model;
using AutoTestPrep.Model.InputInfos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.ViewModel
{
	public class TestFrameworkSelectViewModel : AutoTestPrepViewModelBase
	{
		/// <summary>
		/// Field of available test framework list.
		/// </summary>
		protected ObservableCollection<string> _frameworkList;

		/// <summary>
		/// Field of index of selected item in control.
		/// </summary>
		protected int _selectedItemIndex;

		/// <summary>
		/// Default constructor
		/// </summary>
		public TestFrameworkSelectViewModel() : this(-1) { }

		/// <summary>
		/// Constructor with argument about index in view model list.
		/// </summary>
		/// <param name="index">Index of view model list.</param>
		public TestFrameworkSelectViewModel(int index) : base(index)
		{
			this._frameworkList = new ObservableCollection<string>
			{
				"google test", "min_unit"
			}; ;
			this.SelectedItemIndex = 0;
		}

		/// <summary>
		/// Property of available framework list.
		/// </summary>
		public ObservableCollection<string> FrameworkNameList
		{
			get => this._frameworkList;
			protected set
			{
				this._frameworkList = value;
				this.RaisePropertyChanged(nameof(FrameworkNameList));
			}
		}

		/// <summary>
		/// Property of index of selected item in control.
		/// </summary>
		public int SelectedItemIndex
		{
			get => this._selectedItemIndex;
			set
			{
				this._selectedItemIndex = value; 
				this.RaisePropertyChanged(nameof(this.SelectedItemIndex));
			}
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override void RestoreTestInforamtion(TestDataInfo testDataInfo)
		{
			int frameworkIndex = ((int)testDataInfo.FrameworkTye);
			this.SelectedItemIndex = frameworkIndex;
		}

		public override void SetupTestInfomation(ref TestDataInfo testDataInfo)
		{
			TestFramework.Framework framework = TestFramework.ToFramework(this.SelectedItemIndex);
			testDataInfo.FrameworkTye = framework;
		}

		public override string ToString()
		{
			string frameworkName = "Framework:";
			frameworkName += TestFramework.ToFramework(this.SelectedItemIndex).ToString();
			return frameworkName;
		}
	}
}
