/* STRINGS - basic string functions */
using System;


namespace cs_run.type
{
	class Strings {

		// constants
		public static readonly char[] WHITE_SPACE = new char[] { ' ', '\t', '\r', '\n' };

		// get substring like javascript
		public static string Substring(string s, int begin, int end=int.MaxValue) {
			end = end > s.Length? s.Length : (end < 0? s.Length + end : end);
			return s.Substring(begin, end-begin);
		}

		// split string without empty entries
		public static string[] Split(string s, char[] sep=null) {
			return s.Split(sep != null? sep : WHITE_SPACE, StringSplitOptions.RemoveEmptyEntries);
		}
	}
}
