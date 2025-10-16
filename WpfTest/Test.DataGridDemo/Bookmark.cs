using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DataGridDemo
{
    public class Bookmark : INotifyPropertyChanged
    {
        private string mBookmarkName;
        public string BookmarkName
        {
            get => mBookmarkName;
            set
            {
                mBookmarkName = value;
                RaisePropertyChanged(nameof(BookmarkName));
            }
        }

        private string mUserLocalId;
        public string UserLocalId
        {
            get => mUserLocalId;
            set
            {
                mUserLocalId = value;
                RaisePropertyChanged(nameof(UserLocalId));
            }
        }

        private bool mIsEditing;
        public bool IsEditing
        {
            get => mIsEditing;
            set
            {
                mIsEditing = value;
                RaisePropertyChanged(nameof(IsEditing));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
