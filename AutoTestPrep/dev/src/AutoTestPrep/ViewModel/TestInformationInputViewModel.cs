using AutoTestPrep.Model.InputInfos;
using CSEngineer.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.ViewModel
{
	/// <summary>
	/// View model about test information input view.
	/// </summary>
	public class TestInformationInputViewModel : AutoTestPrepViewModelBase
	{
		/// <summary>
		/// Field of view model about input file path.
		/// </summary>
		protected InformationInputViewModel _InputFilePathVM;

		/// <summary>
		/// Field of view model about output path.
		/// </summary>
		protected InformationInputViewModel _OutputPathVM;

		/// <summary>
		/// Default constructor
		/// </summary>
		public TestInformationInputViewModel() : this(-1) { }

		public TestInformationInputViewModel(int index) : base(index)
		{
			this.InputiFilePathVM = new InformationInputViewModel("入力(テスト定義ファイル)：", string.Empty);
			this.OutputPathVM = new InformationInputViewModel("出力ファイル：", string.Empty);

			this.InputiFilePathVM.PropertyChanged += this.UserInputPropertyChanged;
		}

		/// <summary>
		/// Property of view model about input file path.
		/// </summary>
		public InformationInputViewModel InputiFilePathVM
		{
			get
			{
				return this._InputFilePathVM;
			}
			set
			{
				this._InputFilePathVM = value;
				this.RaisePropertyChanged(nameof(InputiFilePathVM));
			}
		}

		/// <summary>
		/// Property of view model about output path.
		/// </summary>
		public InformationInputViewModel OutputPathVM
		{
			get
			{
				return this._OutputPathVM;
			}
			set
			{
				this._OutputPathVM = value;
				this.RaisePropertyChanged(nameof(OutputPathVM));
			}
		}

		/// <summary>
		/// Path of test information file.
		/// </summary>
		public string InputFilePath
		{
			get
			{
				return this.InputiFilePathVM.InputItem;
			}
			set
			{
				this.InputiFilePathVM.InputItem = value;
			}
		}

		/// <summary>
		/// Path to output.
		/// </summary>
		public string OutputPath
		{
			get
			{
				return this.OutputPathVM.InputItem;
			}
			set
			{
				this.OutputPathVM.InputItem = value;
			}
		}

		public override void SetupTestInfomation(ref TestDataInfo testDataInfo)
		{
			testDataInfo.TestDataFilePath = this.InputFilePath;
			testDataInfo.OutputDirectoryPath = this.OutputPath;
		}

		/// <summary>
		/// Property of InputFilePathVM object changed event handler.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event argument.</param>
		public void UserInputPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName.Equals(nameof(this.InputiFilePathVM.InputItem)))
			{
				if ((string.IsNullOrEmpty(this.OutputPath)) ||
					(string.IsNullOrWhiteSpace(this.OutputPath)))
				{
					if (System.IO.File.Exists(this.InputFilePath))
					{
						string inputFileParentDirPath = System.IO.Path.GetDirectoryName(this.InputFilePath);
						this.OutputPath = inputFileParentDirPath;
					}
				}
			}
		}
	}
}
