using AutoTestPrep.Command;
using AutoTestPrep.Command.Argument;
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
	public class MainWindowsViewModel : NotificationViewModelBase
	{
		protected string _CurrentFilePath;

		protected string _CurrentTitle;

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
		protected HeaderInformationInputViewModel _DriverHeaderInformationVM;

		/// <summary>
		/// View model field to input header file information.
		/// </summary>
		protected HeaderInformationInputViewModel _StubHeaderInformationVM;

		/// <summary>
		/// View model field of input library file information.
		/// </summary>
		protected LibraryInformationInputViewModel _LibraryInformationVM;

		/// <summary>
		/// View model field of input define macro information.
		/// </summary>
		protected DefineMacroInputViewModel _DefineMacroVM;

		protected DelegateCommand _RunCommand;

		protected DelegateCommand _NewProjectCommand;

		protected DelegateCommand _SaveProjectCommand;

		protected DelegateCommand _LoadProjectCommand;

		protected DelegateCommand _OverWriteProjectCommand;

		protected DelegateCommand _ShutdownCommand;

		protected DelegateCommand _AboutCommand;

		protected TestDataInfo BaseTestDataInfo;

		/// <summary>
		/// Event handler to handle a event 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public delegate void SelectedChangedEventHandler(object sender, SelectedStateChangedEventArgs e);
		public event SelectedChangedEventHandler SelectedChanged;

		public delegate void SetupTestInformationRequest(ref TestDataInfo testDataInf);
		public event SetupTestInformationRequest SetupTestInformationReq;

		public delegate void RestoreTestInformationRequest(TestDataInfo testDataInfo);
		public event RestoreTestInformationRequest RestoreInformationReq;

		public delegate void RequestAboutDialogShowRequestEventHandler(object sender, EventArgs e);
		public event RequestAboutDialogShowRequestEventHandler ShowAboutReq;

		/// <summary>
		/// Default consttuctor
		/// </summary>
		public MainWindowsViewModel()
		{
			this.TestConfigurationItems = new ObservableCollection<string>
			{
				"テスト情報",
				"スタブ情報",
				"ヘッダ情報(ドライバ)",
				"ヘッダ情報(スタブ)",
				"ライブラリ情報",
				"マクロ情報"
			};
			this.CurrentFilePath = string.Empty;
			this.CurrentTitle = "AutoTestPrep";

			this.TestInformationInputVM = new TestInformationInputViewModel(0);
			this.BufferSizeVM = new BufferSizeViewModel(1);
			this.DriverHeaderInformationVM = new DriverHeaderInformationInputViewModel(2)
			{
				IsStandartHeaderVisible = true,
				IsUserHeaderVivible = true,
				IsIncludeDirectoryVisible = true
			};
			this.StubHeaderInformationVM = new StubHeaderInformationInputViewModel(3)
			{
				IsStandartHeaderVisible = true,
				IsUserHeaderVivible = true,
				IsIncludeDirectoryVisible = false
			};
			this.LibraryInforamtionVM = new LibraryInformationInputViewModel(4);
			this.DefineMacroVM = new DefineMacroInputViewModel(5);

			this.SelectedChanged += TestInformationInputVM.SelectedStateChangedEventHandler;
			this.SelectedChanged += BufferSizeVM.SelectedStateChangedEventHandler;
			this.SelectedChanged += DriverHeaderInformationVM.SelectedStateChangedEventHandler;
			this.SelectedChanged += StubHeaderInformationVM.SelectedStateChangedEventHandler;
			this.SelectedChanged += LibraryInforamtionVM.SelectedStateChangedEventHandler;
			this.SelectedChanged += DefineMacroVM.SelectedStateChangedEventHandler;

			this.SetupTestInformationReq += this.TestInformationInputVM.SetupTestInfomation;
			this.SetupTestInformationReq += this.BufferSizeVM.SetupTestInfomation;
			this.SetupTestInformationReq += this.DriverHeaderInformationVM.SetupTestInfomation;
			this.SetupTestInformationReq += this.StubHeaderInformationVM.SetupTestInfomation;
			this.SetupTestInformationReq += this.LibraryInforamtionVM.SetupTestInfomation;
			this.SetupTestInformationReq += this.DefineMacroVM.SetupTestInfomation;

			this.RestoreInformationReq += this.TestInformationInputVM.RestoreTestInforamtion;
			this.RestoreInformationReq += this.BufferSizeVM.RestoreTestInforamtion;
			this.RestoreInformationReq += this.DriverHeaderInformationVM.RestoreTestInforamtion;
			this.RestoreInformationReq += this.StubHeaderInformationVM.RestoreTestInforamtion;
			this.RestoreInformationReq += this.LibraryInforamtionVM.RestoreTestInforamtion;
			this.RestoreInformationReq += this.DefineMacroVM.RestoreTestInforamtion;

			this.SelectedConfigurationItemIndex = 0;

			this.BaseTestDataInfo = new TestDataInfo();
			this.SetupTestInformationReq(ref this.BaseTestDataInfo);
		}

		public string CurrentTitle
		{
			get
			{
				return this._CurrentTitle;
			}
			set
			{
				this._CurrentTitle = value;
				this.RaisePropertyChanged(nameof(CurrentTitle));
			}
		}

		public string CurrentFilePath
		{
			get
			{
				return this._CurrentFilePath;
			}
			set
			{
				this._CurrentFilePath = value;
				if ((!string.IsNullOrEmpty(this._CurrentFilePath)) &&
					(!string.IsNullOrWhiteSpace(this._CurrentFilePath)))
				{
					this.CurrentTitle = $"AutoTestPrep : {this._CurrentFilePath}";
				}
				else
				{
					this.CurrentTitle = "AutoTestPrep";
				}
			}
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
		/// Property of view model of header files test driver includes.
		/// </summary>
		public HeaderInformationInputViewModel DriverHeaderInformationVM
		{
			get
			{
				return this._DriverHeaderInformationVM;
			}
			set
			{
				this._DriverHeaderInformationVM = value;
				this.RaisePropertyChanged(nameof(DriverHeaderInformationVM));
			}
		}

		/// <summary>
		/// Property of view model of header files stub file includes.
		/// </summary>
		public HeaderInformationInputViewModel  StubHeaderInformationVM
		{
			get
			{
				return this._StubHeaderInformationVM;
			}
			set
			{
				this._StubHeaderInformationVM = value;
				this.RaisePropertyChanged(nameof(StubHeaderInformationVM));
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
			try
			{
				var testDataInfo = new TestDataInfo();
				this.SetupTestInformationReq?.Invoke(ref testDataInfo);

				Debug.WriteLine("RunCommandExecute()");
				var command = new RunToolCommand();
				command.Execute(testDataInfo);

				var eventArg = new NotificationEventArgs()
				{
					Tilte = "完了",
					Message = "テストデータの解析とコード生成が完了しました。"
				};
				this.NotifyOkInformation?.Invoke(this, eventArg);
			}
			catch (Exception)
			{
				var errArg = new NotificationEventArgs()
				{
					Tilte = "完了(エラーあり)",
					Message = "エラー発生\n" +
						"詳細はログを確認してください。"
				};
				this.NotifyErrorInformation?.Invoke(this, errArg);
			}
		}

		public bool CanRunCommandExecute()
		{
			return true;
		}

		public DelegateCommand NewProjectCommand
		{
			get
			{
				if (null == this._NewProjectCommand)
				{
					this._NewProjectCommand = new DelegateCommand(
						this.NewProjectCommandExecute, this.CanNewProjectCommandExecute);
				}
				return this._NewProjectCommand;
			}
		}

		/// <summary>
		/// Reset project by new project.
		/// </summary>
		public void NewProjectCommandExecute()
		{
			var testData = new TestDataInfo();
			this.SetupTestInformationReq?.Invoke(ref testData);
			var commandArg = new ProjectCommandArgument()
			{
				BaseData = this.BaseTestDataInfo,
				LatestData = testData
			};
			var command = new NewProjectCommand();
			command.Execute(commandArg);

			this.CurrentFilePath = string.Empty;
			this.BaseTestDataInfo = testData;
			this.RestoreInformationReq?.Invoke(testData);
		}

		public bool CanNewProjectCommandExecute() { return true; }

		public DelegateCommand SaveProjectCommand
		{
			get
			{
				if (null == this._SaveProjectCommand)
				{
					this._SaveProjectCommand = 
						new DelegateCommand(
							this.SaveProjectCommandExecute, this.CanSaveProjectCommand);
				}
				return this._SaveProjectCommand;
			}
		}

		public bool CanSaveProjectCommand() { return true; }

		/// <summary>
		/// Save project.
		/// </summary>
		public void SaveProjectCommandExecute()
		{
			var testDataInfo = new TestDataInfo();
			this.SetupTestInformationReq?.Invoke(ref testDataInfo);

			var commandArg = new ProjectCommandArgument()
			{
				FilePath = this.CurrentFilePath,
				LatestData = testDataInfo
			};
			var command = new SaveProjectCommand();
			command.Execute(commandArg);

			this.CurrentFilePath = commandArg.FilePath;
			this.BaseTestDataInfo = new TestDataInfo(commandArg.LatestData);
		}

		public DelegateCommand LoadProjectCommand
		{
			get
			{
				if (null == _LoadProjectCommand)
				{
					this._LoadProjectCommand =
						new DelegateCommand(
							this.LoadProjectCommandExecute, this.CanLoadProjectCommandExecute);
				}
				return this._LoadProjectCommand;
			}
		}

		/// <summary>
		/// Load project file.
		/// </summary>
		public void LoadProjectCommandExecute()
		{
			var latestTestDataInfo = new TestDataInfo();
			this.SetupTestInformationReq?.Invoke(ref latestTestDataInfo);
			var commandArg = new ProjectCommandArgument()
			{
				BaseData = this.BaseTestDataInfo,
				LatestData = latestTestDataInfo
			};
			var command = new LoadProjectCommand();
			command.Execute(commandArg);

			this.CurrentFilePath = commandArg.FilePath;
			this.RestoreInformationReq?.Invoke(commandArg.LatestData);
			this.BaseTestDataInfo = new TestDataInfo(commandArg.LatestData);
		}

		public bool CanLoadProjectCommandExecute() { return true; }

		public DelegateCommand OverWriteProjectCommand
		{
			get
			{
				if (null == this._OverWriteProjectCommand)
				{
					this._OverWriteProjectCommand = new DelegateCommand(
						this.OverWriteProjectCommandExecute, this.CanOverWriteProjectCommandExecute);
				}
				return this._OverWriteProjectCommand;
			}
		}

		/// <summary>
		/// Save (over write) current project.
		/// </summary>
		public void OverWriteProjectCommandExecute()
		{
			var testDataInfo = new TestDataInfo();
			this.SetupTestInformationReq?.Invoke(ref testDataInfo);

			var commandArg = new ProjectCommandArgument()
			{
				FilePath = this.CurrentFilePath,
				LatestData = testDataInfo
			};
			var command = new OverWriteProjectCommand();
			command.Execute(commandArg);

			this.CurrentFilePath = commandArg.FilePath;
			this.BaseTestDataInfo = new TestDataInfo(testDataInfo);
		}
		
		public bool CanOverWriteProjectCommandExecute()
		{
			return (!((string.IsNullOrEmpty(this.CurrentFilePath)) || (string.IsNullOrWhiteSpace(this.CurrentFilePath))));
		}

		public DelegateCommand ShutdownCommand
		{
			get
			{
				if (null == this._ShutdownCommand)
				{
					this._ShutdownCommand = new DelegateCommand(
						this.ShutdownCommandExecute, this.CanShutdownCommandExecute);
				}
				return this._ShutdownCommand;
			}
		}

		public void ShutdownCommandExecute()
		{
			var testDataInfo = new TestDataInfo();
			this.SetupTestInformationReq?.Invoke(ref testDataInfo);

			var commandArg = new ProjectCommandArgument
			{
				FilePath = this.CurrentFilePath,
				BaseData = this.BaseTestDataInfo,
				LatestData = testDataInfo
			};
			var command = new ShutdownCommand();
			command.Execute(commandArg);
		}

		public bool CanShutdownCommandExecute() { return true; }

		public DelegateCommand AboutCommand
		{
			get
			{
				if (null == this._AboutCommand)
				{
					this._AboutCommand = new DelegateCommand(
						this.AboutCommandExecute, this.CanAboutCommandExecute);
				}
				return this._AboutCommand;
			}
		}

		public void AboutCommandExecute()
		{
			this.ShowAboutReq?.Invoke(this, new EventArgs());
		}

		public bool CanAboutCommandExecute()
		{
			return true;
		}
	}
}
