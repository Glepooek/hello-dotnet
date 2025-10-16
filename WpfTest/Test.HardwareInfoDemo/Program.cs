// See https://aka.ms/new-console-template for more information
using Hardware.Info;
using System.Net.NetworkInformation;
using System.ServiceProcess;

// cmd命令：
// sc query winmgmt
// sc start winmgmt
// sc stop winmgmt
// sc config winmgmt start= auto

IHardwareInfo hardwareInfo = null;

try
{
    // 管理 Windows 服务的核心组件
    // 启动服务，需要管理员权限
    //ServiceController serviceController = new ServiceController("winmgmt");
    //if (serviceController.Status != ServiceControllerStatus.Running)
    //{
    //    serviceController.Start(); 
    //    serviceController.WaitForStatus(ServiceControllerStatus.Running);
    //}

    hardwareInfo = new HardwareInfo();

    //hardwareInfo.RefreshOperatingSystem();
    //hardwareInfo.RefreshMemoryStatus();
    //hardwareInfo.RefreshBatteryList();
    //hardwareInfo.RefreshBIOSList();
    //hardwareInfo.RefreshComputerSystemList();
    //hardwareInfo.RefreshCPUList();
    //hardwareInfo.RefreshDriveList();
    //hardwareInfo.RefreshKeyboardList();
    //hardwareInfo.RefreshMemoryList();
    //hardwareInfo.RefreshMonitorList();
    //hardwareInfo.RefreshMotherboardList();
    //hardwareInfo.RefreshMouseList();
    //hardwareInfo.RefreshNetworkAdapterList();
    //hardwareInfo.RefreshPrinterList();
    //hardwareInfo.RefreshSoundDeviceList();
    //hardwareInfo.RefreshVideoControllerList();

    hardwareInfo.RefreshAll();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    return;
}

Console.WriteLine("操作系统信息：");
Console.WriteLine(hardwareInfo.OperatingSystem);

Console.WriteLine("内存状态信息：");
Console.WriteLine(hardwareInfo.MemoryStatus);

Console.WriteLine("电池信息：");
foreach (var hardware in hardwareInfo.BatteryList)
    Console.WriteLine(hardware);

Console.WriteLine("BIOS信息：");
foreach (var hardware in hardwareInfo.BiosList)
    Console.WriteLine(hardware);

Console.WriteLine("计算机系统信息：");
foreach (var hardware in hardwareInfo.ComputerSystemList)
    Console.WriteLine(hardware);

Console.WriteLine("CPU信息：");
foreach (var cpu in hardwareInfo.CpuList)
{
    Console.WriteLine(cpu);

    foreach (var cpuCore in cpu.CpuCoreList)
        Console.WriteLine(cpuCore);
}

Console.WriteLine("磁盘驱动器信息：");
foreach (var drive in hardwareInfo.DriveList)
{
    Console.WriteLine(drive);

    foreach (var partition in drive.PartitionList)
    {
        Console.WriteLine(partition);

        foreach (var volume in partition.VolumeList)
            Console.WriteLine(volume);
    }
}

Console.WriteLine("键盘信息：");
foreach (var hardware in hardwareInfo.KeyboardList)
    Console.WriteLine(hardware);

Console.WriteLine("内存信息：");
foreach (var hardware in hardwareInfo.MemoryList)
    Console.WriteLine(hardware);

Console.WriteLine("显示器信息：");
foreach (var hardware in hardwareInfo.MonitorList)
    Console.WriteLine(hardware);

Console.WriteLine("主板信息：");
foreach (var hardware in hardwareInfo.MotherboardList)
    Console.WriteLine(hardware);

Console.WriteLine("鼠标信息：");
foreach (var hardware in hardwareInfo.MouseList)
    Console.WriteLine(hardware);

Console.WriteLine("网络适配器信息：");
foreach (var hardware in hardwareInfo.NetworkAdapterList)
    Console.WriteLine(hardware);

Console.WriteLine("打印机信息：");
foreach (var hardware in hardwareInfo.PrinterList)
    Console.WriteLine(hardware);

Console.WriteLine("声音设备信息：");
foreach (var hardware in hardwareInfo.SoundDeviceList)
    Console.WriteLine(hardware);

Console.WriteLine("显卡信息：");
foreach (var hardware in hardwareInfo.VideoControllerList)
    Console.WriteLine(hardware);

Console.WriteLine("本地IPv4地址：");
foreach (var address in HardwareInfo.GetLocalIPv4Addresses(NetworkInterfaceType.Ethernet, OperationalStatus.Up))
    Console.WriteLine(address);

Console.WriteLine();

foreach (var address in HardwareInfo.GetLocalIPv4Addresses(NetworkInterfaceType.Wireless80211))
    Console.WriteLine(address);

Console.WriteLine();

foreach (var address in HardwareInfo.GetLocalIPv4Addresses(OperationalStatus.Up))
    Console.WriteLine(address);

Console.WriteLine();

foreach (var address in HardwareInfo.GetLocalIPv4Addresses())
    Console.WriteLine(address);

Console.ReadLine();
