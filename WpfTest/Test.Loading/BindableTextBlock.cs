using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Test.Loading
{
    public class BindableTextBlock : TextBlock
    {
        static BindableTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BindableTextBlock), new FrameworkPropertyMetadata(typeof(BindableTextBlock)));
        }

        public ObservableCollection<Inline> InlineList
        {
            get { return (ObservableCollection<Inline>)GetValue(InlineListProperty); }
            set { SetValue(InlineListProperty, value); }
        }

        public static readonly DependencyProperty InlineListProperty =
            DependencyProperty.Register(nameof(InlineList), typeof(ObservableCollection<Inline>), typeof(BindableTextBlock), new UIPropertyMetadata(null, OnPropertyChanged));

        public static void OnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;

            var textBlock = (BindableTextBlock)sender;

            ObservableCollection<Inline> coll = (ObservableCollection<Inline>)e.NewValue;
            textBlock.Inlines.AddRange((ObservableCollection<Inline>)e.NewValue);
            coll.CollectionChanged += (ss, ee) =>
            {
                if (ee.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    textBlock.Inlines.Add(ee.NewItems[0] as Inline);
                }
            };
        }
    }
}
