using AutoTestPrep.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace AutoTestPrep.View
{
	/// <summary>
	/// HelpWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class HelpWindow : Window
	{
		public HelpWindow()
		{
			InitializeComponent();
		}

		private void HelpWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			try
			{
				this.DataContext = e.NewValue;
				var viewModel = (HelpViewModel)this.DataContext;
				viewModel.CloseWindowRequest += this.CloseWindowsRequest;
			}
			catch (InvalidCastException)
			{
				//Ignore this exception.
			}
			catch (Exception)
			{
				//Ignore this exception.
			}
		}

		/// <summary>
		/// Close this window.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event argument.</param>
		protected void CloseWindowsRequest(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
