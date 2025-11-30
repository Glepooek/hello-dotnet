using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RefitDemo.Common.Models;
using RefitDemo.Common.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace RefitDemo
{
    public class MainWindowViewModel : ObservableObject
    {
        private IJsonPlaceholderApi _jsonPlaceholderApi;
        public MainWindowViewModel(IJsonPlaceholderApi placeholderApi)
        {
            _jsonPlaceholderApi = placeholderApi;
        }

        private ObservableCollection<Post> posts;
        public ObservableCollection<Post> Posts
        {
            get => posts;
            set => SetProperty(ref posts, value, nameof(Posts));
        }

        private RelayCommand loadPostsCommand;
        public RelayCommand LoadPostsCommand
            => loadPostsCommand ??= new RelayCommand(LoadPosts);

        private async void LoadPosts()
        {
            try
            {
                IEnumerable<Post> posts = await _jsonPlaceholderApi.GetPostsAsync();
                Trace.WriteLine($"获取到 {posts.ToList().Count} 个帖子\n");
                Posts = new ObservableCollection<Post>(posts);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
    }
}
