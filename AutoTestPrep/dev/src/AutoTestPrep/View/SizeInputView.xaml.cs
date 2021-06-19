using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
	/// SizeInputView.xaml の相互作用ロジック
	/// </summary>
	public partial class SizeInputView : UserControl
	{
		public SizeInputView()
		{
			InitializeComponent();
		}

		private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			var regex = new Regex("[0-9]");
			e.Handled = !regex.IsMatch(e.Text);
		}

		private void TextBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			if (e.Command == ApplicationCommands.Paste)
			{
				e.Handled = true;
			}

		}
	}
}
