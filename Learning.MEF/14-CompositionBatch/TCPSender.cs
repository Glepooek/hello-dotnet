using System;
using System.ComponentModel.Composition;

namespace _14_CompositionBatch
{
	[Export("TCPSender", typeof(IMessageSender))]
	public class TCPSender : IMessageSender
	{
		public void Send(string message)
		{
			Console.WriteLine($"TCPSender, {message}");
		}
	}
}
