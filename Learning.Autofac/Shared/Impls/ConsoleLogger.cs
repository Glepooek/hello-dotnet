using Shared.Interfaces;
using System;

namespace Shared.Impls
{
	public class ConsoleLogger : ILogger
	{
		public void Log(string content)
		{
			Console.WriteLine(content);
		}
	}
}
