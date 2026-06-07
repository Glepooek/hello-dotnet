using Refit;
using RefitDemo.Common.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RefitDemo.Common.Services
{
    public interface IJsonPlaceholderApi
    {
        /// <summary>
        /// Get all posts
        /// </summary>
        [Get("/posts")]
        Task<IEnumerable<Post>> GetPostsAsync(CancellationToken cancellationToken = default);

        [Get("/posts")]
        Task<IEnumerable<Post>> GetPostsAsync([Query(CollectionFormat.Csv)] int[] ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a single post by ID
        /// </summary>
        [Get("/posts/{id}")]
        Task<Post> GetPostAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a new post
        /// </summary>
        [Post("/posts")]
        Task<Post> CreatePostAsync([Body] Post post, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update an existing post
        /// </summary>
        [Put("/posts/{id}")]
        Task<Post> UpdatePostAsync(int id, [Body] Post post, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete a post
        /// </summary>
        [Delete("/posts/{id}")]
        Task DeletePostAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all users
        /// </summary>
        [Get("/users")]
        Task<List<User>> GetUsersAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a single user by ID
        /// </summary>
        [Get("/users/{id}")]
        Task<User> GetUserAsync(int id, CancellationToken cancellationToken = default);
    }
}
