using System;

namespace CsharplangDemo70.Demos
{
    public static class ExpressionBodyDemo
    {
        // ── C# 6.0: 方法和属性可以用表达式主体 ─────────────────────────
        // ── C# 7.0: 扩展到构造函数、析构函数、属性 get/set ──────────────

        class Person
        {
            private string _name;
            private int _age;

            // C# 7.0: 构造函数表达式主体
            public Person(string name, int age)
                => (_name, _age) = (name, age);

            // C# 7.0: 析构函数表达式主体
            ~Person() => Console.WriteLine("  ~Person() 析构");

            // C# 7.0: 属性 getter 和 setter 各自的表达式主体
            public string Name
            {
                get => _name;
                set => _name = value ?? throw new ArgumentNullException(nameof(value));
            }

            public int Age
            {
                get => _age;
                set => _age = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof(value));
            }

            // C# 6.0 已有: 只读属性表达式主体
            public string Display => _name + " (age=" + _age + ")";

            // C# 6.0 已有: 方法表达式主体
            public override string ToString() => Display;
        }

        // ── 索引器表达式主体（C# 7.0 扩展）──────────────────────────────
        class WeeklySchedule
        {
            private string[] _days = new string[7];

            // C# 7.0: 索引器 get/set 表达式主体
            public string this[int day]
            {
                get => _days[day % 7];
                set => _days[day % 7] = value;
            }

            public string this[DayOfWeek day]
            {
                get => _days[(int)day];
                set => _days[(int)day] = value;
            }
        }

        public static void Run()
        {
            // 构造函数表达式主体
            var p = new Person("Alice", 30);
            Console.WriteLine("  " + p);

            // 属性 setter 表达式主体（带验证）
            p.Name = "Bob";
            p.Age = 25;
            Console.WriteLine("  修改后: " + p);

            // 验证 setter 中的 throw 表达式
            try { p.Age = -1; }
            catch (ArgumentOutOfRangeException) { Console.WriteLine("  Age=-1 抛出异常"); }

            // 索引器表达式主体
            var schedule = new WeeklySchedule();
            schedule[DayOfWeek.Monday] = "工作";
            schedule[DayOfWeek.Saturday] = "休息";
            schedule[DayOfWeek.Sunday] = "休息";
            Console.WriteLine("  周一: " + schedule[DayOfWeek.Monday]);
            Console.WriteLine("  周六: " + schedule[DayOfWeek.Saturday]);

            Console.WriteLine();
            Console.WriteLine("  C# 7.0 扩展表达式主体到:");
            Console.WriteLine("    构造函数、析构函数、属性 get/set、索引器 get/set");
            Console.WriteLine("  C# 6.0 已支持: 方法、只读属性、运算符");
        }
    }
}
