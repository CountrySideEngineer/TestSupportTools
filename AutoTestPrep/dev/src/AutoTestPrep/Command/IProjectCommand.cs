using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Command
{
	public interface IProjectCommand
	{
		/// <summary>
		/// Execute command.
		/// </summary>
		/// <param name="parameter">Command argument parameter.</param>
		void Execute(object parameter);
	}
}
