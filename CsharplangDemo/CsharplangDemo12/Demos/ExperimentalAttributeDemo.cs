using System.Diagnostics.CodeAnalysis;

namespace CsharplangDemo12.Demos;

// ── [Experimental] 特性 ───────────────────────────────────────────────
// 标记实验性 API，访问时编译器发出诊断警告（不是错误）
// 参数: 诊断 ID（字符串），调用方可用 #pragma warning disable <ID> 抑制

[Experimental("DEMO001", UrlFormat = "https://example.com/experimental#{0}")]
public static class ExperimentalFeature
{
    public static string Compute(int x) => $"实验结果: {x * x}";
}

// 整个类标记为实验性: 类中所有成员自动继承该标记
[Experimental("DEMO002")]
public class ExperimentalService
{
    public void Process() => Console.WriteLine("  [实验性服务] 处理中...");
}

// 单个方法标记为实验性
public class MixedApi
{
    public void StableMethod() => Console.WriteLine("  [稳定] 正常方法");

    [Experimental("DEMO003")]
    public void ExperimentalMethod() => Console.WriteLine("  [实验性] 新方法");
}

public static class ExperimentalAttributeDemo
{
    // 在调用实验性 API 的方法上声明相同的诊断 ID，表示"已知晓风险"
    // 或使用 #pragma warning disable 抑制
#pragma warning disable DEMO001
    static void UseExperimentalFeature() =>
        Console.WriteLine($"  {ExperimentalFeature.Compute(7)}");
#pragma warning restore DEMO001

#pragma warning disable DEMO002
    static void UseExperimentalService() => new ExperimentalService().Process();
#pragma warning restore DEMO002

#pragma warning disable DEMO003
    static void UseMixedApi()
    {
        var api = new MixedApi();
        api.StableMethod();
        api.ExperimentalMethod();
    }
#pragma warning restore DEMO003

    public static void Run()
    {
        UseExperimentalFeature();
        UseExperimentalService();
        UseMixedApi();

        Console.WriteLine();
        Console.WriteLine("  [Experimental] 用途:");
        Console.WriteLine("    • 标记 API 尚未稳定，可能在未来版本变更");
        Console.WriteLine("    • 调用时编译器自动发出警告，强制使用方知晓风险");
        Console.WriteLine("    • 比文档注释更强的约束: 代码层面的\"此处有坑\"提示");
        Console.WriteLine("    • 使用 #pragma warning disable <ID> 明确抑制");

        // ── 对比: 拦截器（Interceptors）——实验性特性，仅作说明 ────────
        Console.WriteLine();
        Console.WriteLine("  [拦截器 Interceptors] (C# 12 预览，不建议生产使用):");
        Console.WriteLine("    • 源生成器工具: 编译时将方法调用替换为拦截器实现");
        Console.WriteLine("    • 需配置: <InterceptorsPreviewNamespaces>");
        Console.WriteLine("    • 典型用途: ASP.NET Core 源生成器优化路由注册");
        Console.WriteLine("    • 用 [InterceptsLocation] 特性指定被拦截的调用位置");
    }
}
