using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RefitDemo.Common.Services
{
    /// <summary>
    /// DelegatingHandler that logs HTTP request method, URI, and response status.
    /// Useful for debugging API calls during development.
    ///
    /// Register via: .AddHttpMessageHandler&lt;LoggingHandler&gt;()
    /// </summary>
    public class LoggingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"[HTTP] {request.Method} {request.RequestUri}");

            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

            Debug.WriteLine($"[HTTP] {(int)response.StatusCode} {response.StatusCode}");
            return response;
        }
    }
}
