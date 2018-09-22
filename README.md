Run C# command from console.
> 1. Download [exe file](https://github.com/winp/extra-cs/releases/download/1.0.0/ecs.exe).
> 2. Copy to `C:\Program_Files\Scripts`.
> 3. Add `C:\Program_Files\Scripts` to `PATH` environment variable.


```
ecs [-r <references>] [-a <arguments>] [-i <input source file>]... [-c / -s / -f] <code>
:: -r : Add necessary references (e.g. -r System.Data.dll)
::      (System.dll, System.Core.dll and Microsoft.CSharp.dll are included by default)
:: -a : Arguments to pass to Main() function (e.g. -a (10 2))
::      (must be enclosed using paranthesis)
:: -i : Specify an input source file (e.g. -i Program.cs)
::      (atleast one of the input files must have a Main(), or nothing will be executed)
:: -c : Specify entire source code to be executed as parameters
::      (e.g. -c using System; namespace test { class Program { public static void Main(string[] args) { Console.WriteLine("Hello World!") } } })
:: -s : Specify a statement to be executed as parameters
::      (e.g. -s Console.WriteLine("Hello Statement!");)
:: -f : Specify a function to be executed as parameters (this is default option)
::      (e.g. -f DateTime.Now)
```

```batch
:: Get Date and Time
ecs DateTime.Now
: 28/2/2016 10:26:31 PM

:: Linux-like Clear Screen
ecs -s Console.SetCursorPosition(0, Console.CursorTop+24); Console.SetCursorPosition(0, Console.CursorTop-23);
: (screen cleared)

:: Message Box
ecs -r System.Windows.Forms.dll -s System.Windows.Forms.MessageBox.Show("Something Happened!", "Close")
: (a message box appears)
```


[![Merferry](https://i.imgur.com/xTvqw9i.jpg)](https://merfery.github.io)
> References: [compiling and running code at runtime](http://simeonpilgrim.com/blog/2007/12/04/compiling-and-running-code-at-runtime/).
