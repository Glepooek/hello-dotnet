using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test.DataGridDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }

        private void dataGrid_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            ContentPresenter presenter = e.EditingElement as ContentPresenter;
            if (presenter != null && VisualTreeHelper.GetChildrenCount(presenter) > 0)
            {
                TextBox textBox = VisualTreeHelper.GetChild(presenter, 0) as TextBox;
                if (textBox == null)
                {
                    return;
                }
                textBox?.Focus();
                textBox.SelectionStart = textBox.Text.Length > 0 ? textBox.Text.Length : 0;
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            //var textBox = sender as TextBox;
            //var bookmark = textBox.DataContext as Bookmark;
            //bookmark.IsEditing = true;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            var bookmark = textBox.DataContext as Bookmark;
            //bookmark.IsEditing = false;
            //(this.DataContext as MainWindowViewModel).SaveOrUpdateCommand.Execute(bookmark);
        }

        private void dataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.Enter)
            {
                e.Handled = true;
            }
        }

        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Cancel)
            {
                return;
            }

            Bookmark bookmark;
            //ContentPresenter presenter = e.EditingElement as ContentPresenter;
            //if (presenter != null && VisualTreeHelper.GetChildrenCount(presenter) > 0)
            //{
            //    TextBox textBox = VisualTreeHelper.GetChild(presenter, 0) as TextBox;
            //    if (textBox == null)
            //    {
            //        return;
            //    }

            //    bookmark = textBox.DataContext as Bookmark;
            //}

            var panel = FindElementInDataGridCell<StackPanel>(dataGrid, e.Row.GetIndex(), 1);
            if (panel == null)
            {
                return;
            }
            Button button = VisualTreeHelper.GetChild(panel, 0) as Button;
            if (button.Content.Equals("保存"))
            {
                bookmark = (Bookmark)button.DataContext;
                button.Command.Execute(bookmark);
            }
        }

        private T FindElementInDataGridCell<T>(DataGrid dataGrid, int rowIndex, int columnIndex) where T : FrameworkElement
        {
            DataGridRow rowContainer = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(rowContainer);

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
                if (cell != null)
                {
                    // 查找单元格中的目标元素，例如TextBox或TextBlock
                    T element = FindVisualChild<T>(cell);
                    return element;
                }
            }

            return null;
        }

        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                    return (T)child;

                T childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null)
                    return childOfChild;
            }
            return null;
        }
    }
}