using System;
using System.Threading.Tasks;
using Vlc.DotNet.Core;

namespace VLC.DotNetDemo.StatesPattern
{
    public class TransitionTimerState : TransitionState
    {
        private string mTransitionTimerFilePath =
          AppDomain.CurrentDomain.BaseDirectory + @"Videos\timer.mp4";
        private Uri mTransitionTimerUri;

        public TransitionTimerState(VlcMediaPlayer vlcMediaPlayer) : base(vlcMediaPlayer)
        {
            mTransitionTimerUri = new Uri(mTransitionTimerFilePath);
        }

        public override void Play()
        {
            Task.Run(() =>
            {
                mVlcMediaPlayer.Play(mTransitionTimerUri);
            });
        }

        public override Uri GetUri()
        {
            return mTransitionTimerUri;
        }
    }
}
