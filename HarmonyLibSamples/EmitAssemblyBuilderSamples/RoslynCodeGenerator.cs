using System;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace EmitAssemblyBuilderSamples;

public class RoslynCodeGenerator
{
    public static void Main()
    {
        // 1. 定义要生成的C#代码
        string code = @"
            namespace DynamicCode
            {
                public class Calculator
                {
                    public static int Add(int a, int b)
                    {
                        return a + b;
                    }
                }
            }";

        // 2. 解析代码
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

        // 3. 定义引用
        MetadataReference[] references = new MetadataReference[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location)
        };

        // 4. 设置编译选项
        CSharpCompilationOptions options = new CSharpCompilationOptions(
            OutputKind.DynamicallyLinkedLibrary, // 生成DLL
            optimizationLevel: OptimizationLevel.Release);

        // 5. 编译代码
        CSharpCompilation compilation = CSharpCompilation.Create(
            "DynamicAssembly",
            new[] { syntaxTree },
            references,
            options);

        // 6. 输出到磁盘
        string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DynamicAssembly.dll");
        using FileStream? stream = new FileStream(outputPath, FileMode.Create);
        EmitResult result = compilation.Emit(stream);

        if (result.Success)
        {
            Console.WriteLine($"程序集已保存到: {outputPath}");
        }
        else
        {
            // 输出编译错误
            foreach (Diagnostic diagnostic in result.Diagnostics)
            {
                Console.WriteLine($"{diagnostic.Id}: {diagnostic.GetMessage()}");
            }
        }
    }
}
