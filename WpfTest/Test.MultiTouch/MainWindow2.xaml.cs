using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Test.MultiTouch
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow2 : Window
    {
        public MainWindow2()
        {
            InitializeComponent();
        }

        private void image_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            // 需要定义ManipulationContainer（即为touchPad<Canvas>），该容器的主要用于定义参考坐标，图片的任何操作均以参考坐标为准。
            // ManipulationModes 设置可以限制哪些手势操作是程序允许的，如果没有特殊情况可设置为"All"。
            e.ManipulationContainer = touchPad;
            e.Mode = ManipulationModes.All;
        }

        private void image_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)e.Source;
            element.Opacity = 0.5;
            TransformGroup transformGroup = element.RenderTransform as TransformGroup;

            var scaleTF = transformGroup.Children[0] as ScaleTransform;
            scaleTF.ScaleX *= e.DeltaManipulation.Scale.X;
            scaleTF.ScaleY *= e.DeltaManipulation.Scale.Y;

            var rotateTF = transformGroup.Children[1] as RotateTransform;
            rotateTF.Angle += e.DeltaManipulation.Rotation;

            var translateTF = transformGroup.Children[2] as TranslateTransform;
            translateTF.X += e.DeltaManipulation.Translation.X;
            translateTF.Y += e.DeltaManipulation.Translation.Y;
        }

        private void image_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)e.Source;
            element.Opacity = 1;
        }

        // 启用惯性效果
        private void image_ManipulationInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        {
            e.TranslationBehavior = new InertiaTranslationBehavior
            {
                InitialVelocity = e.InitialVelocities.LinearVelocity,
                DesiredDeceleration = 10.0 * 96.0 / (1000.0 * 1000.0)
            };

            e.ExpansionBehavior = new InertiaExpansionBehavior
            {
                InitialVelocity = e.InitialVelocities.ExpansionVelocity,
                DesiredDeceleration = 0.1 * 96 / 1000.0 * 1000.0
            };

            e.RotationBehavior = new InertiaRotationBehavior
            {
                InitialVelocity = e.InitialVelocities.AngularVelocity,
                DesiredDeceleration = 2 * 360 / (1000.0 * 1000.0)
            };
        }
    }
}