using System;
using System.Collections.Generic;
using System.Text;

namespace gtest_gui.ViewModel
{
	/// <summary>
	/// Main view model class of gtest_gui application.
	/// </summary>
	public class GTestGuiViewModel : ViewModelBase
	{
		/// <summary>
		/// Test file path field.
		/// </summary>
		protected string _testFilePath;

		/// <summary>
		/// Field of which a test can run or not.
		/// </summary>
		protected bool _canRunTest;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public GTestGuiViewModel()
		{
			this.TestFilePath = string.Empty;
			this.CanRunTest = false;
		}

		/// <summary>
		/// Test file path.
		/// </summary>
		public string TestFilePath
		{
			get
			{
				return this._testFilePath;
			}
			set
			{
				this._testFilePath = value;
				this.RaisePropertyChanged("TestFilePath");
			}
		}

		/// <summary>
		/// Gets a values indicating whether the tests listed on and selected by user can run or not.
		/// </summary>
		public bool CanRunTest
		{
			get
			{
				return this._canRunTest;
			}
			set
			{
				this._canRunTest = value;
				this.RaisePropertyChanged("CanRunTest");
			}
		}
	}
}
