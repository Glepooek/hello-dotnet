using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;

namespace SocketClient
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		readonly Socket clientSocket = null;
		Thread receiveMsgThread = null;

		public MainWindow()
		{
			InitializeComponent();

			clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
			{
				// 不使用Nagle algorithm。
				// Nagle algorithm可以提高网络吞吐量，但是实时性却降低了。
				NoDelay = true,
				ReceiveBufferSize = 6 * 1024 * 1024
			};
		}

		private void btnConnect_Click(object sender, RoutedEventArgs e)
		{
			IPAddress ip = IPAddress.Parse(this.txtIP.Text);
			IPEndPoint point = new IPEndPoint(ip, int.Parse(this.txtPort.Text));

			clientSocket.Connect(point);

			ShowMsg("与服务器连接成功！");
			ShowMsg(string.Format("服务器：{0}", clientSocket.RemoteEndPoint.ToString()));
			ShowMsg(string.Format("客户端：{0}", clientSocket.LocalEndPoint.ToString()));

			receiveMsgThread = new Thread(new ThreadStart(ReceiveMsg))
			{
				IsBackground = true
			};
			receiveMsgThread.Start();

			ShowMsg("接收消息线程启动成功！");
		}

		private void btnSend_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(this.txtMsg.Text))
			{
				return;
			}

			try
			{
				ShowMsg(string.Format("客户端：{0}", this.txtMsg.Text));

				byte[] buffer = Encoding.UTF8.GetBytes(this.txtMsg.Text);
				clientSocket.Send(buffer);

				this.txtMsg.Text = string.Empty;
			}
			catch (Exception ex)
			{
				ShowMsg(ex.Message);
			}
		}

		private void ShowMsg(string msg)
		{
			this.Dispatcher.Invoke(new Action(() =>
			{
				this.txtMsgBord.AppendText(string.Format("{0}\r\n", msg));
			}));
		}

		private void ReceiveMsg()
		{
			try
			{
				while (true)
				{
					byte[] buffer = new byte[1024 * 1024];
					int receiveLen = clientSocket.Receive(buffer);
					string msg = Encoding.UTF8.GetString(buffer, 0, receiveLen);
					ShowMsg(msg);
				}
			}
			catch (Exception ex)
			{
				ShowMsg(ex.Message);
			}
		}
	}
}
