using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Test.AttachedProperties;

public class StackPanelBehavior : Behavior<StackPanel>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.IsVisibleChanged += IsVisibleChanged;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.IsVisibleChanged -= IsVisibleChanged;
    }

    public Storyboard MyStoryboard
    {
        get { return (Storyboard)GetValue(MyStoryboardProperty); }
        set { SetValue(MyStoryboardProperty, value); }
    }

    public static readonly DependencyProperty MyStoryboardProperty = DependencyProperty.Register(
        "MyStoryboard",
        typeof(Storyboard),
        typeof(StackPanelBehavior),
        new PropertyMetadata(null)
    );

    private void IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is bool isVisible)
        {
            if (isVisible)
            {
                MyStoryboard?.Begin();
            }
            else
            {
                MyStoryboard?.Stop();
            }
        }
    }
}
