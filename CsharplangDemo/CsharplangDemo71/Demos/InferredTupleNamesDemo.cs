using System;
using System.Linq;

namespace CsharplangDemo71.Demos
{
    public static class InferredTupleNamesDemo
    {
        // ── C# 7.1: 从变量/属性名自动推断元组元素名称 ────────────────────
        // C# 7.0: 必须显式写 (Name: name, Age: age)
        // C# 7.1: 若字段名与变量名相同，可以省略名称标签

        class Person
        {
            public string Name { get; }
            public int Age { get; }
            public string City { get; }

            public Person(string name, int age, string city)
            {
                Name = name;
                Age = age;
                City = city;
            }
        }

        static (string Name, int Count, double Avg) ComputeStats(int[] data)
        {
            double sum = 0;
            foreach (int n in data)
            {
                sum += n;
            }

            return ("数组", data.Length, sum / data.Length);
        }

        public static void Run()
        {
            string name = "Alice";
            int age = 30;
            double score = 95.5;

            // ── C# 7.0 写法: 必须显式命名 ────────────────────────────
            var oldTuple = (Name: name, Age: age, Score: score);
            Console.WriteLine("  旧写法 (显式): " + oldTuple.Name + ", " + oldTuple.Age);

            // ── C# 7.1 写法: 从变量名推断元组元素名称 ────────────────
            var inferred = (name, age, score);   // 元素名自动推断为 name, age, score
            Console.WriteLine("  推断 name:  " + inferred.name);
            Console.WriteLine("  推断 age:   " + inferred.age);
            Console.WriteLine("  推断 score: " + inferred.score);

            // ── 从属性名推断 ──────────────────────────────────────────
            var p = new Person("Bob", 25, "Beijing");
            var personTuple = (p.Name, p.Age, p.City);  // 推断为 Name, Age, City
            Console.WriteLine("  属性推断: " + personTuple.Name + ", " +
                              personTuple.Age + ", " + personTuple.City);

            // ── 混合: 部分推断，部分显式 ─────────────────────────────
            int x = 10, y = 20;
            var mixed = (x, y, Label: "坐标");  // x, y 推断; Label 显式
            Console.WriteLine("  混合: x=" + mixed.x + " y=" + mixed.y +
                              " Label=" + mixed.Label);

            // ── 推断名称可用于相等比较（== 是 C# 7.3 特性，7.1 用 Equals）──
            var t1 = (name, age);
            var t2 = (name, age);
            Console.WriteLine("  Equals: " + t1.Equals(t2));  // C# 7.1 兼容写法

            // ── 在 LINQ 投影中的应用 ──────────────────────────────────
            // Select 投影成元组: n 推断为元素名，Length 显式命名
            string[] names = { "Alice", "Bob", "Carol" };
            var projected = names.Select(n => (n, Length: n.Length));
            Console.Write("  长度投影: ");
            foreach (var item in projected)
                Console.Write(item.n + "=" + item.Length + " ");
            Console.WriteLine();

            // ── ComputeStats 演示 ─────────────────────────────────────
            int[] data = { 3, 1, 4, 1, 5, 9, 2 };
            var stats = ComputeStats(data);
            Console.WriteLine("  Stats: " + stats.Name + " count=" + stats.Count +
                              " avg=" + stats.Avg.ToString("F2"));

            Console.WriteLine();
            Console.WriteLine("  C# 7.0: var t = (Name: name, Age: age) — 必须显式写名称");
            Console.WriteLine("  C# 7.1: var t = (name, age)            — 自动推断为 name/age");
        }
    }
}
