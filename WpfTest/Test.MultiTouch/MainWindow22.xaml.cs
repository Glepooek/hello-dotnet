using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Test.MultiTouch
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow22 : Window
    {
        public MainWindow22()
        {
            InitializeComponent();
        }

        private void image_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            // 需要定义ManipulationContainer（即为touchPad<Canvas>），该容器的主要用于定义参考坐标，图片的任何操作均以参考坐标为准。
            // ManipulationModes 设置可以限制哪些手势操作是程序允许的，如果没有特殊情况可设置为"All"。
            e.ManipulationContainer = touchPad;
            e.Mode = ManipulationModes.Scale | ManipulationModes.Translate;
            e.Handled = true;
        }

        private void image_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)e.Source;
            element.Opacity = 0.5;
            // 更新缩放中心和缩放因子
            scaleTransform.CenterX = e.ManipulationOrigin.X;
            scaleTransform.CenterY = e.ManipulationOrigin.Y;
            scaleTransform.ScaleX *= e.DeltaManipulation.Scale.X;
            scaleTransform.ScaleY *= e.DeltaManipulation.Scale.Y;

            // 更新平移变换
            translateTransform.X += e.DeltaManipulation.Translation.X;
            translateTransform.Y += e.DeltaManipulation.Translation.Y;

            e.Handled = true;
        }

        private void image_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)e.Source;
            element.Opacity = 1;
            e.Handled = true;
        }

        // 启用惯性效果
        private void image_ManipulationInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        {
            e.TranslationBehavior = new InertiaTranslationBehavior
            {
                InitialVelocity = e.InitialVelocities.LinearVelocity,
                DesiredDeceleration = 10.0 * 96.0 / (100.0 * 100.0)
            };

            e.ExpansionBehavior = new InertiaExpansionBehavior
            {
                InitialVelocity = e.InitialVelocities.ExpansionVelocity,
                DesiredDeceleration = 0.1 * 96 / 100.0 * 100.0
            };
        }
    }
}