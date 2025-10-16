using System.Windows;
using System.Windows.Controls;

namespace Test.DragBehaviorDemo
{
    /// <summary>
    /// AnswerButton.xaml 的交互逻辑
    /// </summary>
    public partial class AnswerButton : UserControl
    {
        /// <summary>
        /// 所属Canvas
        /// </summary>
        public static Canvas OwnedByCanvas { get; set; }

        public AnswerButton()
        {
            this.Tag = OwnedByCanvas;
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show("test");
        }
    }
}
