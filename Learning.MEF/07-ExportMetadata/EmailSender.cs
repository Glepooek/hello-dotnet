using System;
using System.ComponentModel.Composition;

namespace _07_ExportMetadata
{
	[Export(typeof(IMessageSender))]
	[ExportMetadata("TransportType", TransportType.SMTP)]
	[ExportMetadata("IsSecure", false)]
	public class EmailSender : IMessageSender
	{
		public void Send(string message)
		{
			Console.WriteLine($"EmailSender, {message}");
		}
	}
}
