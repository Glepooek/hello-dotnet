using LibVLCSharp.Shared;
using System.Windows;

namespace LibVLCharpDemo
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Core.Initialize();
        }
    }
}
