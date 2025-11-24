using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using CommunityToolkit.Diagnostics;

namespace MinHookSamples.Shared
{
    public class HookManager
    {
        // Keep managed references to the delegates to prevent them from being GC'd
        private static readonly Dictionary<IntPtr, Delegate> _detourDelegates = new Dictionary<IntPtr, Delegate>();
        // Keep managed original delegates per target to prevent GC
        private static readonly Dictionary<IntPtr, Delegate> _originalDelegates = new Dictionary<IntPtr, Delegate>();

        // Ensure MinHook is initialized only once
        private static bool _initialized = false;
        private static readonly object _sync = new object();

        /// <summary>
        /// Install hook and return a managed delegate that wraps the original function.
        /// The returned delegate should be kept by the caller if it will be used long-term.
        /// </summary>
        public static T InstallHook<T>(string moduleName, string functionName, T detourFunc) where T : Delegate
        {
            if (detourFunc == null)
            {
                throw new ArgumentNullException(nameof(detourFunc), "detourFunc can not be null.");
            }

            // Disallow open generic delegate types (contains generic parameters)
            if (detourFunc.GetType().ContainsGenericParameters)
            {
                throw new InvalidOperationException($"the type of {nameof(detourFunc)} must be a closed delegate type.");
            }

            lock (_sync)
            {
                // 1. 初始化 MinHook（只初始化一次）
                if (!_initialized)
                {
                    MinHook.MH_STATUS status = MinHook.MH_Initialize();
                    if (status != MinHook.MH_STATUS.MH_OK)
                    {
                        Console.WriteLine($"MH_Initialize failed: {status}");
                        return default;
                    }

                    _initialized = true;
                }

                IntPtr targetPtr = IntPtr.Zero;

                try
                {
                    // 2. 获取目标函数的地址
                    targetPtr = MinHook.GetProcAddress(MinHook.GetModuleHandle(moduleName), functionName);
                    if (targetPtr == IntPtr.Zero)
                    {
                        Console.WriteLine($"Failed to get {functionName} address");
                        return default;
                    }

                    // 如果已经对该地址安装了钩子，拒绝重复安装
                    if (_detourDelegates.ContainsKey(targetPtr))
                    {
                        Console.WriteLine($"Hook for {functionName} already installed.");
                        // return the existing original delegate if available
                        if (_originalDelegates.TryGetValue(targetPtr, out Delegate existing))
                        {
                            return existing as T;
                        }
                        return default;
                    }

                    // 3. 创建钩子
                    // keep a managed reference to prevent GC
                    var del = detourFunc as Delegate;
                    _detourDelegates[targetPtr] = del;

                    IntPtr detourPtr = Marshal.GetFunctionPointerForDelegate(del);
                    MinHook.MH_STATUS status = MinHook.MH_CreateHook(targetPtr, detourPtr, out IntPtr origPtr);
                    if (status != MinHook.MH_STATUS.MH_OK)
                    {
                        Console.WriteLine($"MH_CreateHook failed: {status}");
                        // if creation failed, clear stored delegate
                        _detourDelegates.Remove(targetPtr);
                        return default;
                    }

                    // Create managed delegate for original function and store it to prevent GC
                    Delegate originalDelegate = Marshal.GetDelegateForFunctionPointer(origPtr, typeof(T));
                    _originalDelegates[targetPtr] = originalDelegate;

                    // 4. 启用钩子
                    status = MinHook.MH_EnableHook(targetPtr);
                    if (status != MinHook.MH_STATUS.MH_OK)
                    {
                        Console.WriteLine($"MH_EnableHook failed: {status}");
                        // 若启用失败，尝试移除已创建的钩子
                        try { MinHook.MH_RemoveHook(targetPtr); } catch { }
                        _detourDelegates.Remove(targetPtr);
                        _originalDelegates.Remove(targetPtr);
                        return default;
                    }

                    Console.WriteLine($"{functionName} hook installed successfully");

                    return originalDelegate as T;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"InstallHook exception: {ex.Message}");
                    // Best-effort cleanup
                    try
                    {
                        if (targetPtr != IntPtr.Zero)
                        {
                            MinHook.MH_RemoveHook(targetPtr);
                        }
                    }
                    catch { }

                    if (targetPtr != IntPtr.Zero)
                    {
                        _detourDelegates.Remove(targetPtr);
                        _originalDelegates.Remove(targetPtr);
                    }

                    return default;
                }
            }
        }

        public static void UninstallHook()
        {
            lock (_sync)
            {
                if (_detourDelegates.Count == 0)
                {
                    Console.WriteLine("No active hook to uninstall");
                    return;
                }

                // 逐个禁用并移除钩子
                var targets = _detourDelegates.Keys.ToList();
                foreach (var target in targets)
                {
                    // 1. 禁用钩子
                    var status = MinHook.MH_DisableHook(target);
                    if (status != MinHook.MH_STATUS.MH_OK)
                    {
                        Console.WriteLine($"MH_DisableHook failed for {target}: {status}");
                    }

                    // 2. 移除钩子
                    status = MinHook.MH_RemoveHook(target);
                    if (status != MinHook.MH_STATUS.MH_OK)
                    {
                        Console.WriteLine($"MH_RemoveHook failed for {target}: {status}");
                    }

                    // 清理托管引用
                    _detourDelegates.Remove(target);
                    _originalDelegates.Remove(target);
                }

                // 3. 卸载 MinHook（仅当之前初始化过）
                if (_initialized)
                {
                    var status = MinHook.MH_Uninitialize();
                    if (status != MinHook.MH_STATUS.MH_OK)
                    {
                        Console.WriteLine($"MH_Uninitialize failed: {status}");
                    }
                    _initialized = false;
                }

                Console.WriteLine("Hook(s) uninstalled successfully");
            }
        }

        /// <summary>
        /// Get managed delegate wrapping original function for a hooked target.
        /// </summary>
        public static T GetOriginalDelegateForTarget<T>(IntPtr target) where T : Delegate
        {
            lock (_sync)
            {
                return _originalDelegates.TryGetValue(target, out var d) ? d as T : null;
            }
        }
    }
}
