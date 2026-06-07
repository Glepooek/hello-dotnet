using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace RefitDemo
{
    internal class RefitConfigureOptions : IConfigureOptions<RefitOptions>
    {
        private readonly IConfiguration _configuration;

        public RefitConfigureOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(RefitOptions options)
        {
            var section = _configuration.GetSection("Refit");
            var baseUrl = section["BaseUrl"] ?? "https://jsonplaceholder.typicode.com";

            options.BaseUrl = baseUrl;
            options.BaseUri = new Uri(baseUrl);
            options.TimeoutSeconds = section.GetValue<long>("TimeoutSeconds", 20);
        }
    }
}
