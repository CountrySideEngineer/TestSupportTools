using AutoTestPrep.Model.EventArgs;
using CSEngineer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.ViewModel
{
	public class NotificationViewModelBase : ViewModelBase
	{
		/// <summary>
		/// Event handler to notify information of application.
		/// </summary>
		/// <param name="sender">Information sender</param>
		/// <param name="arg">Information data.</param>
		public delegate void NotifyInformationEventHandler(object sender, NotificationEventArgs arg);

		/// <summary>
		/// Notify OK information.
		/// </summary>
		public NotifyInformationEventHandler NotifyOkInformation;

		/// <summary>
		/// Notify NG information.
		/// </summary>
		public NotifyInformationEventHandler NotifyErrorInformation;
	}
}
