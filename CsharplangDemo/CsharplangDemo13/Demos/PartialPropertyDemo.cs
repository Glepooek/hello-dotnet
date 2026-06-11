namespace CsharplangDemo13.Demos;

// C# 13 新增: partial 可用于属性和索引器 (不仅限于方法)
// 典型场景: 源生成器只提供实现声明, 用户只写声明声明

public partial class UserProfile
{
    // 声明声明 (declaration declaration) -- 只有签名, 无主体
    public partial string Name { get; set; }
    public partial int Age { get; set; }

    // 带验证逻辑的 partial 索引器声明
    public partial string this[string key] { get; set; }
}

public partial class UserProfile
{
    // 实现声明 (implementation declaration) -- 提供主体
    private string _name = string.Empty;
    public partial string Name
    {
        get => _name;
        set => _name = value.Trim();     // 自动去除首尾空格
    }

    private int _age;
    public partial int Age
    {
        get => _age;
        set => _age = value < 0 ? 0 : value;  // 防止负数
    }

    private readonly Dictionary<string, string> _extras = [];
    public partial string this[string key]
    {
        get => _extras.TryGetValue(key, out var v) ? v : string.Empty;
        set => _extras[key] = value;
    }
}

public static class PartialPropertyDemo
{
    public static void Run()
    {
        var user = new UserProfile
        {
            Name = "  张三  ",  // 赋值时自动 Trim
            Age = -5            // 赋值时自动修正为 0
        };
        user["email"] = "zhang@example.com";

        Console.WriteLine($"  Name (已 Trim): '{user.Name}'");
        Console.WriteLine($"  Age  (非负修正): {user.Age}");
        Console.WriteLine($"  email: {user["email"]}");
    }
}
