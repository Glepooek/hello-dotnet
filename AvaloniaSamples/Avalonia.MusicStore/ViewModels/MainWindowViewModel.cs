using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.MusicStore.Helpers;
using Avalonia.MusicStore.Views;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Avalonia.MusicStore.Models;
using Avalonia.MusicStore.Messages;

namespace Avalonia.MusicStore.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IRecipient<MessageParam>
    {
        #region Properties

        public ObservableCollection<AlbumViewModel> Albums { get; } = new();

        public ObservableCollection<Person> People { get; } = new();

        public string Text { get; set; }

        #endregion

        #region Commands

        public RelayCommand LoadedCommand { get; private set; }
        public RelayCommand UnloadedCommand { get; private set; }
        public RelayCommand ShowAlbumsCommand { get; private set; }
        public RelayCommand ShowWebCommand { get; private set; }
        public RelayCommand CallJSMethodCommand { get; private set; }

        #endregion

        #region Constructor

        public MainWindowViewModel()
        {
            LoadedCommand = new RelayCommand(() =>
            {
                LoadAlbums();
            });

            UnloadedCommand = new RelayCommand(() =>
            {
                WeakReferenceMessenger.Default.UnregisterAll(this);
            });

            ShowWebCommand = new RelayCommand(() =>
            {
                WebViewWindow dialog = new WebViewWindow();
                //dialog.ShowDialog(AvaloniaHelper.GetMainWindow());
                dialog.Show(AvaloniaHelper.GetMainWindow());
            });

            CallJSMethodCommand = new RelayCommand(() =>
            {
                WeakReferenceMessenger.Default.Send<MessageParam>(new MessageParam { Reult = true });
            });

            ShowAlbumsCommand = new RelayCommand(() =>
            {
                MusicStoreWindow dialog = new MusicStoreWindow();
                dialog.ShowDialog(AvaloniaHelper.GetMainWindow());
            });

            Text = " <h1>欢迎来到我的网页</h1>\r\n    <p>这是一个段落。你可以在这里添加内容。</p>\r\n    <p>学习HTML是创建网页的第一步。</p>\r\n";

            People.Add(new Person() { Id = "10", Name = "anyu", Address = "Beijing", Sex = Sex.Male });
            People.Add(new Person() { Id = "20", Name = "lff", Address = "Beijing", Sex = Sex.Female });

            WeakReferenceMessenger.Default.Register<MessageParam>(this);
        }

        public async void Receive(MessageParam message)
        {
            if (message == null)
            {
                return;
            }

            if (message.Data is AlbumViewModel albumVM
                && !Albums.Contains(albumVM))
            {
                Albums.Add(albumVM);
                await albumVM.SaveToDiskAsync();
            }
        }

        public async void LoadAlbums()
        {
            var albums = (await Album.LoadCachedAsync()).Select(x => new AlbumViewModel(x));

            foreach (var album in albums)
            {
                Albums.Add(album);
            }

            foreach (var album in Albums.ToList())
            {
                await album.LoadCover();
            }
        }

        #endregion
    }
}
