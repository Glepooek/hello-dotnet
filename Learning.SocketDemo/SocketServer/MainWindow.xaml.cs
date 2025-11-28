using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;

namespace SocketServer
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		readonly Socket serverSocket = null;
		readonly Dictionary<string, Socket> socketDic = new Dictionary<string, Socket>();

		Thread receiveMsgThread = null;
		Thread acceptMsgThread = null;

		public MainWindow()
		{
			InitializeComponent();

			serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		}

		private void btnListen_Click(object sender, RoutedEventArgs e)
		{
			IPAddress ip = IPAddress.Parse(this.txtIP.Text);
			IPEndPoint point = new IPEndPoint(ip, int.Parse(this.txtPort.Text));

			// 绑定监听终结点
			serverSocket.Bind(point);
			// 侦听传入的连接，并指定侦听队列容量
			serverSocket.Listen(10);

			ShowMsg("服务器开始监听！");

			acceptMsgThread = new Thread(AcceptMsg)
			{
				IsBackground = true
			};
			acceptMsgThread.Start(serverSocket);
		}

		private void btnSend_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(this.txtMsg.Text))
			{
				return;
			}

			try
			{
				ShowMsg(string.Format("服务器：{0}", this.txtMsg.Text));

				byte[] buffer = Encoding.UTF8.GetBytes(this.txtMsg.Text);
				foreach (var socket in socketDic.Values)
				{
					socket.Send(buffer);
				}

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

		private void AcceptMsg(object arg)
		{
			Socket socket = arg as Socket;

			while (true)
			{
				// 为新连接创建一个新Socket
				Socket clientSocket = socket.Accept();

				if (clientSocket != null)
				{
					string point = clientSocket.RemoteEndPoint.ToString();
					ShowMsg(string.Format("客户端：{0} 连接成功", point));

					if (!socketDic.ContainsKey(point))
					{
						socketDic.Add(point, clientSocket);

						receiveMsgThread = new Thread(ReceiveMsg)
						{
							IsBackground = true
						};
						receiveMsgThread.Start(clientSocket);
					}
				}
			}
		}

		private void ReceiveMsg(object arg)
		{
			Socket socket = arg as Socket;

			try
			{
				while (true)
				{
					byte[] buffer = new byte[1024 * 1024];
					int receiveLen = socket.Receive(buffer);
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
