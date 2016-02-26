/* PROGRAM : main program */
/* thanks Simeon!: http://simeonpilgrim.com/blog/2007/12/04/compiling-and-running-code-at-runtime/ */

// required modules
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Microsoft.CSharp;
using cs_run.type;


// data types
using IntMap = System.Collections.Generic.Dictionary<string, int>;


namespace cs_run
{
	class Program {

		// run the code
		static void Main(string[] args) {
			StringListMap argm = GetArgs(new string[] {"-c", "\"2+2\""});
			argm.Add("-r", "System.dll").Add("-r", "System.Core.dll").Add("-r", "Microsoft.CSharp.dll");
			CompilerResults res = Compile(argm["-r"].ToArray(), GetSources(argm));
			try { if (res != null) RunCode(res, GetCallArgs(argm)); }
			catch (Exception e) { Console.WriteLine(e.ToString() + "\n" + e.StackTrace); }
		}

		// compile a code block
		static CompilerResults Compile(string[] refs, string[] sources) {
			string pwd = Directory.GetCurrentDirectory();
			CSharpCodeProvider compiler = new CSharpCodeProvider();
			CompilerResults res = compiler.CompileAssemblyFromSource(CompileParams(refs), sources);
			if (res.Errors.HasErrors) { Console.WriteLine(CompileErrors(res)); return null; }
			return res;
		}

		// run compiled code
		static void RunCode(CompilerResults res, object[] args) {
			object ret = res.CompiledAssembly.EntryPoint.Invoke(null, args);
			if (ret != null) Console.WriteLine(ret);
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
			Console.WriteLine(args["-c"].Last());
			if (args.ContainsKey("-c"))
				return new string[] {
					"using System;\n"+
					"namespace cs_run_env {\n"+
					"public class Program {\n"+
					"public static void Main(string[] args) {\n"+
					"Console.WriteLine("+args["-c"].Last()+");\n"+
					"} } }"
				};
			string[] inputs = args["-i"].ToArray();
			string[] o = new string[inputs.Length];
			for (int i = 0; i < inputs.Length; i++)
				o[i] = File.ReadAllText(inputs[i]);
			return o;
		}

		// get call arguments
		static string[] GetCallArgs(StringListMap args) {
			string carg = args.ContainsKey("-a") ? args["-a"].Last() : "\"\"";
			return carg.Substring(1, carg.Length-2).Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
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
	}
}
