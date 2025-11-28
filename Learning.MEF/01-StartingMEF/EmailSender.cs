using System;
using System.ComponentModel.Composition;

namespace _01StartingMEF
{
	[Export("EmailSender", typeof(IMessageSender))]
	public class EmailSender : IMessageSender
	{
		public void Send(string message)
		{
			Console.WriteLine($"EmailSender, {message}");
			Console.ReadLine();
		}
	}
}
