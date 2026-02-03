using LibVLCSharp.Shared;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MediaPlayer = LibVLCSharp.Shared.MediaPlayer;

namespace Test.HwndHostDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;

        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
            {
                // 1. 初始化 LibVLC 库
                Core.Initialize();
                _libVLC = new LibVLC();

                // 2. 创建播放器
                _mediaPlayer = new MediaPlayer(_libVLC);

                // 3. 将播放器挂载到 VideoView 控件上，无空域问题
                // VideoView 内部会获取自身的 HWND 并传给 libvlc
                //VideoPlayer.MediaPlayer = _mediaPlayer;

                // 3. 核心：将 VLC 渲染目标绑定到 Win32 子窗口句柄
                _mediaPlayer.Hwnd = VideoHost.VideoHandle;
            };
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            _mediaPlayer?.Stop();
            _mediaPlayer?.Dispose();
            _libVLC?.Dispose();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (_mediaPlayer == null) return;

            using Media? media = new Media(_libVLC, new Uri("https://vod.unischool.cn/70243167824a71f080155017e1e90102/f27808c875371fda1e7b7a32b4b4a842-sd.mp4"));
            _mediaPlayer.Play(media);
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (_mediaPlayer == null) return;

            if (_mediaPlayer.State == VLCState.Playing)
            {
                _mediaPlayer.Pause();
                pauseBtn.Content = "继续播放";
            }
            else
            {
                _mediaPlayer.Play();
                pauseBtn.Content = "暂停";
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _mediaPlayer?.Stop();
        }
    }
}