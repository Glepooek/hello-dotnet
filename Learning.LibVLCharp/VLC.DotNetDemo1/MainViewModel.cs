using Stylet;
using System;
using System.IO;
using System.Reflection;
using Vlc.DotNet.Core;
using VLC.DotNetDemo.StatesPattern;

namespace VLC.DotNetDemo
{
    public class MainViewModel : Screen
    {
        #region Fields

        private VlcMediaPlayer mVLCPlayer;
        private MainView mMainView;
        private PlayerControl mPlayerControl;

        #endregion

        #region Methods

        protected override void OnViewLoaded()
        {
            InitPlayer();
            InitPlayerControl();
        }

        protected override void OnClose()
        {
            mPlayerControl.Dispose();
            mMainView.VlcControl.SourceProvider.Dispose();
        }

        private void InitPlayer()
        {
            mMainView = this.View as MainView;
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            // Default installation path of VideoLAN.LibVLC.Windows
            var libDirectory = new DirectoryInfo(Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));

            mMainView.VlcControl.SourceProvider.CreatePlayer(libDirectory);
            mVLCPlayer = mMainView.VlcControl.SourceProvider.MediaPlayer;
        }

        private void InitPlayerControl()
        {
            mPlayerControl = new PlayerControl(mVLCPlayer);
            mPlayerControl.Handle();
        }

        #endregion
    }
}
