using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.EventArgs
{
	public class SelectedStateChangedEventArgs : System.EventArgs
	{
		public SelectedStateChangedEventArgs(int oldSelected, int newSelected)
		{
			this.OldSelectedIndex = oldSelected;
			this.NewSelectedIndex = newSelected;
		}

		public int OldSelectedIndex;

		public int NewSelectedIndex;
	}
}
