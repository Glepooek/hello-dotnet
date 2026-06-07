using Refit;
using RefitDemo.Common.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace RefitDemo.Common.Services
{
    public class PostService : IPostService
    {
        private readonly IJsonPlaceholderApi _api;

        public PostService(IJsonPlaceholderApi api)
        {
            _api = api;
        }

        public async Task<OperationResult<IEnumerable<Post>>> GetPostsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var posts = await _api.GetPostsAsync(cancellationToken);
                return OperationResult<IEnumerable<Post>>.Success(posts);
            }
            catch (ApiException ex)
            {
                Debug.WriteLine($"[PostService] GetPosts failed: {ex.StatusCode} - {ex.Message}");
                return OperationResult<IEnumerable<Post>>.Failure(
                    $"Failed to load posts ({(int)ex.StatusCode})", ex.StatusCode);
            }
        }

        public async Task<OperationResult<Post>> GetPostAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var post = await _api.GetPostAsync(id);
                return OperationResult<Post>.Success(post);
            }
            catch (ApiException ex)
            {
                Debug.WriteLine($"[PostService] GetPost({id}) failed: {ex.StatusCode} - {ex.Message}");
                return OperationResult<Post>.Failure(
                    $"Failed to load post {id} ({(int)ex.StatusCode})", ex.StatusCode);
            }
        }

        public async Task<OperationResult<Post>> CreatePostAsync(Post post, CancellationToken cancellationToken = default)
        {
            try
            {
                var created = await _api.CreatePostAsync(post);
                return OperationResult<Post>.Success(created);
            }
            catch (ApiException ex)
            {
                Debug.WriteLine($"[PostService] CreatePost failed: {ex.StatusCode} - {ex.Message}");
                return OperationResult<Post>.Failure(
                    $"Failed to create post ({(int)ex.StatusCode})", ex.StatusCode);
            }
        }

        public async Task<OperationResult<Post>> UpdatePostAsync(int id, Post post, CancellationToken cancellationToken = default)
        {
            try
            {
                var updated = await _api.UpdatePostAsync(id, post);
                return OperationResult<Post>.Success(updated);
            }
            catch (ApiException ex)
            {
                Debug.WriteLine($"[PostService] UpdatePost({id}) failed: {ex.StatusCode} - {ex.Message}");
                return OperationResult<Post>.Failure(
                    $"Failed to update post {id} ({(int)ex.StatusCode})", ex.StatusCode);
            }
        }

        public async Task<OperationResult<bool>> DeletePostAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                await _api.DeletePostAsync(id);
                return OperationResult<bool>.Success(true);
            }
            catch (ApiException ex)
            {
                Debug.WriteLine($"[PostService] DeletePost({id}) failed: {ex.StatusCode} - {ex.Message}");
                return OperationResult<bool>.Failure(
                    $"Failed to delete post {id} ({(int)ex.StatusCode})", ex.StatusCode);
            }
        }
    }
}
