using AutoTestPrep.Model.InputInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.ViewModel
{
	public class DefineMacroInputViewModel : AutoTestPrepViewModelBase
	{
		/// <summary>
		/// Field of 
		/// </summary>
		protected MultiLineInputViewModel _DefineMacroVM;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public DefineMacroInputViewModel() : base(-1) { }

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="index">Index in list.</param>
		public DefineMacroInputViewModel(int index) : base(index)
		{
			this.DefineMacroVM = new MultiLineInputViewModel("マクロ：", string.Empty);
		}

		/// <summary>
		/// View model to input macro names.
		/// </summary>
		public MultiLineInputViewModel DefineMacroVM
		{
			get
			{
				return this._DefineMacroVM;
			}
			set
			{
				this._DefineMacroVM = value;
				this.RaisePropertyChanged(nameof(DefineMacroVM));
			}
		}

		/// <summary>
		/// Property of define macro input.
		/// </summary>
		public string DefineMacro
		{
			get
			{
				return this.DefineMacroVM.InputItem;
			}
			set
			{
				this.DefineMacroVM.InputItem = value;
			}
		}

		/// <summary>
		/// Returns collection of define macro input.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<string> DefineMacroInEnumerable()
		{
			return this.DefineMacroVM.InputItemInIenumerable();
		}

		public override void SetupTestInfomation(ref TestDataInfo testDataInfo)
		{
			testDataInfo.DefineMacros = this.DefineMacroInEnumerable();
		}
	}
}
