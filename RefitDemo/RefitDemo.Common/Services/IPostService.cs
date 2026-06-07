using RefitDemo.Common.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RefitDemo.Common.Services
{
    public interface IPostService
    {
        Task<OperationResult<IEnumerable<Post>>> GetPostsAsync(CancellationToken cancellationToken = default);
        Task<OperationResult<Post>> GetPostAsync(int id, CancellationToken cancellationToken = default);
        Task<OperationResult<Post>> CreatePostAsync(Post post, CancellationToken cancellationToken = default);
        Task<OperationResult<Post>> UpdatePostAsync(int id, Post post, CancellationToken cancellationToken = default);
        Task<OperationResult<bool>> DeletePostAsync(int id, CancellationToken cancellationToken = default);
    }
}
