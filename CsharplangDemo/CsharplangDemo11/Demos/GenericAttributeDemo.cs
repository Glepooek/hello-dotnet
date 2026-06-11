namespace CsharplangDemo11.Demos;

// ── C# 11 前: 特性类不能有泛型类型参数 ───────────────────────────────
// 必须通过传 typeof() 绕过
public class OldValidatorAttribute : Attribute
{
    public OldValidatorAttribute(Type validatorType) =>
        ValidatorType = validatorType;
    public Type ValidatorType { get; }
}

// ── C# 11 起: 泛型特性 ───────────────────────────────────────────────
// 类型参数直接写在特性类上，调用方语法更简洁
public class ValidatorAttribute<T> : Attribute
    where T : class
{
    public Type ValidatorType => typeof(T);
}

public class SerializerAttribute<T> : Attribute
    where T : class
{
    public string Format { get; set; } = "default";
    public Type SerializerType => typeof(T);
}

// ── 演示用的验证器/序列化器 ────────────────────────────────────────
public class EmailValidator   { public static bool Validate(string s) => s.Contains('@'); }
public class PhoneValidator   { public static bool Validate(string s) => s.StartsWith('+'); }
public class JsonSerializer2  { }
public class XmlSerializer2   { }

// ── 应用泛型特性 ─────────────────────────────────────────────────────

// C# 11 前的旧写法
[OldValidator(typeof(EmailValidator))]
public class OldUserModel { public string Email { get; set; } = ""; }

// C# 11 新写法: 更简洁，类型安全
[ValidatorAttribute<EmailValidator>]
public class UserModel { public string Email { get; set; } = ""; }

[ValidatorAttribute<PhoneValidator>]
[SerializerAttribute<JsonSerializer2>(Format = "json")]
public class ContactModel { public string Phone { get; set; } = ""; }

// 泛型特性也可用于方法
public class ApiController
{
    [SerializerAttribute<JsonSerializer2>(Format = "json")]
    public string GetData() => "{}";

    [SerializerAttribute<XmlSerializer2>(Format = "xml")]
    public string GetXml() => "<root/>";
}

public static class GenericAttributeDemo
{
    public static void Run()
    {
        // 通过反射读取泛型特性
        var userAttrs = typeof(UserModel)
            .GetCustomAttributes(false);
        foreach (var attr in userAttrs)
        {
            if (attr is ValidatorAttribute<EmailValidator> va)
                Console.WriteLine($"  UserModel 验证器: {va.ValidatorType.Name}");
        }

        var contactAttrs = typeof(ContactModel)
            .GetCustomAttributes(false);
        foreach (var attr in contactAttrs)
        {
            if (attr is SerializerAttribute<JsonSerializer2> sa)
                Console.WriteLine($"  ContactModel 序列化器: {sa.SerializerType.Name}, 格式: {sa.Format}");
        }

        // ApiController 方法上的泛型特性
        foreach (var method in typeof(ApiController).GetMethods())
        {
            foreach (var attr in method.GetCustomAttributes(false))
            {
                if (attr is SerializerAttribute<JsonSerializer2> ja)
                    Console.WriteLine($"  {method.Name}() -> JSON 序列化器");
                if (attr is SerializerAttribute<XmlSerializer2> xa)
                    Console.WriteLine($"  {method.Name}() -> XML 序列化器");
            }
        }

        Console.WriteLine();
        Console.WriteLine("  C# 11 前: [OldValidator(typeof(EmailValidator))]");
        Console.WriteLine("  C# 11 起: [ValidatorAttribute<EmailValidator>] 类型安全，IDE 友好");
    }
}
