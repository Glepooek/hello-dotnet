using System;
using System.Runtime.Serialization;

namespace CsharplangDemo73.Demos
{
    // ── C# 7.3: [field: 特性] 直接标注自动属性的后备字段 ─────────────────
    // C# 7.3 之前: 若需标注后备字段，必须手动展开为字段 + 属性
    // C# 7.3 起: 可以直接在自动属性上用 [field: 特性名] 标注后备字段

    [DataContract]
    public class UserProfile
    {
        // [field: NonSerialized] 标注后备字段为不序列化
        [field: NonSerialized]
        public string CachedDisplayName { get; set; } = "";

        // [field: DataMember] 标注后备字段参与序列化并指定名称
        [field: DataMember(Name = "user_name")]
        public string UserName { get; set; } = "";

        [field: DataMember(Name = "user_age")]
        public int Age { get; set; }

        // 普通属性（无字段特性）对比
        public string Email { get; set; } = "";
    }

    // ── 对比: C# 7.2 前必须手动展开 ──────────────────────────────────────
    [DataContract]
    public class OldUserProfile
    {
        // 旧写法: 手动声明字段 + 属性
        [NonSerialized]
        private string _cachedDisplayName = "";
        public string CachedDisplayName
        {
            get => _cachedDisplayName;
            set => _cachedDisplayName = value;
        }

        [DataMember(Name = "user_name")]
        private string _userName = "";
        public string UserName
        {
            get => _userName;
            set => _userName = value;
        }
    }

    // ── 自定义特性演示 ─────────────────────────────────────────────────────
    [AttributeUsage(AttributeTargets.Field)]
    public class AuditAttribute : Attribute
    {
        public AuditAttribute(string description) { Description = description; }
        public string Description { get; }
    }

    public class Order
    {
        [field: Audit("订单ID不可修改")]
        public string OrderId { get; set; } = "";

        [field: Audit("金额变更需记录日志")]
        public decimal Amount { get; set; }

        public string Status { get; set; } = "pending";
    }

    public static class BackingFieldAttributeDemo
    {
        public static void Run()
        {
            var user = new UserProfile
            {
                UserName = "alice",
                Age = 30,
                Email = "alice@example.com",
                CachedDisplayName = "Alice (缓存)"
            };

            Console.WriteLine("  UserProfile:");
            Console.WriteLine("    UserName: " + user.UserName);
            Console.WriteLine("    Age:      " + user.Age);
            Console.WriteLine("    Email:    " + user.Email);

            // 通过反射查看后备字段特性
            var type = typeof(UserProfile);
            foreach (var prop in type.GetProperties())
            {
                // 获取对应的后备字段（编译器生成的 <PropertyName>k__BackingField）
                var backingField = type.GetField(
                    "<" + prop.Name + ">k__BackingField",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                if (backingField != null)
                {
                    var attrs = backingField.GetCustomAttributes(false);
                    if (attrs.Length > 0)
                    {
                        Console.Write("    " + prop.Name + " 后备字段特性: ");
                        foreach (var attr in attrs)
                            Console.Write(attr.GetType().Name + " ");
                        Console.WriteLine();
                    }
                }
            }

            // Order 演示
            var order = new Order { OrderId = "ORD-001", Amount = 299.99m };
            Console.WriteLine("  Order: " + order.OrderId + ", " + order.Amount);

            Console.WriteLine();
            Console.WriteLine("  C# 7.3 之前: 需手动展开为字段 + 属性才能标注字段特性");
            Console.WriteLine("  C# 7.3 起: [field: 特性] 直接标注自动属性的后备字段");
        }
    }
}
