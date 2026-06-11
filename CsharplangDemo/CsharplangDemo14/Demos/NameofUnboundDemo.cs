namespace CsharplangDemo14.Demos;

public static class NameofUnboundDemo
{
    public static void Run()
    {
        // C# 14 前: nameof 只接受封闭泛型类型
        string closed1 = nameof(List<int>);           // "List"
        string closed2 = nameof(Dictionary<string, int>); // "Dictionary"

        // C# 14 起: nameof 支持未绑定泛型类型 (无需填写类型参数)
        string unbound1 = nameof(List<>);             // "List"
        string unbound2 = nameof(Dictionary<,>);      // "Dictionary"
        string unbound3 = nameof(Func<,,,>);          // "Func"

        Console.WriteLine($"  nameof(List<int>)           = {closed1}");
        Console.WriteLine($"  nameof(Dictionary<string,int>) = {closed2}");
        Console.WriteLine();
        Console.WriteLine($"  nameof(List<>)              = {unbound1}");
        Console.WriteLine($"  nameof(Dictionary<,>)       = {unbound2}");
        Console.WriteLine($"  nameof(Func<,,,>)           = {unbound3}");

        // 实际用途: 日志/错误信息中引用泛型类型名, 无需关心具体类型参数
        Console.WriteLine();
        Console.WriteLine($"  用途示例 -- 泛型仓库注册:");
        Console.WriteLine($"    Register({nameof(IRepository<>)}<TEntity>)");

        // 配合 typeof 对比: typeof 早就支持未绑定泛型
        Type t = typeof(List<>);
        Console.WriteLine($"  typeof(List<>).Name = {t.Name}");
        Console.WriteLine("  nameof(List<>) 现在与 typeof(List<>).Name 行为一致");
    }
}

// 演示用接口
public interface IRepository<T> { }
