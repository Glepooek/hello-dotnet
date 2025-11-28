using _10_Infrastructure;
using System;
using System.ComponentModel.Composition;

namespace _10_ModuleA
{
	[Export(typeof(IMessageSender))]
	[PartMetadata("IsSecure", "false"), Export]
	public class EmailSender : IMessageSender
	{
		public void Send(string message)
		{
			Console.WriteLine($"EmailSender, {message}");
		}
	}
}
