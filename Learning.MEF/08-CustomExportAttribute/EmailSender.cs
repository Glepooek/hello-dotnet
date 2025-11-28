using System;

namespace _08_CustomExportAttribute
{
	[MessagSender(typeof(IMessageSender), IsSecure = false, TransportType = TransportType.UDP)]
	public class EmailSender : IMessageSender
	{
		public void Send(string message)
		{
			Console.WriteLine($"EmailSender, {message}");
		}
	}
}
