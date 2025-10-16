using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test.DragBehaviorDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // 这种方式可以拖动，但无法点击按钮，弃用
            //this.Loaded += (sender, args) =>
            //{
            //    AnswerButton.OwnedByCanvas = canvas;
            //    AnswerButton button = new AnswerButton();
            //    Canvas.SetLeft(button, 100);
            //    Canvas.SetTop(button, 100);
            //    canvas.Children.Add(button);
            //};

            strings = new ObservableCollection<string> { "1", "2" };
            this.DataContext = this;
        }

        public ObservableCollection<string> strings { get; set; }

        private void DragMoveBehavior1_DragFinished_1(object sender, DragFinishedEventArgs e)
        {
            Debug.WriteLine(e.Margin.Left + "  " + e.Margin.Top);
        }
    }
}
