using AutoTestPrep.Model;
using AutoTestPrep.Model.InputInfos;
using AutoTestPrep.Model.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Command
{
	/// <summary>
	/// Run main tool command.
	/// Read test design document and create test code 
	/// </summary>
	public class RunToolCommand
	{
		public void Run(object data)
		{
			try
			{
				var inputInfos = data as TestDataInfo;
				var parser = new TestParser();
				var tests = (IEnumerable<Test>)parser.Parse(inputInfos.TestDataFilePath);
			}
			catch (InvalidCastException)
			{
				throw;
			}
		}
	}
}
