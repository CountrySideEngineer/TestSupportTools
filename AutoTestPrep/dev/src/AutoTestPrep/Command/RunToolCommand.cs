using AutoTestPrep.Model;
using AutoTestPrep.Model.InputInfos;
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
			//try
			//{
			//	var inputInfos = data as TestDataInfo;
			//	IDriverStubCreator driveStubCreator = SequenceFactory.Create(inputInfos);
			//	driveStubCreator.Create(inputInfos);
			//}
			//catch (System.Exception ex)
			//when ((ex is InvalidCastException) ||
			//	(ex is IOException))
			//{
			//	Logger.FATAL("Input data type error!");

			//	throw;
			//}
			//catch (System.Exception ex)
			//{
			//	Logger.ERROR(ex.Message);
			//}
		}
	}
}
