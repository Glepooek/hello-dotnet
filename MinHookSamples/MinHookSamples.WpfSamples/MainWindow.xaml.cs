using MinHookSamples.Shared;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MinHookSamples.WpfSamples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // 安装钩子
            HookManager.InstallHook("user32.dll", "MessageBoxW",
                (hWnd, text, caption, type) =>
                {
                    Debug.WriteLine($"我已成功拦截到 MessageBox：内容 {text}, 标题: {caption}");

                    HookManager.MessageBoxDelegate? original = Marshal.GetDelegateForFunctionPointer<HookManager.MessageBoxDelegate>(HookManager.OriginalFunction);

                    return original(hWnd, text, caption, type);
                });

            this.Unloaded += (s, e) =>
            {
                // 卸载钩子
                HookManager.UninstallHook();
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "This is a test",
                "Test",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}