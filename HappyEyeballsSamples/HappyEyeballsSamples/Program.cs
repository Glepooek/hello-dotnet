using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

await CheckDnsResolution("k12-teaching.unipus.cn");
await CheckDnsResolution("www.baidu.com");

//using HttpClient httpClient = HappyEyeballsHttp.CreateHttpClient("http://www.baidu.com/");
//using HttpResponseMessage response = httpClient.GetAsync("/").Result;
//string content = response.Content.ReadAsStringAsync().Result;
//Console.WriteLine(content);

Console.ReadLine();

/// <summary>
/// 快速检查域名解析结果
/// <summary> 
static async Task CheckDnsResolution(string hostname)
{
    IPAddress[] entry = await Dns.GetHostAddressesAsync(hostname);
    foreach (var addr in entry)
    {
        Console.WriteLine($"{hostname} -> {addr} ({addr.AddressFamily})");
    }

    if (entry.Any(ip => ip.AddressFamily == AddressFamily.InterNetworkV6))
    {
        Console.WriteLine("该域名支持IPv6，HttpClient可能优先使用IPv6");
    }
}