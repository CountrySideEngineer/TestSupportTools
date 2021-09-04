using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model
{
	public class TestFramework
	{
		public enum Framework
		{
			google_test,
			min_unit,
			Invalid
		};

		public static Framework ToFramework(int index)
		{
			try
			{
				Framework framework = (Framework)Enum.ToObject(typeof(Framework), index);
				return framework;
			}
			catch (ArgumentException)
			{
				return Framework.Invalid;
			}
		}
	}
}
