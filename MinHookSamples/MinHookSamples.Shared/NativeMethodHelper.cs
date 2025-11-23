using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MinHookSamples.Shared
{
    public class NativeMethodHelper
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);
    }
}
