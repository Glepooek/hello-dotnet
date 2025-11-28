using System;
using Vlc.DotNet.Core;

namespace VLC.DotNetDemo.ChainPattern
{
    public abstract class TransitionHandler
    {
        protected TransitionHandler mNextHandler;
        protected VlcMediaPlayer mVlcMediaPlayer;

        public TransitionHandler(VlcMediaPlayer vlcMediaPlayer)
        {
            mVlcMediaPlayer = vlcMediaPlayer;
        }

        public void SetNext(TransitionHandler transitionHandler)
        {
            mNextHandler = transitionHandler;
        }

        public abstract void Play();

        public virtual void Handle()
        {
            mNextHandler?.Play();
        }

        public abstract Uri GetPlayerUri();
    }
}
