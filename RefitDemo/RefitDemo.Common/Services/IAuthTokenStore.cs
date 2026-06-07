using System.Threading;
using System.Threading.Tasks;

namespace RefitDemo.Common.Services
{
    /// <summary>
    /// Provides access to the current authentication token.
    /// Implement this to supply Bearer tokens for API requests.
    /// </summary>
    public interface IAuthTokenStore
    {
        Task<string> GetTokenAsync(CancellationToken cancellationToken = default);
    }
}
