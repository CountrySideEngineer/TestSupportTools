using CSEngineer.ViewModel;
using CStubMKGui.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.ViewModel
{
	public class HelpViewModel : ViewModelBase
	{
		/// <summary>
		/// Field of command to close window.
		/// </summary>
		protected DelegateCommand _RequestCloseWindowCommand;

		/// <summary>
		/// Event handler to request close window.
		/// </summary>
		/// <param name="sender">Request sender</param>
		/// <param name="e">Event argument.</param>
		public delegate void CloseWindowRequestEventHandler(object sender, EventArgs e);

		/// <summary>
		/// Request to close window.
		/// </summary>
		public CloseWindowRequestEventHandler CloseWindowRequest;

		/// <summary>
		/// Title of window.
		/// </summary>
		public string Title
		{
			get
			{
				var assmebly = Assembly.GetExecutingAssembly();
				var fileVersionInfo = FileVersionInfo.GetVersionInfo(assmebly.Location);
				return $"{fileVersionInfo.FileDescription}のバージョン情報";
			}
		}

		/// <summary>
		/// Command to request to close window.
		/// </summary>
		public DelegateCommand RequestCloseWindowsCommand
		{
			get
			{
				if (null == this._RequestCloseWindowCommand)
				{
					this._RequestCloseWindowCommand = new DelegateCommand(
						this.RequestCloseCommandExecute, this.CanRequestCloseCommandExecute);
				}
				return this._RequestCloseWindowCommand;
			}
		}

		/// <summary>
		/// Execute command to close window.
		/// </summary>
		public void RequestCloseCommandExecute()
		{
			this.CloseWindowRequest?.Invoke(this, new EventArgs());
		}

		/// <summary>
		/// Returns whether the command can execute.
		/// </summary>
		/// <returns></returns>
		public bool CanRequestCloseCommandExecute()
		{
			return true;
		}
	}
}
