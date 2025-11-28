using System;
using Vlc.DotNet.Core;

namespace VLC.DotNetDemo.StatesPattern
{
    public abstract class TransitionState
    {
        protected VlcMediaPlayer mVlcMediaPlayer;
        public TransitionState(VlcMediaPlayer vlcMediaPlayer)
        {
            mVlcMediaPlayer = vlcMediaPlayer;
        }
        public abstract void Play();
        public abstract Uri GetUri();
    }
}
