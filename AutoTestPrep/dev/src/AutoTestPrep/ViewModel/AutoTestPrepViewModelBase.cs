using AutoTestPrep.Model.EventArgs;
using CSEngineer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.ViewModel
{
	public class AutoTestPrepViewModelBase : ViewModelBase
	{
		public AutoTestPrepViewModelBase(int viewModelIndex)
		{
			this.ViewModelIndex = viewModelIndex;
			this.IsSelected = true;
		}

		public int ViewModelIndex { get; protected set; }

		public void SelectedStateChangedEventHandler(object sender, EventArgs e)
		{
			var selectedEventArgs = (SelectedStateChangedEventArgs)e;
			if (this.ViewModelIndex == selectedEventArgs.NewSelectedIndex)
			{
				this.IsSelected = true;
			}
			else
			{
				this.IsSelected = false;
			}
		}

		protected bool _IsSelected;
		public bool IsSelected
		{
			get
			{
				return this._IsSelected;
			}
			set
			{
				this._IsSelected = value;
				this.RaisePropertyChanged(nameof(IsSelected));
			}
		}
	}
}
