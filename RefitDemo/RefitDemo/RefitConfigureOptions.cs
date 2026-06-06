using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace RefitDemo
{
    internal class RefitConfigureOptions : IConfigureOptions<RefitOptions>
    {
        private const string ApiBaseUrl = "https://jsonplaceholder.typicode.com";

        public void Configure(RefitOptions options)
        {
            options.BaseUrl = ApiBaseUrl;
            options.BaseUri = new Uri(ApiBaseUrl);
        }
    }
}
