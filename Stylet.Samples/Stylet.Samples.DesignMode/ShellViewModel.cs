namespace Stylet.Samples.DesignMode
{
    public class ShellViewModel : Screen
    {
        public string ViewName { get; set; }

        // 支持XAML Design Mode必须要有默认构造函数
        public ShellViewModel()
        {
            ViewName = "XAML Design Mode Support";
        }
    }
}
