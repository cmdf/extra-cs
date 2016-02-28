# cs-run

Use this program to run any **C# command** from the *command prompt* or from
a *batch file*.


## usage

- Download [cs-run](#).
- Drop it in your *directory*.
- If you want it to be usable from **anywhere**, add its path to the `PATH` *environment variable*.
- Start a *command prompt* in the **same** *directory*.
- Follow the [examples](#examples) or the [reference](#reference).


## examples

* Get Date and Time
```batch
cs-run DateTime.Now
: 28/2/2016 10:26:31 PM
```

* Linux-like Clear Screen
```batch
cs-run -s Console.SetCursorPosition(0, Console.CursorTop+24); Console.SetCursorPosition(0, Console.CursorTop-23)
: (screen cleared)
```

* Message Box
```batch
cs-run -r System.Windows.Forms.dll -s System.Windows.Forms.MessageBox.Show("Something Happened!", "Close")
: (a message box appears)
```


## reference

```
cs-run [-r <references>] [-a <arguments>] [-i <input source file>]... [-c / -s / -f] <code>
-r : Add necessary references (e.g. -r System.Data.dll)
     (System.dll, System.Core.dll and Microsoft.CSharp.dll are included by default)
-a : Arguments to pass to Main() function (e.g. -a (10 2))
     (must be enclosed using paranthesis)
-i : Specify an input source file (e.g. -i Program.cs)
     (atleast one of the input files must have a Main(), or nothing will be executed)
-c : Specify entire source code to be executed as parameters
     (e.g. -s using System; namespace test { class Program { public static void Main(string[] args) { Console.WriteLine("Hello World!") } } })
-s : Specify a statement to be executed as parameters
     (e.g. -s Console.WriteLine("Hello Statement!");)
-f : Specify a function to be executed as parameters (this is default option)
     (e.g. -f DateTime.Now)
```


## objective

The objective of this project is to provide a mechanism to perform operations
not generally possible due to the lack of availability of command-line programs
for all necessary purposes.


## thanks

People at [Stack Overflow] for a lot of QAs:
[0](http://stackoverflow.com/questions/19436244/how-to-reference-an-unused-parameter)
[1](http://stackoverflow.com/questions/17047602/proper-way-to-initialize-a-c-sharp-dictionary-with-values-already-in-it)
[2](http://stackoverflow.com/questions/473782/inline-functions-in-c)
[3](http://stackoverflow.com/questions/28155317/what-is-the-printf-in-c-sharp)
[4](http://stackoverflow.com/questions/8099631/how-to-return-value-from-action)
[5](http://stackoverflow.com/questions/10198370/execute-lambda-expression-immediately-after-its-definition)
[6](http://stackoverflow.com/questions/31683297/cannot-convert-lambda-expression-to-type-bool)
[7](http://stackoverflow.com/questions/15159450/cannot-convert-lambda-expression-to-type-bool-because-it-is-not-a-delegate)
[8](http://stackoverflow.com/questions/11645059/using-where-cannot-convert-lambda-expression-to-type-bool)
[9](http://stackoverflow.com/questions/9219958/remove-first-element-from-array)
[10](http://stackoverflow.com/questions/653563/passing-command-line-arguments-in-c-sharp)
[11](http://stackoverflow.com/questions/16453627/can-i-get-the-arguments-to-my-application-in-the-original-form-e-g-including-qu)
[12](http://stackoverflow.com/questions/9287812/backslash-and-quote-in-command-line-arguments)
[13](http://stackoverflow.com/questions/2168495/what-are-classes-and-modules-for-in-c-sharp)
[14](http://stackoverflow.com/questions/3870480/c-sharp-type-getmethods-doesnt-return-main-method)
[15](http://stackoverflow.com/questions/199761/how-can-you-use-optional-parameters-in-c)
[16](http://stackoverflow.com/questions/15408640/declare-char-array-in-static-class)
[17](http://stackoverflow.com/questions/21342949/how-can-i-split-a-string-while-ignore-commas-in-between-quotes)
[18](http://stackoverflow.com/questions/9271209/how-the-runtime-knows-which-class-contain-the-main-method-in-c-sharp-application)
[19](http://stackoverflow.com/questions/808948/how-do-i-compile-assembly-routines-for-use-with-a-c-program-gnu-assembler)
[20](http://stackoverflow.com/questions/4181668/execute-c-sharp-code-at-runtime-from-code-file)
[21](http://stackoverflow.com/questions/10314815/trying-to-compile-and-execute-c-sharp-code-programmatically)
[22](http://stackoverflow.com/questions/9577567/can-a-c-sharp-dll-assembly-contain-an-entry-point)
[23](http://stackoverflow.com/questions/20804558/what-does-get-or-set-accessor-expected-mean)
[24](http://stackoverflow.com/questions/24665649/why-does-c-sharp-not-allow-me-to-call-a-void-method-as-part-of-the-return-statem)
[25](http://stackoverflow.com/questions/36350/how-to-pass-a-single-object-to-a-params-object)
[26](http://stackoverflow.com/questions/9990378/converting-a-list-to-an-array-with-toarray)
[27](http://stackoverflow.com/questions/16747774/how-do-i-add-a-system-core-dll-reference-to-my-project-in-xamarin-studio-monodev)
[28](http://stackoverflow.com/questions/823024/can-i-add-a-reference-to-system-core-dll-net-3-5-to-a-net-2-0-application-an)
[29](http://stackoverflow.com/questions/826398/is-it-possible-to-dynamically-compile-and-execute-c-sharp-code-fragments)
[30](http://stackoverflow.com/questions/4800267/how-to-execute-code-that-is-in-a-string)

[Microsoft MSDN](https://msdn.microsoft.com) for the huge support in every direction.
[0](https://msdn.microsoft.com/en-us/library/dd264739.aspx)
[1](https://msdn.microsoft.com/en-us/library/ms228504.aspx)
[2](https://msdn.microsoft.com/en-us/library/z5z9kes2.aspx)
[3](https://msdn.microsoft.com/en-us/library/bb384043.aspx)
[4](https://msdn.microsoft.com/en-IN/library/bb397687.aspx)

[Simeon Pilgrim](http://simeonpilgrim.com) for article on Compiling and Running code at runtime
[0](http://simeonpilgrim.com/blog/2007/12/04/compiling-and-running-code-at-runtime/)

[Microsoft Support](https://support.microsoft.com) for article on How to programmatically compile code using C# compiler
[0](https://support.microsoft.com/en-us/kb/304655)

[Bytes.com](https://bytes.com) for QA on Easy way to cast an array of strings to an array of objects
[0](https://bytes.com/topic/c-sharp/answers/275646-easy-way-cast-array-strings-array-objects)

[Dejan Geci](http://headsigned.com/) for his article on C# scripting example using CSharpCodeProvider.
[0](http://headsigned.com/article/csharp-scripting-example-using-csharpcodeprovider)

[Sanjay Legha](http://sanjaylegha.blogspot.in/) for his blog on C# code compilation.
[0](http://paxcel.net/blog/how-to-programmatically-compile-and-use-code-using-c-compiler/)

[Simon Schmid](https://github.com/sschmid) for logo (have not used it yet!)
[0](https://raw.githubusercontent.com/sschmid/Entitas-CSharp/develop/Readme/Images/csharp.png)
