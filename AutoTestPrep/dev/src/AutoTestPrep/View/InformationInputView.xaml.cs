using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace AutoTestPrep.View
{
	/// <summary>
	/// InformationInputView.xaml の相互作用ロジック
	/// </summary>
	public partial class InformationInputView : UserControl
	{
		public InformationInputView()
		{
			InitializeComponent();
		}

		private void TextBox_PreviewDragOver(object sender, DragEventArgs e)
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

		private void TextBox_Drop(object sender, DragEventArgs e)
		{
			string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
			if (null != files)
			{
				this.InputTextBox.Text = files[0];
			}
		}
	}
}
