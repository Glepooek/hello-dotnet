using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Test.ListBoxPage
{
    public class LoadDataBaseVM : ObservableObject
    {
        private int loadEnd;
        /// <summary>
        /// 是否结束数据加载(主动设置并触发加载完成事件)
        /// </summary>
        public int LoadEnd
        {
            get { return loadEnd; }
            set
            {
                loadEnd = value;
                RaisePropertyChanged("LoadEnd");
            }
        }

        private bool completed;
        /// <summary>
        /// 是否完成加载(为true时,不会再触发BeginLoadEvent)
        /// </summary>
        public bool Completed
        {
            get { return completed; }
            set
            {
                completed = value;
                RaisePropertyChanged("Completed");
            }
        }

        private RelayCommand beginLoadCommand;
        /// <summary>
        /// 加载数据命令
        /// </summary>
        public RelayCommand BeginLoadCommand => beginLoadCommand ?? (beginLoadCommand = new RelayCommand(LoadData));

        internal virtual void LoadData() { }
    }
}
