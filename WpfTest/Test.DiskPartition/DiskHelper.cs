using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;

namespace Test.DiskPartition
{
    internal class DiskHelper
    {
        /// <summary>
        /// 获取所有驱动器数组
        /// </summary>
        /// <returns></returns>
        public static DriveInfo[] GetDisks(DriveType driveType)
        {
            return DriveInfo.GetDrives()
                .Where(driveInfo => driveInfo.DriveType == driveType).ToArray();
        }

        /// <summary>
        /// 获取硬盘卷序列号  
        /// </summary>
        /// <param name="name">驱动器的名称，如 C:\。</param>
        /// <returns></returns>
        public static string GetDiskVolumeSN(string name)
        {
            try
            {
                string hdInfo = string.Empty;
                using (ManagementObject disk = new ManagementObject($"win32_logicaldisk.deviceid=\"{name}\""))
                {
                    hdInfo = disk.Properties["VolumeSerialNumber"].Value.ToString();
                }

                return hdInfo?.Trim();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取驱动器号对应的序列号
        /// </summary>
        public static Dictionary<string, string> MatchDriveLetterWithSerial(string prefix)
        {
            string[] diskArray;
            string driveNumber;
            // 驱动器号对应序列号的映射
            Dictionary<string, string> map = new Dictionary<string, string>();
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDiskToPartition");
            // 遍历所有硬盘分区
            foreach (ManagementObject dm in searcher.Get())
            {
                string driveletter = GetValueInQuotes(dm["Dependent"].ToString());
                diskArray = GetValueInQuotes(dm["Antecedent"].ToString()).Split(',');
                driveNumber = diskArray[0].Remove(0, 6).Trim();
                var disks = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
                foreach (ManagementObject disk in disks.Get())
                {
                    if (disk["Name"].ToString() == ("\\\\.\\PHYSICALDRIVE" + driveNumber) & disk["InterfaceType"].ToString() == "USB")
                    {
                        if (!map.ContainsKey(driveletter))
                        {
                            map.Add(driveletter, ParseSerialFromDeviceID(disk["PNPDeviceID"].ToString(), prefix));
                        }
                    }
                }
            }

            return map;
        }

        public static string ParseSerialFromDeviceID(string deviceId, string prefix)
        {
            string[] splitDeviceId = deviceId.Split('\\');
            int arrayLen = splitDeviceId.Length - 1;
            string[] serialArray = splitDeviceId[arrayLen].Split('&');
            string serial = string.Empty;
            if (serialArray != null && serialArray.Length > 0)
            {
                foreach (var item in serialArray)
                {
                    if (!string.IsNullOrEmpty(item) && item.StartsWith(prefix))
                    {
                        serial = item;
                        break;
                    }
                }
            }

            return serial;
        }

        private static string GetValueInQuotes(string inValue)
        {
            int posFoundStart = inValue.IndexOf("\"");
            int posFoundEnd = inValue.IndexOf("\"", posFoundStart + 1);
            string parsedValue = inValue.Substring(posFoundStart + 1, (posFoundEnd - posFoundStart) - 1);
            return parsedValue;
        }
    }
}
