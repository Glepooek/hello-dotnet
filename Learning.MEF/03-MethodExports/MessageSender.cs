using System;
using System.ComponentModel.Composition;

namespace _03_MethodExports
{
	class MessageSender
	{
		[Export(typeof(Action<string>))]
		//[Export("MessageSender")]
		public void Send(string message)
		{
			Console.WriteLine(message);
		}
	}
}
