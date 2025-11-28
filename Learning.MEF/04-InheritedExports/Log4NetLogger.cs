using System;

namespace _04_InheritedExports
{
	class Log4NetLogger : ILogger
	{
		public void Log(string message)
		{
			Console.WriteLine(message);
		}
	}
}
