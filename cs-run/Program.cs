/* PROGRAM : main program */
/* thanks Simeon!: http://simeonpilgrim.com/blog/2007/12/04/compiling-and-running-code-at-runtime/ */

// required modules
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using cs_run.type;

namespace cs_run
{
	class Program {
	
		// constants
		static readonly string INLINE_SOURCE_BEGIN = "using System; namespace cs_run { public class Program { public static void Main(string[] args) {";
		static readonly string INLINE_SOURCE_END = "} } }";

		// run the code
		public static void Main(string[] args) {
			StringListMap argm = GetArgs(Arrays.StringShift(Strings.Split(Environment.CommandLine)));
      argm.Set("-r", "System.dll").Set("-r", "System.Core.dll").Set("-r", "Microsoft.CSharp.dll");
			CompilerResults res = Compile(argm["-r"].ToArray(), GetSources(argm));
			try { if (res != null) RunCode(res, GetCallArgs(argm)); }
			catch (Exception e) { Console.WriteLine(e.ToString() + "\n" + e.StackTrace); }
		}

		// compile a code block
		static CompilerResults Compile(string[] refs, string[] sources) {
			CSharpCodeProvider compiler = new CSharpCodeProvider();
			CompilerResults res = compiler.CompileAssemblyFromSource(CompileParams(refs), sources);
			if (res.Errors.HasErrors) { Console.WriteLine(CompileErrors(res)); return null; }
			return res;
		}

		// run compiled code
		static void RunCode(CompilerResults res, object[] args) {
			foreach(Module mod in res.CompiledAssembly.GetModules())
				foreach (Type typ in mod.GetTypes()) {
					MethodInfo mthd = typ.GetMethod("Main", new Type[] { args.GetType() });
					if (mthd == null) continue;
					object ret = mthd.Invoke(null, new object[] { args });
					if (ret != null) Console.WriteLine(ret);
				}
		}

		// get compile errors
		static string CompileErrors(CompilerResults res) {
			StringBuilder str = new StringBuilder("Compile Error: \n");
			foreach (CompilerError err in res.Errors)
				str.Append(err).Append('\n');
			return str.ToString();
		}

		// get compile parameters
		static CompilerParameters CompileParams(string[] refs) {
			CompilerParameters p = new CompilerParameters(refs);
			p.GenerateInMemory = true;
			p.TreatWarningsAsErrors = false;
			p.GenerateExecutable = false;
			p.CompilerOptions = "/optimize";
			return p;
		}

		// get sources
		static string[] GetSources(StringListMap args) {
			if (args.ContainsKey("")) {
				string argv = String.Join(" ", args[""].ToArray());
				if (args.ContainsKey("-c")) return new string[] { argv };
				else if (args.ContainsKey("-s")) return new string[] { INLINE_SOURCE_BEGIN + argv + INLINE_SOURCE_END };
				else return new string[] { INLINE_SOURCE_BEGIN + "Console.WriteLine(" + argv + ");" + INLINE_SOURCE_END };
			}
			string[] o = args["-i"].ToArray();
			for (int i = 0; i < o.Length; i++)
				o[i] = File.ReadAllText(o[i]);
			return o;
		}

		// get call arguments
		static string[] GetCallArgs(StringListMap args) {
			return Strings.SplitQuoted(args.ContainsKey("-a") ? args["-a"].Last() : "\"\"");
		}

		// get arguments to program
		static StringListMap GetArgs(string[] args) {
			string opt = "";
			StringListMap o = new StringListMap();
			for (int i = 0; i < args.Length; i++) {
				if (!args[i].StartsWith("-")) { o.Add(opt, args[i]); opt = ""; continue; }
				if (opt != "") o.Add(opt, "");
				opt = args[i];
			}
			return o;
		}

		// return help string
		static string GetHelp() {
			StringBuilder str = new StringBuilder("cs-run [-r <references>] [-a <arguments>] [-i <input source file>]... [-c / -s / -f] <code>\n");
			str.Append("-r : Add necessary references (e.g. -r System.Data.dll)\n");
			str.Append("     (System.dll, System.Core.dll and Microsoft.CSharp.dll are included by default)\n");
			str.Append("-a : Arguments to pass to Main() function (e.g. -a (10 2))\n");
			str.Append("     (must be enclosed using paranthesis)\n");
			str.Append("-i : Specify an input source file (e.g. -i Program.cs)\n");
			str.Append("     (atleast one of the input files must have a Main(), or nothing will be executed)\n");
			str.Append("-c : Specify entire source code to be executed as parameters\n");
			str.Append("     (e.g. -s using System; namespace test { class Program { public static void Main(string[] args) { Console.WriteLine(\"Hello World!\") } } })\n");
			str.Append("-s : Specify a statement to be executed as parameters\n");
			str.Append("     (e.g. -s Console.WriteLine(\"Hello Statement!\");)\n");
			str.Append("-f : Specify a function to be executed as parameters (this is default option)\n");
			str.Append("     (e.g. -f DateTime.Now)\n");
			return str.ToString();
		}
	}
}
