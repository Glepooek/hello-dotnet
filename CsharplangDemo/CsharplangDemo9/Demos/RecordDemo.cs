using System;
using System.Collections.Generic;

namespace CsharplangDemo9.Demos
{
    // ── 位置式 record: 编译器自动生成属性/构造函数/相等/ToString/解构 ────
    record Person(string Name, int Age);

    // 带继承的 record
    record Employee(string Name, int Age, string Department) : Person(Name, Age);

    // 非位置式 record: 手动控制属性
    record Address
    {
        public string City    { get; init; } = "";
        public string Country { get; init; } = "";
        public override string ToString() => $"{City}, {Country}";
    }

    // C# 9: record (引用类型, 不写 class 关键字)
    // 注: record class 语法是 C# 10 引入的显式写法
    record Point(double X, double Y)
    {
        public double Distance => Math.Sqrt(X * X + Y * Y);
    }

    public static class RecordDemo
    {
        public static void Run()
        {
            // ── 值相等 (record 的核心特性) ────────────────────────────────
            var p1 = new Person("Alice", 30);
            var p2 = new Person("Alice", 30);
            var p3 = new Person("Bob",   25);

            Console.WriteLine($"  p1: {p1}");
            Console.WriteLine($"  p1 == p2 (同值): {p1 == p2}");   // true
            Console.WriteLine($"  p1 == p3 (异值): {p1 == p3}");   // false
            Console.WriteLine($"  ReferenceEquals: {ReferenceEquals(p1, p2)}"); // false

            // ── with 表达式: 非破坏性修改 ────────────────────────────────
            var senior = p1 with { Age = 60 };
            Console.WriteLine($"  p1 with Age=60: {senior}");
            Console.WriteLine($"  p1 不变:        {p1}");

            // ── 解构 ─────────────────────────────────────────────────────
            var (name, age) = p1;
            Console.WriteLine($"  解构: name={name}, age={age}");

            // ── 继承 ──────────────────────────────────────────────────────
            var emp = new Employee("Carol", 28, "Engineering");
            Console.WriteLine($"  Employee: {emp}");
            var transferred = emp with { Department = "Product" };
            Console.WriteLine($"  调部门:   {transferred}");

            // ── 非位置式 record ───────────────────────────────────────────
            var addr  = new Address { City = "Beijing", Country = "China" };
            var addr2 = addr with { City = "Shanghai" };
            Console.WriteLine($"  Address: {addr}  →  {addr2}");

            // ── Point 记录 ────────────────────────────────────────────────
            var origin = new Point(3.0, 4.0);
            Console.WriteLine($"  Point: {origin}, Distance={origin.Distance:F2}");
        }
    }
}
