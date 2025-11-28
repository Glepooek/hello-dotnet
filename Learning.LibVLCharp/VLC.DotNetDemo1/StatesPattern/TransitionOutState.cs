using System;
using System.Threading.Tasks;
using Vlc.DotNet.Core;

namespace VLC.DotNetDemo.StatesPattern
{
    public class TransitionOutState : TransitionState
    {
        private string mTransitionOutFilePath =
            AppDomain.CurrentDomain.BaseDirectory + @"Videos\transition_end.mov";
        private Uri mTransitionOutUri;

        public TransitionOutState(VlcMediaPlayer vlcMediaPlayer) : base(vlcMediaPlayer)
        {
            mTransitionOutUri = new Uri(mTransitionOutFilePath);
        }

        public override void Play()
        {
            Task.Run(() =>
            {
                mVlcMediaPlayer.Play(mTransitionOutUri);
            });
        }

        public override Uri GetUri()
        {
            return mTransitionOutUri;
        }
    }
}
