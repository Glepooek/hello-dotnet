using System;
using System.ComponentModel.Composition;

namespace _12_Recomposition
{
	[Export(typeof(IMessageSender))]
	public class EmailSender : IMessageSender
	{
		public void Send(string message)
		{
			Console.WriteLine($"EmailSender, {message}");
		}
	}
}
