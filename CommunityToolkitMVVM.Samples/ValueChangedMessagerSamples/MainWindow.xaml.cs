using MessagerSamples.Messages;
using MessagerSamples.Models;
using Microsoft.Toolkit.Mvvm.Messaging;
using System.Windows;

namespace MessagerSamples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IRecipient<LoggedInUserChangedMessage>
    {
        public MainWindow()
        {
            InitializeComponent();
            // 两种注册消息方式
            //WeakReferenceMessenger.Default.Register<LoggedInUserChangedMessage>(this, (r, m) =>
            //{
            //    (r as MainWindow).Title = "133";
            //    MessageBox.Show(m.Value.Name);
            //});

            WeakReferenceMessenger.Default.Register<LoggedInUserChangedMessage>(this);
        }

        public void Receive(LoggedInUserChangedMessage message)
        {
            MessageBox.Show(message.Value.Name);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Send(new LoggedInUserChangedMessage(new User() { Name = "any" }));
        }

        private void UnRegisterMessages()
        {
            //WeakReferenceMessenger.Default.Unregister<LoggedInUserChangedMessage>(this);
            //WeakReferenceMessenger.Default.Unregister<LoggedInUserChangedMessage, int>(this, 42);
            //WeakReferenceMessenger.Default.UnregisterAll(this);
            //WeakReferenceMessenger.Default.UnregisterAll(this, 42);
        }
    }
}
