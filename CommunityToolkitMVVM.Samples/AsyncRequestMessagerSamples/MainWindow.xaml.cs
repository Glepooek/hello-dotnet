using CommunityToolkit.Mvvm.Messaging;
using MessagerSamples.Messages;
using MessagerSamples.Models;
using System.Threading.Tasks;
using System.Windows;

namespace MessagerSamples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IRecipient<LoggedInUserAsyncRequestMessage>
    {
        public MainWindow()
        {
            InitializeComponent();

            // 两种注册消息方式
            //WeakReferenceMessenger.Default.Register<LoggedInUserAsyncRequestMessage>(this, (r, m) =>
            //{
            //    //m.Reply(new User() { Name = "any" });
            //    m.Reply(this.GetUserAsync());
            //});
            WeakReferenceMessenger.Default.Register<LoggedInUserAsyncRequestMessage>(this);
        }

        public void Receive(LoggedInUserAsyncRequestMessage message)
        {
            //message.Reply(new User() { Name = "any" });
            message.Reply(this.GetUserAsync());
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            User user = await WeakReferenceMessenger.Default.Send<LoggedInUserAsyncRequestMessage>();
            MessageBox.Show(user.Name);
        }

        private void UnRegisterMessages()
        {
            //WeakReferenceMessenger.Default.Unregister<LoggedInUserChangedMessage>(this);
            //WeakReferenceMessenger.Default.Unregister<LoggedInUserChangedMessage, int>(this, 42);
            //WeakReferenceMessenger.Default.UnregisterAll(this);
            //WeakReferenceMessenger.Default.UnregisterAll(this, 42);
        }

        public async Task<User> GetUserAsync()
        {
            return new User { Name = "any" };
        }
    }
}
