using System;

namespace CsharplangDemo72.Demos
{
    // ── private protected: 同程序集内的派生类可访问 ───────────────────────
    // C# 7.2 新增第六种访问级别
    //
    // 访问级别从窄到宽:
    //   private           — 仅当前类
    //   private protected — 同程序集内的派生类（C# 7.2 新增）
    //   protected         — 任意程序集的派生类
    //   internal          — 同程序集的任意代码
    //   protected internal— 同程序集或任意程序集的派生类
    //   public            — 所有代码

    public class BaseComponent
    {
        // private: 仅当前类
#pragma warning disable CS0414
        private int _privateField = 1;
#pragma warning restore CS0414

        // private protected: 同程序集内的派生类（C# 7.2）
        private protected int InternalState = 42;
        private protected void InternalLog(string msg)
            => Console.WriteLine("  [InternalLog] " + msg);

        // protected: 任意程序集的派生类
        protected int SharedState = 100;

        // internal: 同程序集所有代码
        internal string ComponentName = "Base";

        // public: 所有代码
        public void PublicMethod()
            => Console.WriteLine("  PublicMethod: InternalState=" + InternalState);
    }

    // 同程序集内的派生类: 可访问 private protected
    public class DerivedComponent : BaseComponent
    {
        public void ShowInternalState()
        {
            Console.WriteLine("  DerivedComponent.InternalState = " + InternalState);  // ✅
            InternalLog("从派生类调用 private protected 方法");                         // ✅
            Console.WriteLine("  SharedState = " + SharedState);                        // ✅ protected
        }

        public void ModifyInternal()
        {
            InternalState = 99;  // ✅ 同程序集派生类可修改
            Console.WriteLine("  修改后 InternalState = " + InternalState);
        }
    }

    // 同程序集的非派生类: 不能访问 private protected
    class SameAssemblyNonDerived
    {
        void Method(BaseComponent b)
        {
            // b.InternalState;  // 编译错误: 非派生类不能访问
            Console.WriteLine("  b.ComponentName = " + b.ComponentName);  // ✅ internal
        }
    }

    public static class PrivateProtectedDemo
    {
        public static void Run()
        {
            var derived = new DerivedComponent();

            Console.WriteLine("  派生类访问 private protected:");
            derived.ShowInternalState();

            Console.WriteLine("  修改 private protected 字段:");
            derived.ModifyInternal();

            derived.PublicMethod();

            Console.WriteLine();
            Console.WriteLine("  访问级别对比:");
            Console.WriteLine("    private           = 只有本类");
            Console.WriteLine("    private protected = 同程序集的派生类 (C# 7.2)");
            Console.WriteLine("    protected         = 任意程序集的派生类");
            Console.WriteLine("    internal          = 同程序集所有代码");
            Console.WriteLine("    protected internal= 同程序集 OR 任意派生类");
            Console.WriteLine("    public            = 所有代码");

            Console.WriteLine();
            Console.WriteLine("  private protected 的典型用途:");
            Console.WriteLine("    库作者: 允许同库内的子类访问，阻止外部库的子类访问");
            Console.WriteLine("    比 protected 更严格，比 private 更灵活");
        }
    }
}
