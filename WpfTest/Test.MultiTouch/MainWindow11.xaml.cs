using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Test.MultiTouch
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow11 : Window
    {
        public MainWindow11()
        {
            InitializeComponent();
        }

        private void touchPad_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = touchPad;
            e.Handled = true;
        }

        private void touchPad_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            UIElement element = e.Source as UIElement;
            MatrixTransform xform = element.RenderTransform as MatrixTransform;
            Matrix matrix = xform.Matrix;

            matrix.ScaleAt(e.DeltaManipulation.Scale.X, e.DeltaManipulation.Scale.Y, e.ManipulationOrigin.X, e.ManipulationOrigin.Y);
            matrix.RotateAt(e.DeltaManipulation.Rotation, e.ManipulationOrigin.X, e.ManipulationOrigin.Y);
            matrix.Translate(e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y);
            xform.Matrix = matrix;

            e.Handled = true;
        }
    }
}