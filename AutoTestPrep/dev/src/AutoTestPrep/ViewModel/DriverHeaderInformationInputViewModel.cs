using AutoTestPrep.Model.InputInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.ViewModel
{
	public class DriverHeaderInformationInputViewModel : HeaderInformationInputViewModel
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public DriverHeaderInformationInputViewModel() : base() { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="index">Index of view model.</param>
		public DriverHeaderInformationInputViewModel(int index) : base(index) { }

		/// <summary>
		/// Set input header file names to property "DriverIncludeXxxxFiles" property of 
		/// TestDataInfo object.
		/// </summary>
		/// <param name="testDataInfo">Object to set header file names.</param>
		public override void SetupTestInfomation(ref TestDataInfo testDataInfo)
		{
			testDataInfo.DriverIncludeStandardHeaderFiles = this.ToEnumrable(this.StandartHeader);
			testDataInfo.DriverIncludeUserHeaderFiles = this.ToEnumrable(this.UserHeader);
			testDataInfo.IncludeDirectoryPath = this.ToEnumrable(this.IncludeDirectory);
		}
	}
}
