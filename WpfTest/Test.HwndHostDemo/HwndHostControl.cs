using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace Test.HwndHostDemo;

internal class HwndHostControl : HwndHost
{
    /// <summary>
    /// 扩展样式：鼠标事件穿透，不拦截输入
    /// </summary>
    private const uint WS_EX_TRANSPARENT = 0x00000020u;
    /// <summary>
    /// 窗口样式：子窗口，依附于父窗口
    /// </summary>
    private const uint WS_CHILD = 0x40000000u;
    /// <summary>
    /// 窗口样式：创建后立即可见
    /// </summary>
    private const uint WS_VISIBLE = 0x10000000u;

    public string TargetProcessName { get; set; } = "有道云笔记";

    private IntPtr _childHwnd;

    protected override HandleRef BuildWindowCore(HandleRef hwndParent)
    {
        // 创建Win32静态文本窗口（类名"STATIC"是系统预定义的）
        var parentHwnd = CreateWindowExW(WS_EX_TRANSPARENT, "static", null, WS_CHILD | WS_VISIBLE, 0, 0, 0, 0, hwndParent.Handle, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

        // 检查是否存在目标进程
        var noteProcess = Process.GetProcessesByName(TargetProcessName).FirstOrDefault();
        if (noteProcess == null)
        {
            Debug.WriteLine($"Process '{TargetProcessName}' not found.");
            return new HandleRef(null, parentHwnd);
        }

        // 获取目标进程的主窗口句柄
        _childHwnd = noteProcess.MainWindowHandle;
        if (_childHwnd == IntPtr.Zero)
        {
            Debug.WriteLine($"Failed to get main window handle for '{TargetProcessName}'.");
            return new HandleRef(null, parentHwnd);
        }

        // 设置目标窗口为子窗口
        SetParent(_childHwnd, parentHwnd);

        return new HandleRef(null, parentHwnd);
    }

    protected override void DestroyWindowCore(HandleRef hwnd)
    {
        if (_childHwnd != IntPtr.Zero)
        {
            // 将子窗口从父窗口中分离
            SetParent(_childHwnd, IntPtr.Zero);
            _childHwnd = IntPtr.Zero;
        }

        if (hwnd.Handle != IntPtr.Zero)
        {
            // 销毁Win32窗口
            DestroyWindow(hwnd.Handle);
        }
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        base.OnRenderSizeChanged(sizeInfo);
        if (Handle != IntPtr.Zero && _childHwnd != IntPtr.Zero)
        {
            // 更新子窗口的尺寸和位置
            MoveWindow(_childHwnd, 0, 0, (int)ActualWidth, (int)ActualHeight, true);
        }
    }

    #region Win32 API

    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr CreateWindowExW(
        uint dwExStyle,
        [MarshalAs(UnmanagedType.LPWStr)] string lpClassName,
        [MarshalAs(UnmanagedType.LPWStr)] string lpWindowName,
        uint dwStyle,
        int x,
        int y,
        int nWidth,
        int nHeight,
        IntPtr hWndParent,
        IntPtr hMenu,
        IntPtr hInstance,
        [MarshalAs(UnmanagedType.AsAny)] object pvParam);

    /// <summary>
    /// 设置窗口的父窗口（改变所有权与 Z 序关系）。返回旧父窗口句柄。
    /// </summary>
    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

    /// <summary>
    /// 销毁窗口，释放系统资源。销毁后句柄变为无效。
    /// </summary>
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(IntPtr hwnd);

    /// <summary>
    /// 移动并调整窗口大小，可选是否重绘（bRepaint）。
    /// </summary>
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    #endregion
}
