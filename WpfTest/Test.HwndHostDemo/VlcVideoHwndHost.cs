using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace Test.HwndHostDemo;

/// <summary>
/// 为 VLC 提供 Win32 窗口句柄的 HwndHost 容器
/// </summary>
public class VlcVideoHwndHost : HwndHost
{
    // Win32 API 声明
    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr CreateWindowEx(
        int dwExStyle,
        string lpClassName,
        string lpWindowName,
        int dwStyle,
        int x, int y, int nWidth, int nHeight,
        IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

    [DllImport("user32.dll")]
    private static extern bool DestroyWindow(IntPtr hWnd);

    // 保存当前 Win32 窗口句柄
    public IntPtr VideoHandle { get; private set; }

    // 窗口样式：子窗口 + 可见
    private const int WS_CHILD = 0x40000000;
    private const int WS_VISIBLE = 0x10000000;

    /// <summary>
    /// 创建 Win32 子窗口（必须重写）
    /// </summary>
    protected override HandleRef BuildWindowCore(HandleRef hwndParent)
    {
        // 创建空白静态窗口，作为 VLC 渲染画布
        VideoHandle = CreateWindowEx(
            0,
            "STATIC", // 系统预定义窗口类
            string.Empty,
            WS_CHILD | WS_VISIBLE,
            0, 0, 0, 0,
            hwndParent.Handle,
            IntPtr.Zero,
            IntPtr.Zero,
            IntPtr.Zero);

        return new HandleRef(this, VideoHandle);
    }

    /// <summary>
    /// 销毁 Win32 窗口（必须重写，防止内存泄漏）
    /// </summary>
    protected override void DestroyWindowCore(HandleRef hwnd)
    {
        if (VideoHandle != IntPtr.Zero)
        {
            DestroyWindow(VideoHandle);
            VideoHandle = IntPtr.Zero;
        }
    }
}
