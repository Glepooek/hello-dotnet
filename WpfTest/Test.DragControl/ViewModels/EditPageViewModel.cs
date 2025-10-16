using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using System;
using System.Windows.Input;
using Test.DragControl.Helper;
using Test.DragControl.Models;
using Test.DragControl.Services;
using Test.DragControl.Utils;

// CanExecute不能很好地工作
// https://www.cnblogs.com/HelloMyWorld/p/4580514.html
// 在.Net4.5或以上版本使用对应的MvvmLight版本时，修改命名空间GalaSoft.MvvmLight.Command为GalaSoft.MvvmLight.CommandWpf。

namespace Test.DragControl.ViewModels
{
    public class EditPageViewModel : ViewModelBase
    {
        #region Constructor

        /// <summary>
        ///  构造函数
        /// </summary>
        public EditPageViewModel(IFrameNavigationService navigationService)
        {
            mNavigationService = navigationService;

            InitCommands();
        }

        #endregion

        #region Fields

        /// <summary>
        /// 是否正在播放音频
        /// </summary>
        private bool mIsAudioPlaying = false;
        private IFrameNavigationService mNavigationService;
        private NAudioPlayer mPlayer = null;

        #endregion

        #region Properties

        private ListeningMaterial mListeningMaterial;
        public ListeningMaterial ListeningMaterial
        {
            get { return mListeningMaterial; }
            set
            {
                mListeningMaterial = value;
                RaisePropertyChanged(nameof(ListeningMaterial));
            }
        }

        #endregion

        #region Commands

        public ICommand AddCommand
        {
            private set;
            get;
        }

        public ICommand SaveCommand
        {
            private set;
            get;
        }

        public ICommand PlayCommand
        {
            private set;
            get;
        }

        public ICommand MoveUpCommand
        {
            private set;
            get;
        }

        public ICommand MoveDownCommand
        {
            private set;
            get;
        }

        public ICommand RemoveCommand
        {
            private set;
            get;
        }

        public ICommand ViewLoadedCommand
        {
            private set;
            get;
        }

        public ICommand ViewUnloadedCommand
        {
            private set;
            get;
        }

        public ICommand GobackCommand
        {
            private set;
            get;
        }

        private void InitCommands()
        {
            AddCommand = new RelayCommand<int>(OnAdd);
            SaveCommand = new RelayCommand(OnSave, CanSave);
            PlayCommand = new RelayCommand<ListeningContent>(OnPlay, CanPlay);
            MoveUpCommand = new RelayCommand<ListeningContent>(OnMoveUp);
            MoveDownCommand = new RelayCommand<ListeningContent>(OnMoveDown);
            RemoveCommand = new RelayCommand<ListeningContent>(OnRemove);
            ViewLoadedCommand = new RelayCommand(OnViewLoaded);
            ViewUnloadedCommand = new RelayCommand(OnViewUnloaded);
            GobackCommand = new RelayCommand(OnGoback);
        }

        private void OnAdd(int contentType)
        {
            var material = CreateNewListeningContent(contentType);
            ListeningMaterial.ContentList.Add(material);
        }

        private bool CanPlay(ListeningContent listeningContent)
        {
            return listeningContent is ListeningTextContent text
              && !string.IsNullOrEmpty(text.Content);
        }

        /// <summary>
        /// 段落播放
        /// </summary>
        /// <remarks>
        /// 1、将文本传给腾讯TTS，获取到base64字符串
        /// 2、将base64字符串保存为mp3文件
        /// 3、用NAudio加载并播放mp3文件
        /// 4、播放文件时更新UI
        /// 5、上传mp3文件到oss
        /// </remarks>
        /// <param name="listeningContent"></param>
        private void OnPlay(ListeningContent listeningContent)
        {
            if (mPlayer == null)
            {
                string rootPath
                    = $"{AppDomain.CurrentDomain.BaseDirectory}\\Materials";
                var fileName = $"{rootPath}\\test3.mp3";
                mPlayer = new NAudioPlayer(fileName);

                if (listeningContent is ListeningTextContent text)
                {
                    text.Audio.AudioTotalTime = mPlayer.TotalTime;
                    mPlayer.ProgressAction += (currentTime, playProgressValue) =>
                    {
                        text.Audio.AudioCurrentTime = currentTime;
                        text.Audio.AudioProgressValue = playProgressValue;
                    };
                    mPlayer.StopNotifactionAction += () =>
                    {
                        mIsAudioPlaying = false;
                    };
                }
            }

            if (mIsAudioPlaying)
            {
                mPlayer.Pause();
                mIsAudioPlaying = false;
            }
            else
            {
                mPlayer.Play();
                mIsAudioPlaying = true;
            }
        }

        private void OnMoveUp(ListeningContent listeningContent)
        {
            int index = ListeningMaterial.ContentList.IndexOf(listeningContent);
            if (index > 0)
            {
                ListeningMaterial.ContentList.Move(index, index - 1);
            }
        }

        private void OnMoveDown(ListeningContent listeningContent)
        {
            int index = ListeningMaterial.ContentList.IndexOf(listeningContent);
            if (index < ListeningMaterial.ContentList.Count - 1)
            {
                ListeningMaterial.ContentList.Move(index, index + 1);
            }
        }

        private void OnRemove(ListeningContent listeningContent)
        {
            ListeningMaterial.ContentList.Remove(listeningContent);
        }

        private bool CanSave()
        {
            return true;
        }

        private void OnSave()
        {
            ListeningMaterial.ContentJson = JsonConvert.SerializeObject(ListeningMaterial.ContentList);
            ListeningMaterial.UpdateTime = DateTime.Now;
            bool result = false;
            if (ListeningMaterial.MaterialId > 0)
            {
                result = SqlSugarHelper.GetInstance().SqlSugarClient
                    .Updateable<ListeningMaterial>(ListeningMaterial)
                    .Where(material => material.MaterialId == ListeningMaterial.MaterialId)
                    .IgnoreColumns(material => new { material.MaterialId, material.CreateTime })
                    .ExecuteCommandHasChange();
            }
            else
            {
                result = SqlSugarHelper.GetInstance().SqlSugarClient
                    .Insertable<ListeningMaterial>(ListeningMaterial)
                    .ExecuteCommand() > 0;
            }

            if (result)
            {
                mNavigationService.NavigateTo(PageKeyConstant.LIST_PAGE_KEY);
            }
        }

        private void OnViewLoaded()
        {
            if (mNavigationService.Parameter is ListeningMaterial material)
            {
                ListeningMaterial = material;
            }
            else
            {
                if (ListeningMaterial == null)
                {
                    ListeningMaterial = new ListeningMaterial();
                }
            }
        }

        private void OnViewUnloaded()
        {
            ListeningMaterial = null;
        }

        private void OnGoback()
        {
            mNavigationService.NavigateTo(PageKeyConstant.LIST_PAGE_KEY);
        }

        #endregion

        #region Methods

        private ListeningContent CreateNewListeningContent(int contentType)
        {
            ListeningContent superTextBox;
            switch (contentType)
            {
                case 0:
                    {
                        superTextBox = new ListeningTextContent()
                        {
                            Title = "独白",
                            Tag = "这里是独白材料",
                            ContentType = 0,
                            Audio = new AudioInfo()
                        };
                        break;
                    }
                case 1:
                    {
                        superTextBox = new ListeningTextContent()
                        {
                            Title = "对话",
                            Tag = "这里是对话材料",
                            ContentType = 1,
                            Audio = new AudioInfo()
                        };
                        break;
                    }
                case 2:
                    {
                        superTextBox = new ListeningTextContent()
                        {
                            Title = "指导语",
                            Tag = "这里是指导语材料",
                            ContentType = 2,
                            Audio = new AudioInfo()
                        };
                        break;
                    }
                default:
                    superTextBox = new ListeningPauseContent()
                    {
                        Title = "停顿时长",
                        Tag = "输入时间"
                    };
                    break;
            };

            return superTextBox;
        }

        #endregion
    }
}
