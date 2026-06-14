using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CsharplangDemo60.Demos
{
    public static class NameofDemo
    {
        // ── C# 6.0: nameof 运算符 ────────────────────────────────────────
        // 返回变量、类型、成员的名称字符串，编译期求值
        // 重命名时编译器自动更新，消除硬编码字符串的重构风险

        // 1. 参数验证（最常见用途）
        static void ValidateAge(int age)
        {
            if (age < 0 || age > 150)
                throw new ArgumentOutOfRangeException(nameof(age), nameof(age) + " 必须在 0-150 之间");
        }

        static string GetDisplay(string name, int age)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (age < 0)
                throw new ArgumentOutOfRangeException(nameof(age));
            return $"{name}: {age}";
        }

        // 2. INotifyPropertyChanged 实现（最重要的实际场景）
        class ViewModel : INotifyPropertyChanged
        {
            private string _title;
            private int _count;

            public event PropertyChangedEventHandler PropertyChanged;

            public string Title
            {
                get { return _title; }
                set
                {
                    _title = value;
                    // C# 6.0 前: if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Title"));
                    // C# 6.0 起: ?. + nameof — ?. 保证线程安全，nameof 保证重命名时编译器检查
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
                }
            }

            public int Count
            {
                get { return _count; }
                set
                {
                    _count = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
                }
            }
        }

        // 3. nameof 的各种用途
        static void DemoNameofTargets()
        {
            // 变量
            int myVariable = 42;
            Console.WriteLine("  变量名: " + nameof(myVariable));

            // 类型
            Console.WriteLine("  类型名: " + nameof(ViewModel));
            Console.WriteLine("  类型名: " + nameof(System.Collections.Generic.List<int>)); // "List"

            // 成员（方法、属性）
            Console.WriteLine("  方法名: " + nameof(Console.WriteLine));
            Console.WriteLine("  属性名: " + nameof(ViewModel.Title));

            // 接口
            Console.WriteLine("  接口名: " + nameof(INotifyPropertyChanged));

            // 枚举
            Console.WriteLine("  枚举值: " + nameof(DayOfWeek.Monday));
        }

        public static void Run()
        {
            // 参数验证
            Console.WriteLine("  参数验证:");
            try { ValidateAge(-1); }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("    捕获: " + e.ParamName + " — " + e.Message.Split('\n')[0]);
            }
            Console.WriteLine("    GetDisplay: " + GetDisplay("Alice", 30));

            // INPC 演示
            Console.WriteLine("  INotifyPropertyChanged:");
            var vm = new ViewModel();
            vm.PropertyChanged += (s, e) =>
                Console.WriteLine("    PropertyChanged: " + e.PropertyName);
            vm.Title = "新标题";
            vm.Count = 5;

            // nameof 的各种目标
            Console.WriteLine("  nameof 各种目标:");
            DemoNameofTargets();

            Console.WriteLine();
            Console.WriteLine("  nameof 核心价值: 编译期字符串，重命名时编译器自动检查");
            Console.WriteLine("  常见场景: ArgumentException、INotifyPropertyChanged、日志");
        }
    }
}
