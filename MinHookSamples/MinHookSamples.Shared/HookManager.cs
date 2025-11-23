using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MinHookSamples.Shared
{
    public class HookManager
    {
        /// <summary>
        /// 原始函数的指针
        /// </summary>
        public static IntPtr OriginalFunction = IntPtr.Zero;
        /// <summary>
        /// 当前钩子的目标函数地址
        /// </summary>
        private static IntPtr _targetFunction = IntPtr.Zero;

        public static void InstallHook(string moduleName, string functionName, Delegate detourFunc)
        {
            // 1. 初始化 MinHook
            MinHook.MH_STATUS status = MinHook.MH_Initialize();
            if (status != MinHook.MH_STATUS.MH_OK)
            {
                Console.WriteLine($"MH_Initialize failed: {status}");
                return;
            }

            // 2. 获取目标函数的地址
            _targetFunction = MinHook.GetProcAddress(MinHook.GetModuleHandle(moduleName), functionName);
            if (_targetFunction == IntPtr.Zero)
            {
                Console.WriteLine($"Failed to get {functionName} address");
                return;
            }

            // 3. 创建钩子
            var detourPtr = Marshal.GetFunctionPointerForDelegate(detourFunc);
            status = MinHook.MH_CreateHook(_targetFunction, detourPtr, out OriginalFunction);
            if (status != MinHook.MH_STATUS.MH_OK)
            {
                Console.WriteLine($"MH_CreateHook failed: {status}");
                return;
            }

            // 4. 启用钩子
            status = MinHook.MH_EnableHook(_targetFunction);
            if (status != MinHook.MH_STATUS.MH_OK)
            {
                Console.WriteLine($"MH_EnableHook failed: {status}");
                return;
            }

            Console.WriteLine($"{functionName} hook installed successfully");
        }

        public static void UninstallHook()
        {
            if (_targetFunction == IntPtr.Zero)
            {
                Console.WriteLine("No active hook to uninstall");
                return;
            }

            // 1. 禁用钩子
            var status = MinHook.MH_DisableHook(_targetFunction);
            if (status != MinHook.MH_STATUS.MH_OK)
            {
                Console.WriteLine($"MH_DisableHook failed: {status}");
            }

            // 2. 移除钩子
            status = MinHook.MH_RemoveHook(_targetFunction);
            if (status != MinHook.MH_STATUS.MH_OK)
            {
                Console.WriteLine($"MH_RemoveHook failed: {status}");
            }

            // 3. 卸载 MinHook
            status = MinHook.MH_Uninitialize();
            if (status != MinHook.MH_STATUS.MH_OK)
            {
                Console.WriteLine($"MH_Uninitialize failed: {status}");
            }

            _targetFunction = IntPtr.Zero;
            OriginalFunction = IntPtr.Zero;

            Console.WriteLine("Hook uninstalled successfully");
        }
    }
}
