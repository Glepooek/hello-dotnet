using System;
using System.ComponentModel.Composition;

namespace _07_ExportMetadata
{
	[Export(typeof(IMessageSender))]
	[ExportMetadata("TransportType", TransportType.SMTP)]
	public class SecureEmailSender : IMessageSender
	{
		public void Send(string message)
		{
			Console.WriteLine($"SecureEmailSender, {message}");
		}
	}
}
