using System;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;

namespace ChannelFirstExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Channel<int> m_Channel;

        public MainWindow()
        {
            InitializeComponent();

            m_Channel = Channel.CreateBounded<int>(new BoundedChannelOptions(5)
            {
                FullMode = BoundedChannelFullMode.Wait
            });

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(async () =>
            {
                await SingleProducerWriteAsync();
            });

            Task.Run(async () =>
            {
                await SingleConsumerReadAsync();
            });
        }

        private async Task SingleProducerWriteAsync()
        {
            var writer = m_Channel.Writer;
            for (int i = 0; i < 100; i++)
            {
                if (await writer.WaitToWriteAsync())
                {
                    await writer.WriteAsync(i + 1);
                    Console.WriteLine($"Writer: {i + 1}");
                }
            }
        }

        private async Task SingleConsumerReadAsync()
        {
            var reader = m_Channel.Reader;
            while (await reader.WaitToReadAsync())
            {
                if (reader.TryRead(out var number))
                {
                    Console.WriteLine($"Reader: {number}");
                }
            }
        }
    }
}
