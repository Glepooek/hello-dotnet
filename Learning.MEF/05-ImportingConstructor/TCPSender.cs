using System;
using System.ComponentModel.Composition;

namespace _05_ImportingConstructor
{
	[Export("TCPSender", typeof(IMessageSender))]
	class TCPSender : IMessageSender
	{
		public void Send(string message)
		{
			Console.WriteLine($"TCPSender, {message}");
		}
	}
}
