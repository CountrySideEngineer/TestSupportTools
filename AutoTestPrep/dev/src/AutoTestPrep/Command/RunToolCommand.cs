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
using CSEngineer;


namespace AutoTestPrep.Command
{
	/// <summary>
	/// Run main tool command.
	/// Read test design document and create test code 
	/// </summary>
	public class RunToolCommand
	{
		/// <summary>
		/// Run cimmand interface.
		/// </summary>
		/// <param name="parameter">Data for command.</param>
		public void Execute(object parameter)
		{
			string logFileName = @"./log.txt";
			using (var stream = new StreamWriter(logFileName, false, Encoding.UTF8))
			{
				Logger.Level = Logger.LogLevel.All;
				Logger.AddStream(stream);
				Logger.INFO("Start logging!");
				this._Run(parameter);
				Logger.RemoveStream(stream);
			}

		}

		/// <summary>
		/// Run command.
		/// </summary>
		/// <param name="data">Parameters used when run command.</param>
		protected void _Run(object data)
		{
			try
			{
				var inputInfos = data as TestDataInfo;

				Logger.INFO($"Start parsing the test data in {inputInfos.TestDataFilePath}.");
				var parser = new TestParser();
				var tests = (IEnumerable<Test>)parser.Parse(inputInfos.TestDataFilePath);
				IEnumerable<IWriter> writers = new List<IWriter>
				{
					new StubWriter(),
					new TestDriverWriter(),
				};

				Logger.INFO("Start generating test codes.");
				var helper = new WriterHelper();
				foreach (var testItem in tests)
				{
					helper.Write(inputInfos, testItem, writers);
				}
			}
			catch (InvalidCastException)
			{
				Logger.FATAL("Input data type error!");

				throw;
			}
			catch (System.Exception ex)
			{
				Logger.ERROR(ex.Message);
			}
		}
	}
}
