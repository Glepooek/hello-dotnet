using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;

namespace Test.RestSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource _cancellationTokenSource;
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var url = "https://k12-teaching-cdn.unipus.cn/digital-platform/online/CDRom/zhejiang/nse7b_zj.zip";
            var fileName = $"{AppDomain.CurrentDomain.BaseDirectory}\\nse7b_zj.zip";
            await DownloadFileAsync(url, fileName, p =>
            {
                text.Text = p.ToString();
            }, _cancellationTokenSource.Token);
        }

        public static async Task<bool> DownloadFileAsync(string url, string fileName, Action<double>? progress = default, CancellationToken cancelationToken = default)
        {
            try
            {
                var handler = new HttpClientHandler()
                {
                    UseProxy = false,
                    AllowAutoRedirect = true,
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip,
                    SslProtocols = System.Security.Authentication.SslProtocols.Tls12
                        | System.Security.Authentication.SslProtocols.Tls13
                        | System.Security.Authentication.SslProtocols.Ssl3
                        | System.Security.Authentication.SslProtocols.Tls
                        | System.Security.Authentication.SslProtocols.Tls11
                };

                long existContentLength = 0;
                if (File.Exists(fileName))
                {
                    FileInfo fileInfo = new FileInfo(fileName);
                    existContentLength = fileInfo.Length;
                }

                using var httpClient = new HttpClient(handler);
                httpClient.Timeout = TimeSpan.FromSeconds(20000);

                if (existContentLength > 0)
                {
                    httpClient.DefaultRequestHeaders.Range = new RangeHeaderValue(existContentLength, null);
                }

                // 发送GET请求，并等待响应
                HttpResponseMessage? response = await httpClient.GetAsync(new Uri(url), HttpCompletionOption.ResponseHeadersRead, cancelationToken);
                // 判断请求是否成功,如果失败则返回 false
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                long contentLength = response.Content.Headers.ContentLength ?? 0;
                using var fs = File.Open(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
                // 创建一个缓冲区，大小为64KB
                byte[] buffer = new byte[65536];
                int readLength = 1;

                // 获取响应流
                var httpStream = await response.Content.ReadAsStreamAsync(cancelationToken);

                while (readLength > 0 && !cancelationToken.IsCancellationRequested)
                {
                    // 用下面这几句处理超时
                    using (var clts = CancellationTokenSource.CreateLinkedTokenSource(cancelationToken))
                    {
                        var token = clts.Token;
                        clts.CancelAfter(20000);
                        using (token.Register(() =>
                        {
                            fs.Close();
                            httpStream.Close();
                        }))
                        {
                            readLength = await httpStream.ReadAsync(buffer, token);
                        };
                    }

                    // 将读取到的内容写入文件流，并调用进度回调函数
                    await fs.WriteAsync(buffer.AsMemory(0, readLength), cancelationToken);
                    await fs.FlushAsync(cancelationToken);
                    progress?.Invoke(Math.Round((double)fs.Length / contentLength * 100, 2));
                }

                return true;
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _cancellationTokenSource.Cancel();
        }
    }
}