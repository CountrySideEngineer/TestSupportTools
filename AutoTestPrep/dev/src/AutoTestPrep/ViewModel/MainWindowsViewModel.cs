﻿using AutoTestPrep.Command;
using AutoTestPrep.Model.EventArgs;
using AutoTestPrep.Model.InputInfos;
using CSEngineer.ViewModel;
using CStubMKGui.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.ViewModel
{
	public class MainWindowsViewModel : ViewModelBase
	{
		protected ObservableCollection<string> _testConfigurationItems;
		protected int _selectedConfigurationItemIndex;

		/// <summary>
		/// View model of test inforamtion input view.
		/// </summary>
		protected TestInformationInputViewModel _TestInformationInputVM;

		/// <summary>
		/// View model field to input stub buffer size.
		/// </summary>
		protected BufferSizeViewModel _BufferSizeVM;

		/// <summary>
		/// View model field to input header file information.
		/// </summary>
		protected HeaderInformationInputViewModel _HeaderInformationVM;

		/// <summary>
		/// View model field of input library file information.
		/// </summary>
		protected LibraryInformationInputViewModel _LibraryInformationVM;

		/// <summary>
		/// View model field of input define macro information.
		/// </summary>
		protected DefineMacroInputViewModel _DefineMacroVM;

		protected DelegateCommand _RunCommand;

		/// <summary>
		/// Event handler to handle a event 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public delegate void SelectedChangedEventHandler(object sender, SelectedStateChangedEventArgs e);
		public event SelectedChangedEventHandler SelectedChanged;

		public delegate void SetupTestInformationRequest(ref TestDataInfo testDataInf);
		public event SetupTestInformationRequest SetupTestInformationReq;

		/// <summary>
		/// Default consttuctor
		/// </summary>
		public MainWindowsViewModel()
		{
			this.TestConfigurationItems = new ObservableCollection<string>
			{
				"テスト情報",
				"スタブ情報",
				"ヘッダ情報",
				"ライブラリ情報",
				"マクロ情報"
			};

			this.TestInformationInputVM = new TestInformationInputViewModel(0);
			this.BufferSizeVM = new BufferSizeViewModel(1);
			this.HeaderInformationVM = new HeaderInformationInputViewModel(2);
			this.LibraryInforamtionVM = new LibraryInformationInputViewModel(3);
			this.DefineMacroVM = new DefineMacroInputViewModel(4);

			this.SelectedChanged += TestInformationInputVM.SelectedStateChangedEventHandler;
			this.SelectedChanged += BufferSizeVM.SelectedStateChangedEventHandler;
			this.SelectedChanged += HeaderInformationVM.SelectedStateChangedEventHandler;
			this.SelectedChanged += LibraryInforamtionVM.SelectedStateChangedEventHandler;
			this.SelectedChanged += DefineMacroVM.SelectedStateChangedEventHandler;

			this.SetupTestInformationReq += this.TestInformationInputVM.SetupTestInfomation;
			this.SetupTestInformationReq += this.BufferSizeVM.SetupTestInfomation;
			this.SetupTestInformationReq += this.HeaderInformationVM.SetupTestInfomation;
			this.SetupTestInformationReq += this.LibraryInforamtionVM.SetupTestInfomation;
			this.SetupTestInformationReq += this.DefineMacroVM.SetupTestInfomation;

			this.SelectedConfigurationItemIndex = 0;
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
				this.RaisePropertyChanged(nameof(SelectedConfigurationItemIndex));

				var eventArgs = new SelectedStateChangedEventArgs(-1, this.SelectedConfigurationItemIndex);
				this.SelectedChanged?.Invoke(this, eventArgs);
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

		/// <summary>
		/// Property of view model of libraries.
		/// </summary>
		public LibraryInformationInputViewModel LibraryInforamtionVM
		{
			get
			{
				return this._LibraryInformationVM;
			}
			set
			{
				this._LibraryInformationVM = value;
				this.RaisePropertyChanged(nameof(LibraryInforamtionVM));
			}
		}

		/// <summary>
		/// Define macro input view model.
		/// </summary>
		public DefineMacroInputViewModel DefineMacroVM
		{
			get
			{
				return this._DefineMacroVM;
			}
			set
			{
				this._DefineMacroVM = value;
				this.RaisePropertyChanged(nameof(DefineMacroVM));
			}
		}

		public DelegateCommand RunCommand
		{
			get
			{
				if (null == this._RunCommand)
				{
					this._RunCommand = new DelegateCommand(this.RunCommandExecute, this.CanRunCommandExecute);
				}
				return this._RunCommand;
			}
		}

		public void RunCommandExecute()
		{
			var testDataInfo = new TestDataInfo();
			this.SetupTestInformationReq?.Invoke(ref testDataInfo);

			Debug.WriteLine("RunCommandExecute()");
			var command = new RunToolCommand();
			command.Run(testDataInfo);
		}

		public bool CanRunCommandExecute()
		{
			return true;
		}
	}
}