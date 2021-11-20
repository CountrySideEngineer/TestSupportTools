using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.EventArgs
{
	public class NotificationEventArgs : System.EventArgs
	{
		/// <summary>
		/// Tilte of notification.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Message of notification.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NotificationEventArgs()
		{
			this.Title = string.Empty;
			this.Message = string.Empty;
		}

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="tilte">Title of notification.</param>
		/// <param name="message">Message of notification.</param>
		public NotificationEventArgs(string tilte, string message)
		{
			this.Title = tilte;
			this.Message = message;
		}
	}
}
