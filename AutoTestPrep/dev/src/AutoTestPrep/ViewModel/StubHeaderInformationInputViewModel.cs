using AutoTestPrep.Model.InputInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.ViewModel
{
	public class StubHeaderInformationInputViewModel : HeaderInformationInputViewModel
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public StubHeaderInformationInputViewModel() : base() { }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="index">Index of view model.</param>
		public StubHeaderInformationInputViewModel(int index) : base(index) { }

		/// <summary>
		/// Set input header file names to property "StubIncludeXxxxFiles" property of 
		/// TestDataInfo object.
		/// </summary>
		/// <param name="testDataInfo">Object to set header file names.</param>
		public override void SetupTestInfomation(ref TestDataInfo testDataInfo)
		{
			testDataInfo.StubIncludeStandardHeaderFiles = this.ToEnumrable(this.StandartHeader);
			testDataInfo.StubIncludeUserHeaderFiles = this.ToEnumrable(this.UserHeader);
		}
	}
}
