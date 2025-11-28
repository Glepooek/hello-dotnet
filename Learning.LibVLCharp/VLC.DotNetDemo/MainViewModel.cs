using Stylet;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Vlc.DotNet.Core;
using VLC.DotNetDemo.ChainPattern;

namespace VLC.DotNetDemo
{
    public class MainViewModel : Screen
    {
        #region Fields

        private VlcMediaPlayer mVLCPlayer;
        private MainView mMainView;
        TransitionInHandler mInHandler;
        TransitionTimerHandler mTimerHandler;
        TransitionOutHandler mOutHandler;

        #endregion

        #region Methods

        protected override void OnViewLoaded()
        {
            InitPlayer();
            InitTransitionHandler();
        }

        protected override void OnClose()
        {
            Task.Run(() =>
            {
                if (mVLCPlayer.IsPlaying())
                {
                    mVLCPlayer.Stop();
                }
                mVLCPlayer.EndReached -= OnMediaEndReached;
                mVLCPlayer.Dispose();
            });

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
            mVLCPlayer.EndReached += OnMediaEndReached;
        }

        private void InitTransitionHandler()
        {
            mInHandler = new TransitionInHandler(mVLCPlayer);
            mTimerHandler = new TransitionTimerHandler(mVLCPlayer);
            mOutHandler = new TransitionOutHandler(mVLCPlayer);

            Random random = new Random();
            int totalTime = random.Next(1, 13);

            if (totalTime > 3)
            {
                mInHandler.SetNext(mTimerHandler);
                mTimerHandler.SetNext(mOutHandler);
            }
            else
            {
                mInHandler.SetNext(mOutHandler);
            }

            mInHandler.Play();
        }

        private void OnMediaEndReached(object sender, EventArgs e)
        {
            if (sender is VlcMediaPlayer player)
            {
                var media = player.GetMedia();
                if (media.Mrl == mInHandler.GetPlayerUri().AbsoluteUri)
                {
                    mInHandler.Handle();
                }
                else if (media.Mrl == mTimerHandler.GetPlayerUri().AbsoluteUri)
                {
                    mTimerHandler.Handle();
                }
            }
        }

        #endregion
    }
}
