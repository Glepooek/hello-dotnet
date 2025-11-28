using System;
using System.Threading.Tasks;
using Vlc.DotNet.Core;

namespace VLC.DotNetDemo.ChainPattern
{
    public class TransitionOutHandler : TransitionHandler
    {
        private string mTransitionOutFilePath =
            AppDomain.CurrentDomain.BaseDirectory + @"Videos\transition_end.mov";
        private Uri mTransitionOutUri;

        public TransitionOutHandler(VlcMediaPlayer vlcMediaPlayer) : base(vlcMediaPlayer)
        {
            mTransitionOutUri = new Uri(mTransitionOutFilePath);
        }

        public override void Play()
        {
            Task.Run(() =>
            {
                mVlcMediaPlayer?.Play(mTransitionOutUri);
            });
        }

        public override Uri GetPlayerUri()
        {
            return mTransitionOutUri;
        }
    }
}
