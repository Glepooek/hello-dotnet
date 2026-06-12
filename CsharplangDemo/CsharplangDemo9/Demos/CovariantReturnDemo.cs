using System;

namespace CsharplangDemo9.Demos
{
    // ── 协变返回类型: 重写方法可返回比基类更具体的派生类型 ─────────────────
    public class Animal
    {
        public string Name { get; init; } = "Animal";
        public virtual Animal Clone() => new Animal { Name = this.Name };
        public virtual Animal Create(string name) => new Animal { Name = name };
        public override string ToString() => $"{GetType().Name}({Name})";
    }

    public class Dog : Animal
    {
        public string Breed { get; init; } = "Unknown";

        // C# 9 起: 协变返回，直接返回 Dog，无需强转
        public override Dog Clone() =>
            new Dog { Name = this.Name, Breed = this.Breed };

        public override Dog Create(string name) =>
            new Dog { Name = name, Breed = "Mixed" };

        public override string ToString() => $"Dog({Name}, Breed={Breed})";
    }

    public class GoldenRetriever : Dog
    {
        public override GoldenRetriever Clone() =>
            new GoldenRetriever { Name = this.Name, Breed = "Golden Retriever" };
    }

    public abstract class Builder
    {
        public abstract Builder WithName(string name);
        protected string _name = "";
        public abstract string Build();
    }

    public class SqlBuilder : Builder
    {
        private string _table = "";

        public override SqlBuilder WithName(string name) { _name = name; return this; }
        public SqlBuilder WithTable(string t) { _table = t; return this; }
        public override string Build() => $"SELECT * FROM {_table} WHERE name='{_name}'";
    }

    public static class CovariantReturnDemo
    {
        public static void Run()
        {
            var dog = new Dog { Name = "Rex", Breed = "Labrador" };
            Dog cloned = dog.Clone();
            Console.WriteLine($"  原始: {dog}");
            Console.WriteLine($"  克隆: {cloned}");

            var golden = new GoldenRetriever { Name = "Buddy" };
            GoldenRetriever goldenClone = golden.Clone();
            Console.WriteLine($"  GoldenRetriever 克隆: {goldenClone}");

            Animal animalRef = dog;
            Animal result = animalRef.Clone();
            Console.WriteLine($"  通过基类引用克隆: {result.GetType().Name}");

            var query = new SqlBuilder()
                .WithTable("users")
                .WithName("Alice")
                .Build();
            Console.WriteLine($"  SQL: {query}");

            Console.WriteLine();
            Console.WriteLine("  协变返回类型消除了向下强转:");
            Console.WriteLine("    C# 9 前: Dog d = (Dog)animal.Clone()");
            Console.WriteLine("    C# 9 起: Dog d = dog.Clone()");
        }
    }
}
