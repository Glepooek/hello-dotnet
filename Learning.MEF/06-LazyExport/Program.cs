namespace _06_LazyExport
{
	class Program
	{
		static void Main(string[] args)
		{
			HttpServerHealthMonitor httpServer = new HttpServerHealthMonitor();
			httpServer.Run();
		}
	}
}
