using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using App.type;
using App.code;


namespace App {
  class Program {
    // Constants.
    static readonly string INLINE_SOURCE = "using System; namespace App { public class Program { public static void Main(string[] args) { %i; } } }";


    // Compile a code block.
    static CompilerResults Compile(string[] refs, string[] sources) {
      CSharpCodeProvider compiler = new CSharpCodeProvider();
      CompilerResults res = compiler.CompileAssemblyFromSource(CompileParams(refs), sources);
      if (res.Errors.HasErrors) { Console.WriteLine(CompileErrors(res)); return null; }
      return res;
    }

    // Run compiled code.
    static void RunCode(CompilerResults res, object[] args) {
      foreach (Module mod in res.CompiledAssembly.GetModules())
        foreach (Type typ in mod.GetTypes()) {
          MethodInfo mthd = typ.GetMethod("Main", new Type[] { args.GetType() });
          if (mthd == null) continue;
          object ret = mthd.Invoke(null, new object[] { args });
          if (ret != null) Console.WriteLine(ret);
        }
    }

    // Get compile errors.
    static string CompileErrors(CompilerResults res) {
      StringBuilder str = new StringBuilder("Compile Error: \n");
      foreach (CompilerError err in res.Errors)
        str.Append(err).Append('\n');
      return str.ToString();
    }

    // Get compile parameters.
    static CompilerParameters CompileParams(string[] refs) {
      CompilerParameters p = new CompilerParameters(refs);
      p.GenerateInMemory = true;
      p.TreatWarningsAsErrors = false;
      p.GenerateExecutable = false;
      p.CompilerOptions = "/optimize";
      return p;
    }

    // Get sources.
    static string[] GetSources(ListMap<string> args) {
      string[] o = null;
      if (args.ContainsKey("")) {
        o = new string[] { String.Join(" ", args[""].ToArray()) };
        if (args.ContainsKey("-s")) o[0] = INLINE_SOURCE.Replace("%i", o[0]);
        else if (!args.ContainsKey("-c")) o[0] = INLINE_SOURCE.Replace("%i", "Console.WriteLine(" + o[0] + ")");
      }
      else if (args.ContainsKey("-i")) {
        o = args["-i"].ToArray();
        for (int i = 0; i < o.Length; i++)
          o[i] = File.ReadAllText(o[i]);
      }
      return o;
    }

    // Get call arguments.
    static string[] GetCallArgs(ListMap<string> args) {
      return args.ContainsKey("-a") ? args["-a"].ToArray() : new string[0];
    }

    // Get invalid option details.
    static string GetInvalidOptions(ListMap<string> args) {
      StringBuilder str = new StringBuilder("invalid option(s): ");
      foreach (string opt in args["_invalid"])
        str.Append(opt).Append(' ');
      return str.ToString();
    }

    // Get option types.
    static Map<ArgType> GetOptionTypes() {
      return new Map<ArgType>() {
                { "-r", ArgType.NORMAL },
                { "-a", ArgType.NORMAL },
                { "-i", ArgType.NORMAL },
                { "-c", ArgType.EXTENDED },
                { "-s", ArgType.EXTENDED },
                { "-f", ArgType.EXTENDED }
            };
    }

    // Return help text.
    static string GetHelp() {
      StringBuilder z = new StringBuilder("ecs [-r <references>] [-a <arguments>] [-i <input source file>]... [-c / -s / -f] <code>\n");
      z.Append("-r : Add necessary references (e.g. -r System.Data.dll)\n");
      z.Append("     (System.dll, System.Core.dll and Microsoft.CSharp.dll are included by default)\n");
      z.Append("-a : Arguments to pass to Main() function (e.g. -a (10 2))\n");
      z.Append("     (must be enclosed using paranthesis)\n");
      z.Append("-i : Specify an input source file (e.g. -i Program.cs)\n");
      z.Append("     (atleast one of the input files must have a Main(), or nothing will be executed)\n");
      z.Append("-c : Specify entire source code to be executed as parameters\n");
      z.Append("     (e.g. -c using System; namespace test { class Program { public static void Main(string[] args) { Console.WriteLine(\"Hello World!\") } } })\n");
      z.Append("-s : Specify a statement to be executed as parameters\n");
      z.Append("     (e.g. -s Console.WriteLine(\"Hello Statement!\");)\n");
      z.Append("-f : Specify a function to be executed as parameters (this is default option)\n");
      z.Append("     (e.g. -f DateTime.Now)\n");
      return z.ToString();
    }


    // Main.
    public static void Main(string[] args) {
      args = Arrays<string>.Shift(Strings.Split(Environment.CommandLine));
      if (args.Length <= 0) { Console.WriteLine(GetHelp()); return; }
      ListMap<string> argm = new ArgMap(GetOptionTypes(), args);
      if (argm.ContainsKey("_invalid")) { Console.WriteLine(GetInvalidOptions(argm)); return; }
      argm.Set("-r", "System.dll").Set("-r", "System.Core.dll").Set("-r", "Microsoft.CSharp.dll");
      string[] sources = GetSources(argm);
      if (sources == null || sources.Length == 0) { Console.WriteLine(""); return; }
      CompilerResults res = Compile(argm["-r"].ToArray(), GetSources(argm));
      try { if (res != null) RunCode(res, GetCallArgs(argm)); }
      catch (Exception e) { Console.WriteLine(e.ToString() + "\n" + e.StackTrace); }
    }
  }
}
