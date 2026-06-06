using System;
using System.Collections.Generic;
using System.Text;

namespace RefitDemo
{
    internal class RefitOptions
    {
        public string BaseUrl { get; set; }
        public Uri BaseUri { get; set; }
        public long TimeoutSeconds { get; set; } = 20;
        public string MediaType { get; set; } = "application/json";
    }
}