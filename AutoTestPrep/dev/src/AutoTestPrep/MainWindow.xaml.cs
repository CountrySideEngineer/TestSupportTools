using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AutoTestPrep.Model.EventArgs;
using AutoTestPrep.ViewModel;
using CSEngineer;

namespace AutoTestPrep
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Data context changed event handler.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">Argument of event.</param>
		private void MainWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.DataContext = e.NewValue;
			var viewModel = (NotificationViewModelBase)this.DataContext;
			viewModel.NotifyOkInformation += this.NotifyOkInformation;
			viewModel.NotifyErrorInformation += this.NotifyErrorInformation;
		}

		/// <summary>
		/// OK information notificatoin event handler
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="args">Event argument</param>
		protected void NotifyOkInformation(object sender, NotificationEventArgs args)
		{
			MessageBox.Show(args.Message, args.Tilte, MessageBoxButton.OK, MessageBoxImage.None);
		}

		/// <summary>
		/// Error information notification event handler.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="args">Event argument</param>
		protected void NotifyErrorInformation(object sender, NotificationEventArgs args)
		{
			MessageBox.Show(args.Message, args.Tilte, MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}
}
