/* ARGMAP - defines an argument map */
using App.type;

namespace App.code {
  class ArgMap : ListMap<string> {

    // get arguments from a string
    public ArgMap(Map<ArgType> types, string args) : this(types, Strings.Split(args)) { }

    // get arguments from a string array
    public ArgMap(Map<ArgType> types, string[] args) {
      string opt = null; bool group = false;
      for (int i = 0; i < args.Length; i++) {
        if (opt != null) {
          if (opt == "") Add(opt, args[i]);
          else if (group) { if (args[i] == ")") { group = false; opt = null; } else Add(opt, args[i]); }
          else if (args[i] == "(") group = true;
          else { Add(opt, args[i]); opt = null; }
        }
        else if (args[i].StartsWith("-")) {
          if (!types.ContainsKey(opt = args[i])) Set("_invalid", opt);
          else if (types[opt] != ArgType.NORMAL) { Set(opt, ""); opt = types[opt] == ArgType.ZERO ? null : ""; };
        }
        else { opt = ""; Add(opt, args[i]); }
      }
    }
  }
}
