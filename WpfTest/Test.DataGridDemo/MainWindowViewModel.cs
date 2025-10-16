using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

// 保存按钮无法执行

namespace Test.DataGridDemo
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Fields
        private DataGrid mDataGrid;
        #endregion

        #region Properties
        private ObservableCollection<Bookmark> mBookmarks;
        public ObservableCollection<Bookmark> Bookmarks
        {
            get { return mBookmarks; }
            set
            {
                mBookmarks = value;
                RaisePropertyChanged(nameof(Bookmarks));
            }
        }

        private Bookmark mCurrentBookmark;
        public Bookmark CurrentBookmark
        {
            get { return mCurrentBookmark; }
            set
            {
                mCurrentBookmark = value;
                RaisePropertyChanged(nameof(CurrentBookmark));
            }
        }
        #endregion

        #region Commands
        public RelayCommand<DataGrid> LoadedCommand { get; private set; }
        public RelayCommand AddCommand { get; private set; }
        private RelayCommand<Bookmark> mSaveOrUpdateCommand;
        public RelayCommand<Bookmark> SaveOrUpdateCommand => mSaveOrUpdateCommand ??= new RelayCommand<Bookmark>(OnSaveOrUpdate);
        public RelayCommand<Bookmark> EditCommand { get; private set; }
        public RelayCommand<Bookmark> DeleteCommand { get; private set; }
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            mBookmarks = new ObservableCollection<Bookmark>();
            LoadedCommand = new RelayCommand<DataGrid>(OnLoaded);
            AddCommand = new RelayCommand(OnAdd, CanAdd);
            //SaveOrUpdateCommand = new RelayCommand<Bookmark>(OnSaveOrUpdate);
            EditCommand = new RelayCommand<Bookmark>(OnEdit);
            DeleteCommand = new RelayCommand<Bookmark>(OnDelete);
        }
        #endregion

        #region Methods
        private void OnLoaded(DataGrid grid)
        {
            mDataGrid = grid;
        }

        private void OnAdd()
        {
            Bookmark bookmark = new Bookmark();
            Bookmarks.Insert(0, bookmark);

            StartEdit(bookmark);
        }

        private bool CanAdd()
        {
            return CurrentBookmark == null || !CurrentBookmark.IsEditing;
        }

        private async void OnSaveOrUpdate(Bookmark bookmark)
        {
            if (string.IsNullOrEmpty(bookmark.BookmarkName))
            {
                MessageBox.Show("书签名称不能为空");
                StartEdit(bookmark);
                return;
            }

            if (bookmark.BookmarkName.Length > 40)
            {
                MessageBox.Show("书签名称长度不能超过40个字符");
                StartEdit(bookmark);
                return;
            }

            if (string.IsNullOrEmpty(bookmark.UserLocalId))
            {
                bookmark.UserLocalId = Guid.NewGuid().ToString();
            }
            else
            {

            }

            await Task.Delay(100);
            (AddCommand as RelayCommand).NotifyCanExecuteChanged();
        }

        private void OnEdit(Bookmark bookmark)
        {
            StartEdit(bookmark);
        }

        private void OnDelete(Bookmark bookmark)
        {
            if (Bookmarks.Contains(bookmark))
            {
                Bookmarks.Remove(bookmark);
            }
        }

        private async void StartEdit(Bookmark bookmark)
        {
            CurrentBookmark = bookmark;
            await Task.Delay(100);
            mDataGrid.Dispatcher.Invoke(() =>
            {
                mDataGrid.CurrentCell = new DataGridCellInfo(bookmark, mDataGrid.Columns[0]);
                mDataGrid.BeginEdit();
            });

            (AddCommand as RelayCommand).NotifyCanExecuteChanged();
        }

        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
