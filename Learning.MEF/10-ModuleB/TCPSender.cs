using _10_Infrastructure;
using System;
using System.ComponentModel.Composition;

namespace _10_ModuleB
{
	[Export(typeof(IMessageSender))]
	[PartMetadata("IsSecure", "true"), Export]
	public class TCPSender : IMessageSender
	{
		public void Send(string message)
		{
			Console.WriteLine($"TCPSender, {message}");
		}
	}
}
