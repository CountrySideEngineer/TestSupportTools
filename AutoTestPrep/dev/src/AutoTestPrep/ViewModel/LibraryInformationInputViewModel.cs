using AutoTestPrep.Model.InputInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.ViewModel
{
	/// <summary>
	/// View model class of LibraryInformationView.
	/// </summary>
	public class LibraryInformationInputViewModel : AutoTestPrepViewModelBase
	{
		/// <summary>
		/// Field of library files view model.
		/// </summary>
		protected MultiLineInputViewModel _LibraryInputVM;

		/// <summary>
		/// Field of library scan directory view model.
		/// </summary>
		protected MultiLineInputViewModel _LibraryDirectoryVM;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public LibraryInformationInputViewModel() : base(-1) { }

		/// <summary>
		/// Constructor with argument with index.
		/// </summary>
		/// <param name="index">Index in list.</param>
		public LibraryInformationInputViewModel(int index) : base(index)
		{
			this.LibraryInputVM = new MultiLineInputViewModel("ライブラリ：", string.Empty);
			this.LibraryDirectoryVM = new MultiLineInputViewModel("ライブラリディレクトリ", string.Empty);
		}

		/// <summary>
		/// Property of library file name view model.
		/// </summary>
		public MultiLineInputViewModel LibraryInputVM
		{
			get
			{
				return this._LibraryInputVM;
			}
			set
			{
				this._LibraryInputVM = value;
				this.RaisePropertyChanged(nameof(LibraryInputVM));
			}
		}

		/// <summary>
		/// Property of library scan file path view model.
		/// </summary>
		public MultiLineInputViewModel LibraryDirectoryVM
		{
			get
			{
				return this._LibraryDirectoryVM;
			}
			set
			{
				this._LibraryDirectoryVM = value;
				this.RaisePropertyChanged(nameof(LibraryDirectoryVM));
			}
		}

		/// <summary>
		/// Property of library file name in string.
		/// </summary>
		public string LibraryInput
		{
			get
			{
				return this.LibraryInputVM.InputItem;
			}
			set
			{
				this.LibraryInputVM.InputItem = value;
			}
		}

		/// <summary>
		/// Proeprty of library scan directory in string.
		/// </summary>
		public string LibraryDirectory
		{
			get
			{
				return this.LibraryDirectoryVM.InputItem;
			}
			set
			{
				this.LibraryDirectoryVM.InputItem = value;
			}
		}

		/// <summary>
		/// Returns collection of library file name.
		/// </summary>
		/// <returns>Collection of library file name.</returns>
		public IEnumerable<string> LibraryInputInIEnumerable()
		{
			return this.LibraryInputVM.InputItemInIenumerable();
		}

		/// <summary>
		/// Returns collection of library scan directory path.
		/// </summary>
		/// <returns>Colletion of library scan directory path.</returns>
		public IEnumerable<string> LibraryDirectoryInIEnumerable()
		{
			return this.LibraryDirectoryVM.InputItemInIenumerable();
		}

		/// <summary>
		/// Setup test user input data into
		/// Set the data entered by users in the object specified by the argument.
		/// </summary>
		/// <param name="testDataInfo">Object to set input data.</param>
		public override void SetupTestInfomation(ref TestDataInfo testDataInfo)
		{
			testDataInfo.LibraryNames = this.LibraryInputInIEnumerable();
			testDataInfo.LibraryDirectoryPath = this.LibraryDirectoryInIEnumerable();
		}

		/// <summary>
		/// Restore the data in object specified by argument.
		/// </summary>
		/// <param name="testDataInfo">Source data object.</param>
		public override void RestoreTestInforamtion(TestDataInfo testDataInfo)
		{
			this.LibraryInput = this.LibraryInputVM.EnumerableToMultilineString(testDataInfo.LibraryNames);
			this.LibraryDirectory = this.LibraryDirectoryVM.EnumerableToMultilineString(testDataInfo.LibraryDirectoryPath);
		}
	}
}
