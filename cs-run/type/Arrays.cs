using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_run.type
{
	class Arrays {

		// shift elements up one level
		public static string[] StringShift(string[] src, int amt=1) {
			amt = amt < src.Length ? amt : src.Length;
			string[] dst = new string[src.Length - 1];
			Array.Copy(src, 1, dst, 0, dst.Length);
			return dst;
		}
	}
}
