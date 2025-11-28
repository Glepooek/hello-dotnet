using Stylet;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using Vlc.DotNet.Core;

namespace VLC.DotNetDemo
{
    public class MainViewModel : Screen
    {
        #region Fields

        private string mTransitionInFilePath =
           AppDomain.CurrentDomain.BaseDirectory + @"Videos\e79800759e0a9407f7dc3033c3cf230d-480p.mp4";
        private VlcMediaPlayer mVLCPlayer;
        private MainView mMainView;

        #endregion

        #region Methods

        protected override void OnViewLoaded()
        {
            InitPlayer();
        }

        protected override void OnClose()
        {
            mMainView.VlcControl.SourceProvider.Dispose();
        }

        private void InitPlayer()
        {
            mMainView = this.View as MainView;
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            // Default installation path of VideoLAN.LibVLC.Windows
            var libDirectory = new DirectoryInfo(Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));

            mMainView.VlcControl.SourceProvider.CreatePlayer(libDirectory);
            mVLCPlayer = mMainView.VlcControl.SourceProvider.MediaPlayer;
        }

        public void DoBeginPlay()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (!mVLCPlayer.IsPlaying())
                {
                    mVLCPlayer.Play(new Uri(mTransitionInFilePath));
                }
            });
        }

        public void DoPausePlay()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (mVLCPlayer.IsPausable())
                {
                    mVLCPlayer.Pause();
                }
            });
        }

        public void DoStopPlay()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (mVLCPlayer.IsPlaying() || mVLCPlayer.IsPausable())
                {
                    mVLCPlayer.Stop();
                }
            });
        }

        public void DoResetPlay()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (mVLCPlayer.IsPlaying())
                {
                    mVLCPlayer.ResetMedia();
                    mTransitionInFilePath = AppDomain.CurrentDomain.BaseDirectory + @"Videos\b20dd8cdab22731629fe45455b22c327-480p.mp4";
                }
            });
        }

        #endregion
    }
}
