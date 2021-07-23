using AutoTestPrep.Model.InputInfos;
using AutoTestPrep.Model.Option;
using AutoTestPrep.Model.Tempaltes;
using AutoTestPrep.Model.Tempaltes.Driver.gtest;
using CSEngineer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Writer
{
	public class TestDriverWriter : IWriter
	{
		/// <summary>
		/// Write test driver code into file specified by <para>path</para>.
		/// </summary>
		/// <param name="path">Path to file to output.</param>
		/// <param name="parameters">Collection of parameters used to genearate codes.</param>
		public void Write(string path, object[] parameters)
		{
			Logger.INFO("Start generating test driver codes.");

			try
			{
				var writers = new IWriter[]
				{
					new TestDriverSourceWriter(),
					new TestDriverHeaderWriter()
				};

				foreach (var writer in writers)
				{
					writer.Write(path, parameters);
				}
			}
			catch (NullReferenceException)
			{
				Logger.WARN("Skip generating driver source code and header code.");
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
