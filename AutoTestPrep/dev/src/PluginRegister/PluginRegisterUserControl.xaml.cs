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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PluginRegister
{
    /// <summary>
    /// UserControl1.xaml の相互作用ロジック
    /// </summary>
    public partial class PluginRegisterUserControl : UserControl
    {
        public PluginRegisterUserControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Data context changed event handler.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event argument.</param>
		private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
            var viewModel = (NotificationViewModelBase)e.NewValue;
            viewModel.NotifyOkInformation = this.NotifyOkInformationEventHandler;
		}

        /// <summary>
        /// Event handler notifying "OK" event.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="args">Event argument.</param>
        public void NotifyOkInformationEventHandler(object sender, EventArgs args)
		{
            MessageBox.Show("OK", "messageBox", MessageBoxButton.OK);
		}

		/// <summary>
		/// Event handler of preview drag over event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PluginPathInput_PreviewDragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
			{
				e.Effects = DragDropEffects.Copy;
			}
			else
			{
				e.Effects = DragDropEffects.None;
			}
			e.Handled = true;
		}

		/// <summary>
		/// Preview drop event handler.
		/// </summary>
		/// <param name="sender">Event sender object.</param>
		/// <param name="e">Event argument.</param>
		private void PluginPathInput_PreviewDrop(object sender, DragEventArgs e)
		{
			string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
			if (null != files)
			{
				this.PluginPathInput.Text = files[0];
			}
		}
	}
}
