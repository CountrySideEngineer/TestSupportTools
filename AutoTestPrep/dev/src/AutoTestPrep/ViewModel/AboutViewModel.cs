using CSEngineer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CSEngineer.ViewModel
{
	public class AboutViewModel : ViewModelBase
	{
		/// <summary>
		/// Tilte of the application.
		/// </summary>
		public string Title
		{
			get
			{
				var assmebly = Assembly.GetExecutingAssembly();
				var fileVersionInfo = FileVersionInfo.GetVersionInfo(assmebly.Location);
				return fileVersionInfo.FileDescription;
			}
		}

		/// <summary>
		/// Version information of the application.
		/// </summary>
		public string Version
		{
			get
			{
				var assmebly = Assembly.GetExecutingAssembly();
				var fileVersionInfo = FileVersionInfo.GetVersionInfo(assmebly.Location);
				return $"Version {fileVersionInfo.ProductVersion}";
			}
		}

		/// <summary>
		/// Copyright of the application.
		/// </summary>
		public string CopyRight
		{
			get
			{
				var info = Assembly.GetEntryAssembly().GetReferencedAssemblies();


				var assmebly = Assembly.GetExecutingAssembly();
				var fileVersionInfo = FileVersionInfo.GetVersionInfo(assmebly.Location);
				return fileVersionInfo.LegalCopyright;
			}
		}

		public IEnumerable<string> Installed
		{
			get
			{
				var installedAssemblies = new List<string>();
				var assemblyInfos = Assembly.GetEntryAssembly().GetReferencedAssemblies();
				foreach (var assemblyItem in assemblyInfos)
				{
					string assemblyInfo = $"{assemblyItem.Name} - {assemblyItem.Version.ToString()}";
					installedAssemblies.Add(assemblyInfo);
				}
				installedAssemblies.Sort();
				return installedAssemblies;
			}
		}
	}
}
