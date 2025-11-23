using MinHookSamples.Shared;
using System.Runtime.InteropServices;

// 在 Win32 API 层面：
// MessageBoxW 是 Windows 导出的 Unicode 版本函数（参数类型为 LPCWSTR，也就是 UTF‑16 / wchar_t*）。
// MessageBoxA 是 ANSI 版本（参数为 LPCSTR，按当前代码页编码的字节串）。
// 在 C/C++ 头文件里通常有一个宏 MessageBox，它根据是否定义 UNICODE 映射到 MessageBoxW（Unicode）或 MessageBoxA（ANSI）。

// 安装钩子
HookManager.InstallHook("user32.dll", "MessageBoxW",
    (hWnd, text, caption, type) =>
    {
        Console.WriteLine($"我已成功拦截到 MessageBox：内容 {text}, 标题: {caption}");

        HookManager.MessageBoxDelegate? original = Marshal.GetDelegateForFunctionPointer<HookManager.MessageBoxDelegate>(HookManager.OriginalFunction);

        return original(hWnd, text, caption, type);
    });

// 测试 MessageBox 调用（钩子会捕获这个）
NativeMethodHelper.MessageBox(IntPtr.Zero, "This is a test", "Test", 0);

// 卸载钩子
HookManager.UninstallHook();

Console.ReadLine();