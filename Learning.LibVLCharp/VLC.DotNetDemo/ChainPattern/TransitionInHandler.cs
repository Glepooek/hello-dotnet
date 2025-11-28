using System;
using Vlc.DotNet.Core;

namespace VLC.DotNetDemo.ChainPattern
{
    public class TransitionInHandler : TransitionHandler
    {
        private string mTransitionInFilePath =
           AppDomain.CurrentDomain.BaseDirectory + @"Videos\transition_begin.mov";
        private Uri mTransitionInUri;

        public TransitionInHandler(VlcMediaPlayer vlcMediaPlayer) : base(vlcMediaPlayer)
        {
            mTransitionInUri = new Uri(mTransitionInFilePath);
        }

        public override void Play()
        {
            mVlcMediaPlayer?.Play(mTransitionInUri);
        }

        public override Uri GetPlayerUri()
        {
            return mTransitionInUri;
        }
    }
}
