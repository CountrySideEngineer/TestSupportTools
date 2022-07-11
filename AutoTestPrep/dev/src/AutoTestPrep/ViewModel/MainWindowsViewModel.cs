using AutoTestPrep.Command;
using AutoTestPrep.Command.Argument;
using AutoTestPrep.Model.EventArgs;
using AutoTestPrep.Model.InputInfos;
using CStubMKGui.Command;
using System;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace AutoTestPrep.ViewModel
{
	using AutoTestPrep.Model.Converter;
	using CSEngineer.Logger;
	using CSEngineer.Logger.Interface;
	using Plugin;
	using StubDriverPlugin.Data;

	public class MainWindowsViewModel : NotificationViewModelBase
	{
		protected string _CurrentFilePath;

		protected string _CurrentTitle;

		protected ObservableCollection<string> _testConfigurationItems;
		protected int _selectedConfigurationItemIndex;

		protected ObservableCollection<PluginInfo> _defaultPlugins;

		protected ObservableCollection<PluginInfo> _customPlugins;

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

		protected TestFrameworkSelectViewModel _FrameworkSelectVM;

		protected bool _CustomPluginEnable;

		/// <summary>
		/// View model field of input define macro information.
		/// </summary>
		protected DefineMacroInputViewModel _DefineMacroVM;

		protected DelegateCommand _NewProjectCommand;

		protected DelegateCommand _SaveProjectCommand;

		protected DelegateCommand _LoadProjectCommand;

		protected DelegateCommand _OverWriteProjectCommand;

		protected DelegateCommand _ShutdownCommand;

		protected DelegateCommand _AboutCommand;

		protected DelegateCommand _RegisterPluginCommand;

		protected DelegateCommand<PluginInfo> _DefaultPluginCommand;

		protected DelegateCommand<PluginInfo> _CustomPluginCommand;

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

		public delegate void PluginRegisterShowRequetEventHandler(object sender, EventArgs e);
		public event PluginRegisterShowRequetEventHandler ShowPluginRegisterReq;


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
				"マクロ情報",
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
			this.FrameworkSelectVM = new TestFrameworkSelectViewModel(6);

			this.SelectedChanged += TestInformationInputVM.SelectedStateChangedEventHandler;
			this.SelectedChanged += BufferSizeVM.SelectedStateChangedEventHandler;
			this.SelectedChanged += DriverHeaderInformationVM.SelectedStateChangedEventHandler;
			this.SelectedChanged += StubHeaderInformationVM.SelectedStateChangedEventHandler;
			this.SelectedChanged += LibraryInforamtionVM.SelectedStateChangedEventHandler;
			this.SelectedChanged += DefineMacroVM.SelectedStateChangedEventHandler;
			this.SelectedChanged += FrameworkSelectVM.SelectedStateChangedEventHandler;

			this.SetupTestInformationReq += this.TestInformationInputVM.SetupTestInfomation;
			this.SetupTestInformationReq += this.BufferSizeVM.SetupTestInfomation;
			this.SetupTestInformationReq += this.DriverHeaderInformationVM.SetupTestInfomation;
			this.SetupTestInformationReq += this.StubHeaderInformationVM.SetupTestInfomation;
			this.SetupTestInformationReq += this.LibraryInforamtionVM.SetupTestInfomation;
			this.SetupTestInformationReq += this.DefineMacroVM.SetupTestInfomation;
			this.SetupTestInformationReq += this.FrameworkSelectVM.SetupTestInfomation;

			this.RestoreInformationReq += this.TestInformationInputVM.RestoreTestInforamtion;
			this.RestoreInformationReq += this.BufferSizeVM.RestoreTestInforamtion;
			this.RestoreInformationReq += this.DriverHeaderInformationVM.RestoreTestInforamtion;
			this.RestoreInformationReq += this.StubHeaderInformationVM.RestoreTestInforamtion;
			this.RestoreInformationReq += this.LibraryInforamtionVM.RestoreTestInforamtion;
			this.RestoreInformationReq += this.DefineMacroVM.RestoreTestInforamtion;
			this.RestoreInformationReq += this.FrameworkSelectVM.RestoreTestInforamtion;

			this.SelectedConfigurationItemIndex = 0;

			this.BaseTestDataInfo = new TestDataInfo();
			this.SetupTestInformationReq(ref this.BaseTestDataInfo);

			this.LoadPlugins();
			this.UpdateCustomPluginEnable();
			this.SetUpLogger();
		}

		public string CurrentTitle
		{
			get => this._CurrentTitle;
			set
			{
				this._CurrentTitle = value;
				this.RaisePropertyChanged(nameof(CurrentTitle));
			}
		}

		public string CurrentFilePath
		{
			get => this._CurrentFilePath;
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
			get => this._testConfigurationItems;
			protected set
			{
				this._testConfigurationItems = value;
				this.RaisePropertyChanged(nameof(TestConfigurationItems));
			}
		}

		public ObservableCollection<PluginInfo> DefaultPlugins
		{
			get => this._defaultPlugins;
			protected set
			{
				this._defaultPlugins = value;
				this.RaisePropertyChanged(nameof(DefaultPlugins));
			}
		}

		public ObservableCollection<PluginInfo> CustomPlugins
		{
			get => this._customPlugins;
			set
			{
				this._customPlugins = value;
				this.RaisePropertyChanged(nameof(CustomPlugins));
			}
		}

		public int SelectedConfigurationItemIndex
		{
			get => this._selectedConfigurationItemIndex;
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
			get => this._TestInformationInputVM;
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
			get => this._BufferSizeVM;
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
			get => this._DriverHeaderInformationVM;
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
			=> this._StubHeaderInformationVM;
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
			get => this._LibraryInformationVM;
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
			get => this._DefineMacroVM;
			set
			{
				this._DefineMacroVM = value;
				this.RaisePropertyChanged(nameof(DefineMacroVM));
			}
		}

		/// <summary>
		/// Test framework select view model.
		/// </summary>
		public TestFrameworkSelectViewModel FrameworkSelectVM
		{
			get => this._FrameworkSelectVM;
			set
			{
				this._FrameworkSelectVM = value;
				this.RaisePropertyChanged(nameof(this.FrameworkSelectVM));
			}
		}

		public bool CustomPluginEnable
		{
			get => this._CustomPluginEnable;
			set
			{
				this._CustomPluginEnable = value;
				this.RaisePropertyChanged(nameof(CustomPluginEnable));
			}
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

		/// <summary>
		/// Command to show window to register plugin command.
		/// </summary>
		public DelegateCommand RegisterPluginCommand
		{
			get
			{
				if (null == this._RegisterPluginCommand)
				{
					this._RegisterPluginCommand = new DelegateCommand(
						this.PluginRegisterCommandExecute, 
						this.CanPluginRegisterCommandExecute);
				}
				return this._RegisterPluginCommand;
			}
		}

		/// <summary>
		/// Body of command to register plugin.
		/// </summary>
		public void PluginRegisterCommandExecute()
		{
			this.ShowPluginRegisterReq?.Invoke(this, new EventArgs());
		}

		/// <summary>
		/// Returns whether the command to register plugin can execute or not.
		/// </summary>
		/// <returns></returns>
		public bool CanPluginRegisterCommandExecute()
		{
			return true;
		}

		/// <summary>
		/// Load default and user custom plugin information.
		/// </summary>
		protected void LoadPlugins()
		{
			LoadPluginCommand loadDefaultCommand = new LoadDefaultPluginCommand();
			this.DefaultPlugins = this.LoadPlugins(loadDefaultCommand);
			LoadPluginCommand loadCustomPluginCommand = new LoadCustomPluginCommand();
			this.CustomPlugins = this.LoadPlugins(loadCustomPluginCommand);
		}

		/// <summary>
		/// Update whetther the custom plugins can execute or not.
		/// </summary>
		protected void UpdateCustomPluginEnable()
		{
			if (0 < this.CustomPlugins.Count)
			{
				this.CustomPluginEnable = true;
			}
			else
			{
				this.CustomPluginEnable = false;
			}
		}

		protected void SetUpLogger()
		{
			var logger = Log.GetInstance();
			ILogEvent consoleLog = new CSEngineer.Logger.Console.Log();
			ILogEvent fileLog = new CSEngineer.Logger.File.Log();

			SetUpLogger(ref consoleLog);
			SetUpLogger(ref fileLog);
		}

		protected void SetUpLogger(ref ILogEvent logEvent)
		{
			var logger = Log.GetInstance();
			logger.TraceLogEventHandler += logEvent.TRACE;
			logger.DebugLogEventHandler += logEvent.DEBUG;
			logger.InfoLogEventHandler += logEvent.INFO;
			logger.WarnLogEventHandler += logEvent.WARN;
			logger.ErrorLogEventHandler += logEvent.ERROR;
			logger.FatalLogEventHandler += logEvent.FATAL;
		}

		/// <summary>
		/// Load plugin using IPluginCommand, and its concrete object.
		/// </summary>
		/// <param name="loadPluginCommnad">Command to load plugin information.</param>
		/// <returns>Collection of plugin information.</returns>
		protected ObservableCollection<PluginInfo> LoadPlugins(IPluginCommand loadPluginCommnad)
		{
			var pluginInfos = new ObservableCollection<PluginInfo>();
			loadPluginCommnad.Execute(pluginInfos);

			return pluginInfos;
		}

		/// <summary>
		/// Delegate command property to execute plugin command.
		/// </summary>
		public DelegateCommand<PluginInfo> DefaultPluginCommand
		{
			get
			{
				if (null == this._DefaultPluginCommand)
				{
					this._DefaultPluginCommand = new DelegateCommand<PluginInfo>(
						this.DefaultPluginCommandExecute, this.CanDefaultPluginCommandExecute);
				}
				return this._DefaultPluginCommand;
			}
		}

		/// <summary>
		/// Body of command to execute command about menu item selected.
		/// </summary>
		/// <param name="pluginInfo">Plugin information selected.</param>
		public void DefaultPluginCommandExecute(PluginInfo pluginInfo)
		{
			var testDataInfo = new TestDataInfo();
			this.SetupTestInformationReq?.Invoke(ref testDataInfo);

			var converter = new TestDataInfoConverter();
			PluginInput pluginInput = converter.ToPluginInput(testDataInfo);

			var command = new ExecDefaultPluginCommand();
			var commandArg = new PluginCommandArgument(pluginInfo, pluginInput);
			this.PluginCommandExecute(command, commandArg);
		}

		/// <summary>
		/// Returns whether the command of menu item can execute or not.
		/// </summary>
		/// <param name="data">Command data.</param>
		/// <returns>Returns true if the command can execute, otherwise false.</returns>
		public bool CanDefaultPluginCommandExecute(object data)
		{
			return true;
		}

		public DelegateCommand<PluginInfo> CustomPluginCommand
		{
			get
			{
				if (null == this._CustomPluginCommand)
				{
					this._CustomPluginCommand = new DelegateCommand<PluginInfo>(
						this.CustomPluginCommandExecute, this.CanCustomPluginCommandExecute);
				}
				return this._CustomPluginCommand;
			}
		}

		/// <summary>
		/// Execute user custom plugin command.
		/// </summary>
		/// <param name="pluginInfo"></param>
		public void CustomPluginCommandExecute(PluginInfo pluginInfo)
		{
			var testDataInfo = new TestDataInfo();
			this.SetupTestInformationReq?.Invoke(ref testDataInfo);

			var converter = new TestDataInfoConverter();
			PluginInput pluginInput = converter.ToPluginInput(testDataInfo);

			var command = new ExecCustomPluginCommand();
			var commandArg = new PluginCommandArgument(pluginInfo, pluginInput);
			this.PluginCommandExecute(command, commandArg);
		}

		/// <summary>
		/// Return whether the command of menu item about user custom plugin can execute or not.
		/// </summary>
		/// <param name="data">Command data</param>
		/// <returns>Returns ture if the command can execute, otherwise returns false.</returns>
		public bool CanCustomPluginCommandExecute(object data)
		{
			return this.CustomPluginEnable;
		}

		protected void PluginCommandExecute(ExecPluginCommand command, PluginCommandArgument commandArg)
		{
			try
			{
				command.Execute(commandArg);

				var message = new NotificationEventArgs()
				{
					Title = "プラグイン実行結果 - " + commandArg.PluginOutput.Title,
					Message = commandArg.PluginOutput.Message
				};
				this.NotifyOkInformation?.Invoke(this, message);
			}
			catch (System.Exception ex)
			when ((ex is ArgumentException) || (ex is ArgumentNullException) || (ex is NullReferenceException))
			{
				Debug.WriteLine(ex.StackTrace);

				var message = new NotificationEventArgs()
				{
					Title = "エラー",
					Message = "コマンド実行エラー"
				};
				this.NotifyErrorInformation.Invoke(this, message);
			}
		}
	}
}
