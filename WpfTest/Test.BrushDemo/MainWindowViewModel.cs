using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Test.BrushDemo
{
    internal class MainWindowViewModel : ObservableObject
    {
        private ObservableCollection<BookInfo> bookList;
        public ObservableCollection<BookInfo> BookList
        {
            get => bookList;
            set => SetProperty(ref bookList, value);
        }

        public RelayCommand AddBookCommand => new RelayCommand(() =>
        {
            BookList.Add(new BookInfo { VolumeName = "New Book " + (BookList.Count + 1) });
        });

        public RelayCommand RemoveBookCommand => new RelayCommand(() =>
        {
            if (BookList.Count > 0)
            {
                BookList.RemoveAt(BookList.Count - 1);
            }
        });

        public MainWindowViewModel()
        {
            BookList = new ObservableCollection<BookInfo>
            {
                new BookInfo { VolumeName = "The Great Gatsby" },
                new BookInfo { VolumeName = "To Kill a Mockingbird" },
                new BookInfo { VolumeName = "1984" },
                new BookInfo { VolumeName = "Pride and Prejudice" },
                new BookInfo { VolumeName = "The Catcher in the Rye" }
            };
        }
    }
}
