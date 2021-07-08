using AutoTestPrep.Model;
using AutoTestPrep.Model.InputInfos;
using AutoTestPrep.Model.Parser;
using AutoTestPrep.Model.Writer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
		/// <summary>
		/// Run command.
		/// </summary>
		/// <param name="data">Parameters used when run command.</param>
		public void Run(object data)
		{
			try
			{
				var inputInfos = data as TestDataInfo;
				var parser = new TestParser();
				var tests = (IEnumerable<Test>)parser.Parse(inputInfos.TestDataFilePath);
				IEnumerable<IWriter> writers = new List<IWriter>
				{
					new StubWriter(),
					new TestDriverWriter(),
				};
				var helper = new WriterHelper();
				foreach (var testItem in tests)
				{
					helper.Write(inputInfos, testItem, writers);
				}
			}
			catch (InvalidCastException)
			{
				throw;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}
	}
}
