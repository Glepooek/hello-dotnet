using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RefitDemo.Common.Models;
using RefitDemo.Common.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace RefitDemo
{
    public sealed class MainWindowViewModel : ObservableObject
    {
        private readonly IPostService _postService;

        public MainWindowViewModel(IPostService postService)
        {
            _postService = postService;
        }

        private ObservableCollection<Post> _posts;
        public ObservableCollection<Post> Posts
        {
            get => _posts;
            set => SetProperty(ref _posts, value);
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        private bool _hasError;
        public bool HasError
        {
            get => _hasError;
            set => SetProperty(ref _hasError, value);
        }

        private AsyncRelayCommand _loadPostsCommand;
        public AsyncRelayCommand LoadPostsCommand
            => _loadPostsCommand ??= new AsyncRelayCommand(LoadPostsAsync);

        private AsyncRelayCommand<int> _deletePostCommand;
        public AsyncRelayCommand<int> DeletePostCommand
            => _deletePostCommand ??= new AsyncRelayCommand<int>(DeletePostAsync);

        private async Task LoadPostsAsync()
        {
            IsLoading = true;
            HasError = false;
            ErrorMessage = null;

            var result = await _postService.GetPostsAsync();

            if (result.IsSuccess)
            {
                Posts = new ObservableCollection<Post>(result.Data);
            }
            else
            {
                HasError = true;
                ErrorMessage = result.ErrorMessage;
            }

            IsLoading = false;
        }

        private async Task DeletePostAsync(int postId)
        {
            var result = await _postService.DeletePostAsync(postId);

            if (result.IsSuccess)
            {
                var post = Posts.FirstOrDefault(p => p.Id == postId);
                if (post != null)
                {
                    Posts.Remove(post);
                }
            }
            else
            {
                HasError = true;
                ErrorMessage = result.ErrorMessage;
            }
        }
    }
}
