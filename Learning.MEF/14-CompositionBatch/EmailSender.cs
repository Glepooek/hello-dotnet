using System;
using System.ComponentModel.Composition;

namespace _14_CompositionBatch
{
	[Export("EmailSender", typeof(IMessageSender))]
	public class EmailSender : IMessageSender
	{
		public void Send(string message)
		{
			Console.WriteLine($"EmailSender, {message}");
		}
	}
}
