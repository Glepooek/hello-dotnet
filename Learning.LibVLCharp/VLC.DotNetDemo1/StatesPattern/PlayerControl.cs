using System;
using System.Threading.Tasks;
using Vlc.DotNet.Core;

namespace VLC.DotNetDemo.StatesPattern
{
    public class PlayerControl
    {
        #region Fields

        private TransitionState mTransitionState;
        private VlcMediaPlayer mVLCPlayer;
        private TransitionInState mTransitionInState;
        private TransitionOutState mTransitionOutState;
        private TransitionTimerState mTransitionTimerState;

        #endregion

        #region Constructor

        public PlayerControl(VlcMediaPlayer vlcMediaPlayer)
        {
            mVLCPlayer = vlcMediaPlayer;
            mVLCPlayer.EndReached += OnMediaEndReached;
            InitTransitionState();
            ChangeState(mTransitionInState);
        }

        #endregion

        #region Public Methods

        public void ChangeState(TransitionState transitionState)
        {
            mTransitionState = transitionState;
        }

        public void Handle()
        {
            mTransitionState?.Play();
        }

        public void Dispose()
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
        }

        #endregion

        #region Private Methods

        private void InitTransitionState()
        {
            mTransitionInState = new TransitionInState(mVLCPlayer);
            mTransitionTimerState = new TransitionTimerState(mVLCPlayer);
            mTransitionOutState = new TransitionOutState(mVLCPlayer);
        }

        private void OnMediaEndReached(object sender, EventArgs e)
        {
            if (sender is VlcMediaPlayer player)
            {
                var media = player.GetMedia();
                if (media.Mrl == mTransitionOutState.GetUri().AbsoluteUri)
                {
                    return;
                }

                if (media.Mrl == mTransitionInState.GetUri().AbsoluteUri)
                {
                    ChangeState(mTransitionTimerState);
                }
                else if (media.Mrl == mTransitionTimerState.GetUri().AbsoluteUri)
                {
                    ChangeState(mTransitionOutState);
                }

                Handle();
            }
        }

        #endregion
    }
}
