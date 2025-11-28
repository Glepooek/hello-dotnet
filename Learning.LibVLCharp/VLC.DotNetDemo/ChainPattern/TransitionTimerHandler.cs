using System;
using System.Threading.Tasks;
using Vlc.DotNet.Core;

namespace VLC.DotNetDemo.ChainPattern
{
    public class TransitionTimerHandler : TransitionHandler
    {
        private string mTransitionTimerFilePath =
            AppDomain.CurrentDomain.BaseDirectory + @"Videos\timer.mp4";
        private Uri mTransitionTimerUri;

        public TransitionTimerHandler(VlcMediaPlayer vlcMediaPlayer) : base(vlcMediaPlayer)
        {
            mTransitionTimerUri = new Uri(mTransitionTimerFilePath);
        }

        public override void Play()
        {
            Task.Run(() =>
            {
                mVlcMediaPlayer?.Play(mTransitionTimerUri);
            });
        }

        public override Uri GetPlayerUri()
        {
            return mTransitionTimerUri;
        }
    }
}
