using gtest_gui.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

		protected DelegateCommand _setTestFileByUserCommand;

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

		/// <summary>
		/// Command to let user to select target test file.
		/// </summary>
		public DelegateCommand SetTestFileByUserCommand
		{
			get
			{
				if (null == this._setTestFileByUserCommand)
				{
					this._setTestFileByUserCommand = new DelegateCommand(this.SetTestFileByUserCommandExecute);
				}
				return this._setTestFileByUserCommand;
			}
		}

		/// <summary>
		/// Actual command function to select target test file.
		/// </summary>
		public void SetTestFileByUserCommandExecute()
		{
			var dialog = new OpenFileDialog();
			dialog.Title = "ファイルを開く";
			dialog.Filter = "(*.exe)|*.exe";
			if (true == dialog.ShowDialog())
			{
				this.TestFilePath = dialog.FileName;
			}
		}
	}
}
