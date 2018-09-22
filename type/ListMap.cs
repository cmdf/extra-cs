/* LISTMAP - datatype for string input arguments */
using System.Collections.Generic;


namespace App.type {
  class ListMap<T> : Map<List<T>> {

    // add a value
    public ListMap<T> Add(string key, T val) {
      if (!ContainsKey(key)) this[key] = new List<T>();
      this[key].Add(val);
      return this;
    }

    // set a value
    public ListMap<T> Set(string key, T val) {
      if (!ContainsKey(key) || !this[key].Contains(val)) return Add(key, val);
      return this;
    }
  }
}
