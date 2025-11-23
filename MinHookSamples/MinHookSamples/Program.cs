using MinHookSamples.Shared;
using System.Runtime.InteropServices;

// 定义 MessageBox 的委托
[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
public delegate int MessageBoxDelegate(IntPtr hWnd, string text, string caption, uint type);

// SendMessageW 的原始函数签名
[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
public delegate IntPtr SendMessageWDelegate(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);


public class Program
{
    public static void Main(string[] args)
    {
        // 在 Win32 API 层面：
        // MessageBoxW 是 Windows 导出的 Unicode 版本函数（参数类型为 LPCWSTR，也就是 UTF‑16 / wchar_t*）。
        // MessageBoxA 是 ANSI 版本（参数为 LPCSTR，按当前代码页编码的字节串）。
        // 在 C/C++ 头文件里通常有一个宏 MessageBox，它根据是否定义 UNICODE 映射到 MessageBoxW（Unicode）或 MessageBoxA（ANSI）。

        MessageBoxDelegate messageBoxDl = new MessageBoxDelegate((hWnd, text, caption, type) =>
        {
            Console.WriteLine($"我已成功拦截到 MessageBox：内容 {text}, 标题: {caption}");
            MessageBoxDelegate original = Marshal.GetDelegateForFunctionPointer<MessageBoxDelegate>(HookManager.OriginalFunction);
            return original(hWnd, text, caption, type);
        });

        SendMessageWDelegate sendMessageDl = new SendMessageWDelegate((IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam) =>
        {
            Console.WriteLine($"[HOOK] SendMessageW: hWnd=0x{hWnd.ToInt64():X}, Msg=0x{Msg:X}");

            SendMessageWDelegate? original = Marshal.GetDelegateForFunctionPointer<SendMessageWDelegate>(HookManager.OriginalFunction);
            // 获取窗口所属的线程和进程ID
            uint processId = 0;
            uint threadId = NativeMethodHelper.GetWindowThreadProcessId(hWnd, out processId);

            // 使用 System.Diagnostics.Process 获取进程信息
            string processName = "Unknown";
            try
            {
                var targetProcess = System.Diagnostics.Process.GetProcessById((int)processId);
                processName = targetProcess.ProcessName;

                Console.WriteLine($"Window belongs to - ThreadID: {threadId}, ProcessID: {processId}, ProcessName: {processName}");

                //定时检测代码：如果超时自动抓取dump
                Task.Run(() =>
                {
                    Thread.Sleep(3000);

                    if (Msg == 0x0010)
                    {
                        string dumpPath = Path.Combine(Environment.CurrentDirectory, $"ProcessDump_{processName}_{DateTime.Now:yyyyMMddHHmmss}.dmp");

                        DumpGen.GenerateProcessDump(targetProcess.Id, dumpPath);

                        Console.WriteLine($"Launching ProcDump to generate dump: {dumpPath}");
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // 调用原始函数
            return original(hWnd, Msg, wParam, lParam);
        });

        #region 测试SendMessage
        //// 安装 Hook
        //HookManager.InstallHook("user32.dll", "SendMessageW", sendMessageDl);

        //// 测试：发送 WM_CLOSE 消息（会触发 Hook）
        //IntPtr hWnd = NativeMethodHelper.FindWindow(null, "wpf");

        //if (hWnd != IntPtr.Zero)
        //{
        //    NativeMethodHelper.SendMessage(hWnd, NativeMethodHelper.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        //    Console.WriteLine("Sent WM_CLOSE to target window.");
        //}
        //else
        //{
        //    Console.WriteLine("Target window not found.");
        //}
        #endregion

        #region 测试MessageBox
        HookManager.InstallHook("user32.dll", "MessageBoxW", messageBoxDl);
        NativeMethodHelper.MessageBox(IntPtr.Zero, "This is a test", "Test", 0);
        #endregion

        Console.ReadKey();

        // 卸载 Hook
        HookManager.UninstallHook();
    }
}