using AutoTestPrep.Command.Argument;
using AutoTestPrep.ViewModel;
using CStubMKGui.Command;
using Plugin;
using PluginRegister.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginRegister.ViewModel
{
	public class PluginRegisterViewModel : NotificationViewModelBase
	{
		/// <summary>
		/// Field of plugin name input area title.
		/// </summary>
		protected string _pluginNameInputAreaTitle;

		/// <summary>
		/// Field of plugin path input area title.
		/// </summary>
		protected string _pluginPathInputAreaTitle;

		/// <summary>
		/// Field of plugin name.
		/// </summary>
		protected string _pluginName;

		/// <summary>
		/// Field of plugin path.
		/// </summary>
		protected string _pluginPath;

		protected bool _canRegistPlugin;

		/// <summary>
		/// Command field to regist plugin information
		/// </summary>
		protected DelegateCommand _registPluginCommand;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public PluginRegisterViewModel()
		{
			this.PluginNameInputAreaTitle = "名前：";
			this.PluginPathInputAreaTitle = "パス：";
			this.PluginName = string.Empty;
			this.PluginPath = string.Empty;
		}

		/// <summary>
		/// Plugin name input area title.
		/// </summary>
		public string PluginNameInputAreaTitle
		{
			get
			{
				return this._pluginNameInputAreaTitle;
			}
			set
			{
				this._pluginNameInputAreaTitle = value;
				this.RaisePropertyChanged(nameof(PluginNameInputAreaTitle));
			}
		}

		/// <summary>
		/// Plugin paht input area title.
		/// </summary>
		public string PluginPathInputAreaTitle
		{
			get
			{
				return this._pluginPathInputAreaTitle;
			}
			set
			{
				this._pluginPathInputAreaTitle = value;
				this.RaisePropertyChanged(nameof(PluginPathInputAreaTitle));
			}
		}

		/// <summary>
		/// Property of plugin name.
		/// </summary>
		public string PluginName
		{
			get
			{
				return this._pluginName;
			}
			set
			{
				this._pluginName = value;
				this.RaisePropertyChanged(nameof(PluginName));
			}
		}

		/// <summary>
		/// Property of plugin file path.
		/// </summary>
		public string PluginPath
		{
			get
			{
				return this._pluginPath;
			}
			set
			{
				this._pluginPath = value;
				this.RaisePropertyChanged(nameof(PluginPath));
			}
		}

		public bool CanRegistPlugin
		{
			get
			{
				if (((string.IsNullOrEmpty(this.PluginName)) || (string.IsNullOrWhiteSpace(this.PluginName))) || 
					((string.IsNullOrEmpty(this.PluginPath)) || (string.IsNullOrWhiteSpace(this.PluginPath))))
				{
					this._canRegistPlugin = false;
				}
				else
				{
					this._canRegistPlugin = true;
				}
				return this._canRegistPlugin;
			}
			set
			{
				this._canRegistPlugin = value;
				this.RaisePropertyChanged(nameof(CanRegistPlugin));
			}
		}

		/// <summary>
		/// Command to regist plugin command execution.
		/// </summary>
		public DelegateCommand RegistPluginCommand
		{
			get
			{
				if (null == this._registPluginCommand)
				{
					this._registPluginCommand = new DelegateCommand(
						this.RegistPluginCommandExecute, 
						this.CanRegistPluginCommandExecute);
				}
				return this._registPluginCommand;
			}
		}

		/// <summary>
		/// Returns whether the command to regist 
		/// </summary>
		/// <returns></returns>
		public bool CanRegistPluginCommandExecute()
		{
			return this.CanRegistPlugin;
		}

		/// <summary>
		/// Execute command to register plugin information.
		/// </summary>
		public void RegistPluginCommandExecute()
		{
			try
			{
				var pluginInfo = new PluginInfo()
				{
					Name = this.PluginName,
					FileName = this.PluginPath
				};
				var commandArg = new PluginCommandArgument(pluginInfo, null);
				var command = new RegistPluginCommand();
				command.Execute(commandArg);

				this.NotifyOkInformation?.Invoke(this, null);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.StackTrace);

				throw;
			}
		}
	}
}
