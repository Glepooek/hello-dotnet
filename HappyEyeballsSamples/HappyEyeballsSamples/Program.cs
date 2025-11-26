using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HappyEyeballsSamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //CheckDnsResolution("k12-teaching.unipus.cn");
            //CheckDnsResolution("www.baidu.com");

            using HttpClient httpClient = HappyEyeballsHttp.CreateHttpClient("http://www.baidu.com/");
            using HttpResponseMessage response = httpClient.GetAsync("/").Result;
            string content = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content);


            Console.ReadLine();
        }

        // 快速检查域名解析结果
        public static async Task CheckDnsResolution(string hostname)
        {
            IPHostEntry entry = await Dns.GetHostEntryAsync(hostname);
            foreach (var addr in entry.AddressList)
            {
                Console.WriteLine($"{hostname} -> {addr} ({addr.AddressFamily})");
            }

            if (entry.AddressList.Any(ip => ip.AddressFamily == AddressFamily.InterNetworkV6))
            {
                Console.WriteLine("该域名支持IPv6，HttpClient可能优先使用IPv6");
            }
        }
    }
}
