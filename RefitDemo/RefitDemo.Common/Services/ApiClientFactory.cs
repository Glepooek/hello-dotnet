using Refit;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RefitDemo.Common.Services
{
    /// <summary>
    /// Static factory for creating Refit API clients as an alternative to DI registration.
    ///
    /// Preferred approach: use <c>services.AddRefitClient&lt;T&gt;()</c> in DI (see App.xaml.cs).
    /// This factory is useful for simple scenarios, unit tests, or when DI is not available.
    /// </summary>
    public static class ApiClientFactory
    {
        public const string NamedClientName = "RefitDemo";

        /// <summary>
        /// Create a Refit client using IHttpClientFactory (recommended).
        /// The named client "RefitDemo" must be registered in DI.
        /// </summary>
        public static T Create<T>(IHttpClientFactory httpClientFactory)
        {
            var httpClient = httpClientFactory.CreateClient(NamedClientName);

            return RestService.For<T>(httpClient, new RefitSettings()
            {
                CollectionFormat = CollectionFormat.Csv,
                UrlParameterFormatter = new CustomDateUrlParameterFormatter()
            });
        }

        public static T Create<T>(string baseUrl, HttpClientHandler httpClientHandler)
        {
            HttpClient httpClient = new HttpClient(httpClientHandler)
            {
                BaseAddress = new Uri(baseUrl),
                Timeout = TimeSpan.FromSeconds(30)
            };

            // 添加默认请求头
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return RestService.For<T>(httpClient);
        }
    }
}
