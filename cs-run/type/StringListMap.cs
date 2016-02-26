/* STRINGLISTMAP - datatype for string input arguments */
using System;
using System.Linq;
using System.Collections.Generic;

namespace cs_run.type
{
	class StringListMap : Dictionary<string, List<string>> {

		// add a value
		public StringListMap Add(string key, string val) {
			if (!ContainsKey(key)) this[key] = new List<string>();
			this[key].Add(val);
			return this;
		}
	}
}
