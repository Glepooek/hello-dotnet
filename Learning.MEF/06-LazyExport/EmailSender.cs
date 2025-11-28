using System;
using System.ComponentModel.Composition;

namespace _06_LazyExport
{
	[Export(typeof(IMessageSender))]
	class EmailSender : IMessageSender
	{
		public void Send(string message)
		{
			Console.WriteLine(message);
			Console.ReadLine();
		}
	}
}
