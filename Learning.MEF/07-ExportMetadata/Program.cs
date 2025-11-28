using System;

namespace _07_ExportMetadata
{
	class Program
	{
		static void Main(string[] args)
		{
			HttpServerHealthMonitor httpServer = new HttpServerHealthMonitor();
			httpServer.Run();
			Console.ReadLine();
		}
	}
}
