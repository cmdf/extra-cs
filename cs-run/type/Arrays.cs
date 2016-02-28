/* ARRAYS - defines basic array functions */
using System;


namespace cs_run.type
{
	class Arrays<T> {

		// shift elements up one level
		public static T[] Shift(T[] src, int amt=1) {
			amt = amt < src.Length ? amt : src.Length;
			T[] dst = new T[src.Length - amt];
			Array.Copy(src, amt, dst, 0, dst.Length);
			return dst;
		}
	}
}
