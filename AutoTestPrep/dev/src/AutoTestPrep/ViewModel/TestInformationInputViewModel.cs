using CSEngineer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.ViewModel
{
	/// <summary>
	/// View model about test information input view.
	/// </summary>
	public class TestInformationInputViewModel : ViewModelBase
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
		public TestInformationInputViewModel()
		{
			this.InputiFilePathVM = new InformationInputViewModel("入力(テスト定義ファイル)：", string.Empty);
			this.OutputPathVM = new InformationInputViewModel("出力ファイル：", string.Empty);
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
	}
}
