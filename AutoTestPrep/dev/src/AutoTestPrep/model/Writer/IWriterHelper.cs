using AutoTestPrep.Model.InputInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Writer
{
	public interface IWriterHelper
	{
		/// <summary>
		/// Interface to write 
		/// </summary>
		/// <param name="testDataInfo"></param>
		/// <param name="tests"></param>
		/// <param name="writers"></param>
		void Write(TestDataInfo testDataInfo, IEnumerable<Test> tests, IEnumerable<IWriter> writers);
	}
}
