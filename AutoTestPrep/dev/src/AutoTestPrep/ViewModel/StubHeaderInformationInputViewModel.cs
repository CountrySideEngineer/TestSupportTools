﻿using AutoTestPrep.Model.InputInfos;
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
			IEnumerable<string> stdHeaderFiles = this.ToEnumrable(this.StandartHeader);
			if (0 < stdHeaderFiles.Count())
			{
				testDataInfo.StubIncludeStandardHeaderFiles = stdHeaderFiles;
			}
			else
			{
				testDataInfo.StubIncludeStandardHeaderFiles = new List<string>(0);
			}

			IEnumerable<string> usrHeaderFiles = this.ToEnumrable(this.StandartHeader);
			if (0 < stdHeaderFiles.Count())
			{
				testDataInfo.StubIncludeUserHeaderFiles = usrHeaderFiles;
			}
			else
			{
				testDataInfo.StubIncludeUserHeaderFiles = new List<string>(0);
			}
		}
	}
}
