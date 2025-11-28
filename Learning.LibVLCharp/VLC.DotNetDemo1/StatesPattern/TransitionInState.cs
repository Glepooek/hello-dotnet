using System;
using Vlc.DotNet.Core;

namespace VLC.DotNetDemo.StatesPattern
{
    public class TransitionInState : TransitionState
    {
        private string mTransitionInFilePath =
           AppDomain.CurrentDomain.BaseDirectory + @"Videos\transition_begin.mov";
        private Uri mTransitionInUri;

        public TransitionInState(VlcMediaPlayer vlcMediaPlayer) : base(vlcMediaPlayer)
        {
            mTransitionInUri = new Uri(mTransitionInFilePath);
        }

        public override void Play()
        {
            mVlcMediaPlayer.Play(mTransitionInUri);
        }

        public override Uri GetUri()
        {
            return mTransitionInUri;
        }
    }
}
