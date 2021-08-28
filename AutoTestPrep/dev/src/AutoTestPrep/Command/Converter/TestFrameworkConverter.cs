using AutoTestPrep.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AutoTestPrep.Command.Converter
{
	public class TestFrameworkConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var testFrameworkName = value as string;
			if (null == testFrameworkName)
			{
				return null;
			}

			TestFramework.Framework frameworkType = TestFramework.Framework.Invalid;
			if ("framework_google_test".Equals(testFrameworkName))
			{
				frameworkType = TestFramework.Framework.google_test;
			}
			else if ("framework_min_unit".Equals(testFrameworkName))
			{
				frameworkType = TestFramework.Framework.min_unit;
			}
			else
			{
				frameworkType = TestFramework.Framework.Invalid;
			}
			return frameworkType;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
