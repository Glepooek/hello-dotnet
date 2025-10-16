using GalaSoft.MvvmLight.CommandWpf;
using SqlSugar;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Test.DragControl.Helper;
using Test.DragControl.Models;
using Test.DragControl.Services;
using Test.DragControl.Utils;

namespace Test.DragControl.ViewModels
{
    public class ListPageViewModel : ViewModelBase
    {
        #region Constructor

        /// <summary>
        ///  构造函数
        /// </summary>
        public ListPageViewModel(IFrameNavigationService navigationService)
        {
            mNavigationService = navigationService;

            InitCommands();
        }

        #endregion

        #region Fields

        private IFrameNavigationService mNavigationService;

        #endregion

        #region Properties

        private ObservableCollection<ListeningMaterial> mListeningMaterials;
        public ObservableCollection<ListeningMaterial> ListeningMaterials
        {
            get { return mListeningMaterials; }
            set
            {
                mListeningMaterials = value;
                RaisePropertyChanged(nameof(ListeningMaterials));
            }
        }

        private int mPageIndex = 1;
        public int PageIndex
        {
            get { return mPageIndex; }
            set
            {
                mPageIndex = value;
                RaisePropertyChanged(nameof(PageIndex));
            }
        }

        private int mPageSize = 10;
        public int PageSize
        {
            get { return mPageSize; }
            set
            {
                mPageSize = value;
                RaisePropertyChanged(nameof(PageSize));
            }
        }

        private int mMaxPageCount = 0;
        public int MaxPageCount
        {
            get { return mMaxPageCount; }
            set
            {
                mMaxPageCount = value;
                RaisePropertyChanged(nameof(MaxPageCount));
            }
        }

        #endregion

        #region Commands

        public ICommand CreateCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand ViewLoadedCommand { get; private set; }
        public ICommand PageUpdatedCommand
        {
            private set;
            get;
        }

        private void InitCommands()
        {
            PageUpdatedCommand = new RelayCommand<int>(OnPageUpdated);
            CreateCommand = new RelayCommand(OnCreate);
            EditCommand = new RelayCommand<ListeningMaterial>(OnEdit);
            ViewLoadedCommand = new RelayCommand(OnViewLoaded);
            DeleteCommand = new RelayCommand<int>(OnDelete);
        }

        private void OnEdit(ListeningMaterial material)
        {
            mNavigationService.NavigateTo(PageKeyConstant.EDIT_PAGE_KEY, material);
        }

        private void OnDelete(int materialId)
        {
            bool result = SqlSugarHelper.GetInstance().SqlSugarClient
                .Deleteable<ListeningMaterial>()
                .In<int>(materialId)
                .ExecuteCommandHasChange();
            if (result)
            {
                ListeningMaterials = GetListeningMaterials(mPageIndex);
            }
        }

        private void OnCreate()
        {
            mNavigationService.NavigateTo(PageKeyConstant.EDIT_PAGE_KEY);
        }

        private void OnViewLoaded()
        {
            ListeningMaterials = GetListeningMaterials(mPageIndex);
        }

        private void OnPageUpdated(int pageIndex)
        {
            ListeningMaterials = GetListeningMaterials(pageIndex);
        }

        #endregion

        #region Methods

        /// <summary>
        /// 从数据库加载
        /// </summary>
        private ObservableCollection<ListeningMaterial> GetListeningMaterials(int pageIndex = 1)
        {
            int totalCounts = 0;
            var currentPageDate = SqlSugarHelper.GetInstance().SqlSugarClient
                .Queryable<ListeningMaterial>()
                .OrderBy(material => material.CreateTime, OrderByType.Desc)
                .ToPageList(pageIndex, mPageSize, ref totalCounts);
            CalPageCount(totalCounts);
            var list = new ObservableCollection<ListeningMaterial>(currentPageDate);
            return list;
        }

        /// <summary>
        /// 计算总页数
        /// </summary>
        /// <param name="totalCounts">数据总条数</param>
        private void CalPageCount(int totalCounts)
        {
            var remainder = totalCounts % mPageSize;
            if (remainder != 0)
            {
                MaxPageCount = totalCounts / mPageSize + 1;
            }
            else
            {
                MaxPageCount = totalCounts / mPageSize;
            }
        }

        #endregion
    }
}
