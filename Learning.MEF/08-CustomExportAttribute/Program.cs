using System;

namespace _08_CustomExportAttribute
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
