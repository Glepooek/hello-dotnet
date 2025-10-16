using MessagerSamples.Messages;
using MessagerSamples.Models;
using Microsoft.Toolkit.Mvvm.Messaging;
using System.Collections.Generic;
using System.Windows;

namespace MessagerSamples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IRecipient<LoggedInUserCollectionRequestMessage>
    {
        public MainWindow()
        {
            InitializeComponent();

            // 两种注册消息方式
            //WeakReferenceMessenger.Default.Register<LoggedInUserCollectionRequestMessage>(this, (r, m) =>
            //{
            //    m.Reply(new User() { Name = "any" });
            //});
            WeakReferenceMessenger.Default.Register<LoggedInUserCollectionRequestMessage>(this);
        }

        public void Receive(LoggedInUserCollectionRequestMessage message)
        {
            message.Reply(new User() { Name = "any" });
            message.Reply(new User() { Name = "any1" });
            message.Reply(new User() { Name = "any2" });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var message = WeakReferenceMessenger.Default.Send<LoggedInUserCollectionRequestMessage>();
            foreach (var item in message.Responses)
            {
                MessageBox.Show(item.Name);
            }
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
