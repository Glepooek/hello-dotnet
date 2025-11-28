using System;
using System.ComponentModel.Composition;

namespace _13_SearchContainer
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
