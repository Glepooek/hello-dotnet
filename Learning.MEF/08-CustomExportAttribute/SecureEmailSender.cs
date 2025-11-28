using System;

namespace _08_CustomExportAttribute
{
	[MessagSender(typeof(IMessageSender), TransportType = TransportType.TCP)]
	public class SecureEmailSender : IMessageSender
	{
		public void Send(string message)
		{
			Console.WriteLine($"SecureEmailSender, {message}");
		}
	}
}
