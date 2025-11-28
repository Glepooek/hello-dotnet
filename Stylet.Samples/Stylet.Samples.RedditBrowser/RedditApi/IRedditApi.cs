using RestEase;
using Stylet.Samples.RedditBrowser.RedditApi.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stylet.Samples.RedditBrowser.RedditApi
{
    public interface IRedditApi
    {
        [Get("/r/{subreddit}/{mode}.json")]
        Task<PostsResponse> FetchPostsAsync([Path] string subreddit, [Path] string mode);

        [Get("/r/{subreddit}/{mode}.json?after={after}&count={count}")]
        Task<PostsResponse> FetchNextPostsAsync([Path] string subreddit, [Path] string mode, [Path] string after, [Path] int count);

        [Get("/r/{subreddit}/{mode}.json?before={before}&count={count}")]
        Task<PostsResponse> FetchPrevPostsAsync([Path] string subreddit, [Path] string mode, [Path] string before, [Path] int count);

        [Get("/r/{subreddit}/comments/{postId}.json")]
        Task<List<CommentsResponse>> FetchCommentsAsync([Path] string subreddit, [Path] string postId);
    }
}
