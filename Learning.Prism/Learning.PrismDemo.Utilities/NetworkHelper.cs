using System;
using System.Linq;
using System.Net.NetworkInformation;

namespace Learning.PrismDemo.Utilities
{
	public class NetworkHelper
	{
		#region Singleton

		/// <summary>
		/// 类<see cref="NetworkHelper"/>的实例
		/// </summary>
		public static NetworkHelper Instance
		{
			get { return SingletonProvider<NetworkHelper>.Instance; }
		}

		#endregion

		#region Properties

		/// <summary>
		/// 网络连接可用状态更改委托
		/// </summary>
		public Action<bool> NetworkStateChangedAction;

		/// <summary>
		/// 指示是否有任何网络连接可用
		/// </summary>
		/// <remarks>
		/// 如果任何网络接口被标记为“打开”并且不是环回或隧道接口，则认为网络连接可用。
		/// </remarks>
		public bool IsNetworkAvailable { get; set; } = NetworkInterface.GetIsNetworkAvailable();

		/// <summary>
		/// 本机所有能传输数据的网卡信息
		/// </summary>
		public NetworkInterface[] NetworkInterfaces { get; set; } = NetworkInterface.GetAllNetworkInterfaces()
				.Where(N => N.NetworkInterfaceType != NetworkInterfaceType.Loopback
							&& N.OperationalStatus == OperationalStatus.Up).ToArray();

		#endregion

		#region Constructor

		private NetworkHelper()
		{
			NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
		}

		#endregion

		#region Methods

		void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
		{
			IsNetworkAvailable = e.IsAvailable;
			NetworkStateChangedAction?.Invoke(e.IsAvailable);
			NetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces().Where(N => N.NetworkInterfaceType != NetworkInterfaceType.Loopback && N.OperationalStatus == OperationalStatus.Up).ToArray();
		}

		/// <summary>
		/// 获取当前传输数据的网卡信息
		/// </summary>
		/// <remarks>
		/// 如果同时连接有线和无线，返回的网卡信息为有线网卡信息
		/// </remarks>
		/// <returns></returns>
		public NetworkInterface GetCurrentNetworkInterface()
		{
			if (NetworkInterfaces == null || NetworkInterfaces.Length == 0)
			{
				return null;
			}

			if (NetworkInterfaces.Any(n => n.NetworkInterfaceType == NetworkInterfaceType.Ethernet))
			{
				return NetworkInterfaces.First(n => n.NetworkInterfaceType == NetworkInterfaceType.Ethernet);
			}
			else
			{
				return NetworkInterfaces.FirstOrDefault(n => n.NetworkInterfaceType == NetworkInterfaceType.Wireless80211);
			}
		}

		/// <summary>
		/// 判断Wifi是否已连接
		/// </summary>
		/// <returns></returns>
		public bool IsWifiConnected()
		{
			if (NetworkInterfaces == null || NetworkInterfaces.Length == 0)
			{
				return false;
			}

			NetworkInterface nface = NetworkInterfaces.First(x => x.NetworkInterfaceType == NetworkInterfaceType.Wireless80211);

			if (nface == null)
			{
				return false;
			}

			var ipProperties = nface.GetIPProperties();
			// 获取默认网关
			var defualtGateway = ipProperties.GatewayAddresses[0];
			Ping ping = new Ping();
			var pingReplyTask = ping.SendPingAsync(defualtGateway.Address);
			var reply = pingReplyTask.Result;

			if (reply != null && reply.Status == IPStatus.Success)
			{
				return true;
			}

			return false;
		}

		#endregion
	}
}
