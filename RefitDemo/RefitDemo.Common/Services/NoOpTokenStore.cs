using System.Threading;
using System.Threading.Tasks;

namespace RefitDemo.Common.Services
{
    /// <summary>
    /// Default IAuthTokenStore that returns no token.
    /// Replace with a real implementation (e.g., reading from secure storage)
    /// when authentication is needed.
    /// </summary>
    public class NoOpTokenStore : IAuthTokenStore
    {
        public Task<string> GetTokenAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult<string>(null);
        }
    }
}
