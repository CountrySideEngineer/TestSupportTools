using AutoTestPrep.ViewModel;
using System;
using System.Collections.Generic;
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

	}
}
