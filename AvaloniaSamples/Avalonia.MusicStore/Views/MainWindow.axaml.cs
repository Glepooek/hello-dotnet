using Avalonia.Controls;
using Avalonia.MusicStore.ViewModels;
using CommunityToolkit.Mvvm.Messaging;

namespace Avalonia.MusicStore.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Unloaded += MainWindow_Unloaded;
        }

        private void MainWindow_Unloaded(object? sender, Interactivity.RoutedEventArgs e)
        {
            MainWindowViewModel viewModel = this.DataContext as MainWindowViewModel;
            WeakReferenceMessenger.Default.UnregisterAll(viewModel);
        }
    }
}