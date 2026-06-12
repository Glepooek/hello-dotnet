using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CsharplangDemo71.Demos
{
    public static class AsyncMainDemo
    {
        // ── C# 7.0 前: 控制台程序无法直接 await，必须阻塞 ───────────────
        // 旧写法 1: .GetAwaiter().GetResult()（阻塞线程）
        static void OldWay()
        {
            string result = FetchDataAsync().GetAwaiter().GetResult();
            Console.WriteLine("  旧写法(阻塞): " + result);
        }

        // ── C# 7.1 起: async Main 四种合法签名 ───────────────────────────
        // static async Task Main(string[] args)     ← 最常用
        // static async Task Main()
        // static async Task<int> Main(string[] args) ← 有退出码
        // static async Task<int> Main()
        //
        // Program.cs 中的 Main 已经是 async Task，此处演示 async 方法调用

        static async Task<string> FetchDataAsync()
        {
            await Task.Delay(10);  // 模拟异步操作（如 HTTP 请求）
            return "async 数据已获取";
        }

        static async Task<int[]> GenerateAsync(int count)
        {
            await Task.Delay(5);
            int[] result = new int[count];
            for (int i = 0; i < count; i++) result[i] = i * i;
            return result;
        }

        public static void Run()
        {
            Console.WriteLine("  async Main 支持四种签名:");
            Console.WriteLine("    static async Task Main(string[] args)  ← 最常用");
            Console.WriteLine("    static async Task Main()");
            Console.WriteLine("    static async Task<int> Main(string[] args) ← 有退出码");
            Console.WriteLine("    static async Task<int> Main()");
            Console.WriteLine();
            Console.WriteLine("  C# 7.0 前: 必须 FetchDataAsync().GetAwaiter().GetResult()");
            Console.WriteLine("  C# 7.1 起: 直接在 Main 中 await（见下方 RunAsync）");

            // 旧方式演示
            OldWay();
        }

        public static async Task RunAsync()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n── 5. async Main 实际调用演示 ──");
            Console.ResetColor();

            // 直接 await，无阻塞
            string data = await FetchDataAsync();
            Console.WriteLine("  await FetchDataAsync: " + data);

            int[] squares = await GenerateAsync(5);
            Console.Write("  await GenerateAsync(5): [");
            for (int i = 0; i < squares.Length; i++)
                Console.Write(squares[i] + (i < squares.Length - 1 ? ", " : ""));
            Console.WriteLine("]");

            // 并行异步
            Task<string> t1 = FetchDataAsync();
            Task<int[]>  t2 = GenerateAsync(3);
            await Task.WhenAll(t1, t2);
            Console.WriteLine("  并行完成: " + t1.Result + " | squares=[" +
                              string.Join(", ", t2.Result) + "]");
        }
    }
}
