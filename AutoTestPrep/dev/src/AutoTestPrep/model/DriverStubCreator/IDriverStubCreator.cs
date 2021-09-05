using AutoTestPrep.Model.InputInfos;
using AutoTestPrep.Model.Parser;
using AutoTestPrep.Model.Writer;
using CSEngineer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.DriverStubCreator
{
	/// <summary>
	/// Factory object to run sequence
	/// </summary>
	public class SequenceFactory
	{
		public static IDriverStubCreator Create(TestDataInfo dataInfo)
		{
			IDriverStubCreator driverStubCreator = null;
			TestFramework.Framework framework = dataInfo.FrameworkTye;
			if (TestFramework.Framework.google_test.Equals(framework))
			{
				driverStubCreator = new GTestDriverStubCreator();
			}
			else if (TestFramework.Framework.min_unit.Equals(framework))
			{
				driverStubCreator = new MinUnitDriverStubCreator();
			}
			else if(TestFramework.Framework.mid_unit.Equals(framework))
			{
				driverStubCreator = new MidUnitDriverStubCreator();
			}
			return driverStubCreator;
		}
	}

	/// <summary>
	/// Create 
	/// </summary>
	public interface IDriverStubCreator
	{
		void Create(TestDataInfo dataInfo);
	}

}
