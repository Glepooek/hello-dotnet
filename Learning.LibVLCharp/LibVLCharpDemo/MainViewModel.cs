using LibVLCSharp.Shared;
using Stylet;
using System;
using System.Threading;
using MediaPlayer = LibVLCSharp.Shared.MediaPlayer;

namespace LibVLCharpDemo
{
    public class MainViewModel : Screen
    {
        #region Constructor

        public MainViewModel()
        {
            mTransitionBeginUri = new Uri(mTransitionBeginFilePath);
            mTransitionEndUri = new Uri(mTransitionEndFilePath);
            mTransitionTimerUri = new Uri(mTransitionTimerFilePath);

            InitPlayer();
        }

        #endregion

        #region Fields

        private string mTransitionBeginFilePath =
            AppDomain.CurrentDomain.BaseDirectory + @"Videos\transition_begin.mov";
        private string mTransitionEndFilePath =
            AppDomain.CurrentDomain.BaseDirectory + @"Videos\transition_end.mov";
        private string mTransitionTimerFilePath =
            AppDomain.CurrentDomain.BaseDirectory + @"Videos\timer.mp4";
        private Uri mTransitionBeginUri;
        private Uri mTransitionEndUri;
        private Uri mTransitionTimerUri;

        LibVLC mLibVLC;
        private MediaPlayer mVLCPlayer;
        private AutoResetEvent mAutoResetEvent;

        #endregion

        #region Properties

        public MediaPlayer VLCPlayer
        {
            get { return mVLCPlayer; }
            set { SetAndNotify(ref mVLCPlayer, value); }
        }

        #endregion

        #region Methods

        private void InitPlayer()
        {
            mLibVLC = new LibVLC();
            mVLCPlayer = new MediaPlayer(mLibVLC);
            mAutoResetEvent = new AutoResetEvent(false);
            mVLCPlayer.EndReached += OnMediaEndReached;
            mVLCPlayer.Stopped += MVLCPlayer_Stopped;
        }

        private void MVLCPlayer_Stopped(object sender, EventArgs e)
        {
        }

        private void OnMediaEndReached(object sender, EventArgs e)
        {
            mAutoResetEvent.Set();

            //VLCPlayer.Stop();
        }

        public void DoBeginPlay()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (!VLCPlayer.IsPlaying)
                {
                    using (var media = new Media(mLibVLC, mTransitionBeginUri))
                        VLCPlayer.Play(media);
                    mAutoResetEvent.WaitOne();
                    using (var media = new Media(mLibVLC, mTransitionTimerUri))
                        VLCPlayer.Play(media);
                    mAutoResetEvent.WaitOne();
                    using (var media = new Media(mLibVLC, mTransitionEndUri))
                        VLCPlayer.Play(media);
                }
            });
        }

        public void DoStopPlay()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (VLCPlayer.IsPlaying)
                {
                    VLCPlayer.Stop();
                }
            });
        }

        protected override void OnClose()
        {
            VLCPlayer.Stop();
            VLCPlayer.EndReached -= OnMediaEndReached;
            VLCPlayer.Dispose();
            mLibVLC.Dispose();
        }

        #endregion
    }
}
