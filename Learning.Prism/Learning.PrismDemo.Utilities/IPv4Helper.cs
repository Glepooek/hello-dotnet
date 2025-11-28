using System;
using System.Net;

namespace Learning.PrismDemo.Utilities
{
	/// <summary>
	/// IPv4帮助类
	/// </summary>
	/// <remarks>
	/// “ 内网保留地址”定义如下：
	/// 1. 10.0.0.0/8，即 10.0.0.0- 10.255.255.255;
	/// 2. 172.16.0.0/12，即 172.16.0.0- 172.31.255.255;
	/// 3. 192.168.0.0/16，即 192.168.0.0- 192.168.255.255。
	/// </remarks>
	public class IPv4Helper
	{
		/// <summary>
		/// 判断IP地址是否为内网
		/// </summary>
		/// <param name="ipv4Address"></param>
		/// <returns></returns>
		public static bool IsPrivateNetwork(string ipv4Address)
		{
			if (IPAddress.TryParse(ipv4Address, out _))
			{
				if (ipv4Address.StartsWith("192.168.") || ipv4Address.StartsWith("10."))
				{
					return true;
				}

				if (ipv4Address.StartsWith("172."))
				{
					string seg2 = ipv4Address[4..7];
					if (seg2.EndsWith('.') &&
						String.Compare(seg2, "16.") >= 0 &&
						String.Compare(seg2, "31.") <= 0)
					{
						return true;
					}
				}
			}

			return false;
		}

		public static bool IsPrivateNetwork2(string ipv4Address) => IPAddress.TryParse(ipv4Address, out _)
			&& (ipv4Address.StartsWith("192.168.")
						|| ipv4Address.StartsWith("10.")
						|| (ipv4Address.StartsWith("172.")
									&& ipv4Address[6] == '.'
									&& int.Parse(ipv4Address[4..6]) switch
									{
										var x when x >= 16 && x <= 31 => true,
										_ => false
									}));

		public static bool IsPrivateNetwork3(string ipv4Address)
		{
			if (IPAddress.TryParse(ipv4Address, out var ip))
			{
				byte[] ipBytes = ip.GetAddressBytes();
				if (ipBytes[0] == 10)
					return true;
				if (ipBytes[0] == 172 && ipBytes[1] >= 16 && ipBytes[1] <= 31)
					return true;
				if (ipBytes[0] == 192 && ipBytes[1] == 168)
					return true;
			}

			return false;
		}

		public static bool IsPrivateNetwork4(string ipv4Address) => IPAddress.TryParse(ipv4Address, out var ip) && ip.GetAddressBytes() switch
		{
			var x when x[0] == 10 => true,
			var x when x[0] == 172 && x[1] >= 16 && x[1] <= 31 => true,
			var x when x[0] == 192 && x[1] == 168 => true,
			_ => false
		};
	}
}
