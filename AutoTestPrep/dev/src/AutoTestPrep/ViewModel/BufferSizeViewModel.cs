using CSEngineer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.ViewModel
{
	public class BufferSizeViewModel : ViewModelBase
	{
		/// <summary>
		/// Field of view model of buffer size 1.
		/// </summary>
		protected SizeInputViewModel _BufferSize1VM;

		/// <summary>
		/// Field of view model of buffer size 2.
		/// </summary>
		protected SizeInputViewModel _BufferSize2VM;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public BufferSizeViewModel()
		{
			this.BufferSize1VM = new SizeInputViewModel("バッファサイズ1", 0);
			this.BufferSize2VM = new SizeInputViewModel("バッファサイズ2", 0);
		}

		/// <summary>
		/// Propety of view model of buffer size 1.
		/// </summary>
		public SizeInputViewModel BufferSize1VM
		{
			get
			{
				return this._BufferSize1VM;
			}
			set
			{
				this._BufferSize1VM = value;
				this.RaisePropertyChanged(nameof(BufferSize1VM));
			}
		}

		/// <summary>
		/// Property of view model of bueer size 2.
		/// </summary>
		public SizeInputViewModel BufferSize2VM
		{
			get
			{
				return this._BufferSize2VM;
			}
			set
			{
				this._BufferSize2VM = value;
				this.RaisePropertyChanged(nameof(BufferSize2VM));
			}
		}

		/// <summary>
		/// Value of buffer size 1.
		/// </summary>
		public long BufferSize1
		{
			get
			{
				return this.BufferSize1VM.ItemValue;
			}
			set
			{
				this.BufferSize1VM.ItemValue = value;
			}
		}

		/// <summary>
		/// Value of buffer size 2.
		/// </summary>
		public long BufferSize2
		{
			get
			{
				return this.BufferSize2VM.ItemValue;
			}
			set
			{
				this.BufferSize2VM.ItemValue = value;
			}
		}
	}
}
