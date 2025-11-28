using System;
using System.ComponentModel.Composition;

namespace _01StartingMEF
{
	[Export("TCPSender", typeof(IMessageSender))]
	public class TCPSender : IMessageSender
	{
		public void Send(string message)
		{
			Console.WriteLine($"TCPSender, {message}");
			Console.ReadLine();
		}
	}
}
