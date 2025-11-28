using System;
using System.ComponentModel.Composition;

namespace _05_ImportingConstructor
{
	[Export("EmailSender", typeof(IMessageSender))]
	class EmailSender : IMessageSender
	{
		public void Send(string message)
		{
			Console.WriteLine($"EmailSender, {message}");
		}
	}
}
