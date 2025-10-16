using NAudio.Wave;
using System;
using System.Windows.Threading;

namespace Test.DragControl.Helper
{
    public class NAudioPlayer : IDisposable
    {
        #region Constructor
        /// <summary>
        /// 实例化播放音效帮助类
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="isLoop">是否循环</param>
        public NAudioPlayer(string fileName, bool isLoop = false)
        {
            mIsLoop = isLoop;
            mPlayer = new WaveOutEvent();
            mPlayer.PlaybackStopped += OnPlaybackStopped;
            mFileileReader = new Mp3FileReader(fileName);
            mTimer = new DispatcherTimer(DispatcherPriority.Background)
            {
                Interval = TimeSpan.FromMilliseconds(100),
            };
            mTimer.Tick += OnDispatcherTimerTick;
            TotalTime = mFileileReader.TotalTime;

            try
            {
                mPlayer.Init(mFileileReader);
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region Fields
        /// <summary>
        /// 播放器对象
        /// </summary>
        private IWavePlayer mPlayer;
        /// <summary>
        /// 文件读取对象
        /// </summary>
        private Mp3FileReader mFileileReader;
        /// <summary>
        /// 是否循环播放
        /// </summary>
        private bool mIsLoop;
        /// <summary>
        /// 定时器
        /// </summary>
        private DispatcherTimer mTimer;
        /// <summary>
        /// 播放进度委托
        /// </summary>
        public Action<TimeSpan, double> ProgressAction;
        /// <summary>
        /// 播放结束通知委托
        /// </summary>
        public Action StopNotifactionAction;
        #endregion

        #region Properties

        /// <summary>
        /// 音频总时长
        /// </summary>
        public TimeSpan TotalTime { get; private set; }

        #endregion

        #region Public Methods
        /// <summary>
        /// 开始播放
        /// </summary>
        public void Play()
        {
            try
            {
                mTimer.Start();
                mPlayer.Play();
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// 停止播放
        /// </summary>
        public void Stop()
        {
            try
            {
                mPlayer.Stop();
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// 暂停
        /// </summary>
        public void Pause()
        {
            try
            {
                mTimer.Stop();
                mPlayer.Pause();
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region IDispose
        /// <summary>
        /// 释放播放器资源
        /// </summary>
        public void Dispose()
        {
            try
            {
                if (mPlayer != null)
                {
                    mPlayer.PlaybackStopped -= OnPlaybackStopped;
                    mPlayer.Dispose();
                    mPlayer = null;
                }
                if (mFileileReader != null)
                {
                    mFileileReader.Dispose();
                    mFileileReader = null;
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// 播放结束时调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (e.Exception == null)
            {
                try
                {
                    if (mIsLoop)
                    {
                        mFileileReader.CurrentTime = TimeSpan.FromSeconds(0);
                        mPlayer.Play();
                    }
                    else
                    {
                        StopNotifactionAction?.Invoke();
                        mTimer.Stop();
                        mPlayer.Dispose();
                        mFileileReader.Dispose();
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void OnDispatcherTimerTick(object sender, EventArgs e)
        {
            var currentTime = (mPlayer.PlaybackState == PlaybackState.Stopped) ? TimeSpan.Zero : mFileileReader.CurrentTime;
            var grogressValue = Math.Min(100, (int)(100 * mFileileReader.CurrentTime.TotalSeconds / mFileileReader.TotalTime.TotalSeconds));
            ProgressAction?.Invoke(currentTime, grogressValue);
        }

        #endregion
    }
}
