using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MinHookSamples.Shared
{
    public class DumpGen
    {
        public static void GenerateProcessDump(int processId, string dumpPath)
        {
            try
            {
                // ProcDump 命令行参数：
                // -mm: 生成 MiniDump
                // -accepteula: 自动接受许可协议（避免首次运行时弹出提示）
                string procDumpPath = $@"{Environment.CurrentDirectory}\procdump64.exe";
                string arguments = $"-accepteula -mm {processId} \"{dumpPath}\"";

                var startInfo = new ProcessStartInfo
                {
                    FileName = procDumpPath,
                    Arguments = arguments,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (var proc = new Process { StartInfo = startInfo })
                {
                    proc.Start();
                    proc.WaitForExit();
                    Console.WriteLine("Dump captured successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to launch ProcDump: {ex.Message}");
            }
        }
    }
}
