using MessagerSamples.Messages;
using MessagerSamples.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows;

namespace MessagerSamples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IRecipient<LoggedInUserRequestMessage>
    {
        public MainWindow()
        {
            InitializeComponent();

            // 两种注册消息方式
            //WeakReferenceMessenger.Default.Register<LoggedInUserRequestMessage>(this, (r, m) =>
            //{
            //    m.Reply(new User() { Name = "any" });
            //});
            WeakReferenceMessenger.Default.Register<LoggedInUserRequestMessage>(this);
        }

        public void Receive(LoggedInUserRequestMessage message)
        {
            message.Reply(new User() { Name = "any" });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User user = WeakReferenceMessenger.Default.Send<LoggedInUserRequestMessage>();
            MessageBox.Show(user.Name);
        }

        private void UnRegisterMessages()
        {
            //WeakReferenceMessenger.Default.Unregister<LoggedInUserRequestMessage>(this);
            //WeakReferenceMessenger.Default.Unregister<LoggedInUserRequestMessage, int>(this, 42);
            //WeakReferenceMessenger.Default.UnregisterAll(this);
            //WeakReferenceMessenger.Default.UnregisterAll(this, 42);
        }
    }
}
