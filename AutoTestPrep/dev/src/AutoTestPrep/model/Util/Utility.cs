using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestPrep.Model.Util
{
	public class Utility
	{
		/// <summary>
		/// Get the number of poiter code from target string.
		/// The string should be data type string.
		/// </summary>
		/// <param name="target">Target data type string to check.</param>
		/// <returns>The number of pointer code.</returns>
		public static int GetPointerNum(string target)
		{
			int pointerNum = 0;
			int targetLen = target.Length;
			for (int index = 0; index < targetLen; index++)
			{
				string targetChar = target.Substring(targetLen - 1 - index, 1);
				if (("*").Equals(targetChar, StringComparison.Ordinal))
				{
					pointerNum++;
				}
				else
				{
					break;
				}
			}
			return pointerNum;
		}

		/// <summary>
		///	Deletes characaters "*" that indicates the pointer from the end.
		/// </summary>
		/// <param name="target">Target string</param>
		/// <returns>String pointer code deleted.</returns>
		public static string RemovePointer(string target)
		{
			int pointerNum = Utility.GetPointerNum(target);
			int targetLen = target.Length;
			string targetWithoutPointer = target.Substring(0, targetLen - 1 - pointerNum);

			return targetWithoutPointer;
		}
	}
}
