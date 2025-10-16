using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Test.AttachedProperties;

public static class StackPanelHelper
{
    public static Storyboard GetMyStoryboard(DependencyObject obj)
    {
        return (Storyboard)obj.GetValue(MyStoryboardProperty);
    }

    public static void SetMyStoryboard(DependencyObject obj, bool value)
    {
        obj.SetValue(MyStoryboardProperty, value);
    }

    public static readonly DependencyProperty MyStoryboardProperty =
        DependencyProperty.RegisterAttached(
            "MyStoryboard",
            typeof(Storyboard),
            typeof(StackPanelHelper),
            new PropertyMetadata(null, On1)
        );

    private static void On1(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is StackPanel panel)
        {
            if (e.NewValue is Storyboard storyboard)
            {
                panel.IsVisibleChanged -= IsVisibleChanged;
                panel.IsVisibleChanged += IsVisibleChanged;
            }
            else
            {
                panel.IsVisibleChanged -= IsVisibleChanged;
            }
        }
    }

    private static void IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (sender is DependencyObject obj && e.NewValue is bool isVisible)
        {
            var storyboard = GetMyStoryboard(obj);
            if (isVisible)
            {
                storyboard?.Begin();
            }
            else
            {
                storyboard?.Stop();
            }
        }
    }
}
