using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace Test.HwndHostDemo;

internal class HwndHostControl : HwndHost
{
    /// <summary>
    /// Win32窗口句柄
    /// </summary>
    private IntPtr _parentHwnd;
    private IntPtr _childHwnd;

    protected override HandleRef BuildWindowCore(HandleRef hwndParent)
    {
        // 创建Win32静态文本窗口（类名"STATIC"是系统预定义的）
        _parentHwnd = CreateWindowExW(0x00000020u, "static", null, 0x40000000u | 0x10000000u, 0, 0, 0, 0, hwndParent.Handle, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

        // 检查是否存在 有道云笔记 进程
        var noteProcess = Process.GetProcessesByName("有道云笔记").FirstOrDefault();
        if (noteProcess == null)
        {
            Debug.WriteLine("Notepad process not found. Please start Notepad before using this control.");
            return new HandleRef(null, _parentHwnd);
        }

        // 获取 有道云笔记 的主窗口句柄
        _childHwnd = noteProcess.MainWindowHandle;
        if (_childHwnd == IntPtr.Zero)
        {
            Debug.WriteLine("Failed to get Notepad main window handle.");
            return new HandleRef(null, _parentHwnd);
        }

        // 设置 有道云笔记 窗口为子窗口
        SetParent(_childHwnd, _parentHwnd);

        return new HandleRef(null, _parentHwnd);
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
        if (_parentHwnd != IntPtr.Zero && _childHwnd != IntPtr.Zero)
        {
            // 更新子窗口的尺寸和位置
            MoveWindow(_childHwnd, 0, 0, (int)ActualWidth, (int)ActualHeight, true);
        }
    }

    #region Win32 API

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern IntPtr CreateWindowEx(
        int exStyle, string className, string windowName,
        int style, int x, int y, int width, int height,
        IntPtr parent, IntPtr menu, IntPtr hInstance, IntPtr lpParam);

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

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern bool SetWindowText(IntPtr hWnd, string text);

    #endregion
}
