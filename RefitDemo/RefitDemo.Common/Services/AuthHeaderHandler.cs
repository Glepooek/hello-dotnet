using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace RefitDemo.Common.Services
{
    /// <summary>
    /// DelegatingHandler that injects Bearer token from IAuthTokenStore
    /// into every outgoing HTTP request.
    ///
    /// Register via: .AddHttpMessageHandler&lt;AuthHeaderHandler&gt;()
    /// </summary>
    public class AuthHeaderHandler : DelegatingHandler
    {
        private readonly IAuthTokenStore _tokenStore;

        public AuthHeaderHandler(IAuthTokenStore tokenStore)
        {
            _tokenStore = tokenStore;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _tokenStore.GetTokenAsync(cancellationToken);

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
