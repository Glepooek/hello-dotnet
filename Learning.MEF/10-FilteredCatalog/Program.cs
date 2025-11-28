using _10_Infrastructure;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace _10_FilteredCatalog
{
	class Program
	{
		//[Import]
		//IMessageSender Sender { get; set; }

		Lazy<IMessageSender> Sender { get; set; }

		static void Main(string[] args)
		{
			Program p = new Program();
			p.Run();
		}

		public void Run()
		{
			Compose();

			Sender.Value.Send("Hello MEF");
			Console.ReadLine();
		}

		private void Compose()
		{
			// ***********方式一**************
			//AssemblyCatalog acatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
			//DirectoryCatalog dcatalog = new DirectoryCatalog(@"./Modules");
			//// 筛选指定目录下的导出部件
			//FilteredCatalog fcatalog = new FilteredCatalog(dcatalog, p => p.Metadata.ContainsKey("IsSecure") && p.Metadata["IsSecure"].ToString() == "true");

			//AggregateCatalog composableParts = new AggregateCatalog(acatalog, fcatalog);

			//CompositionContainer container = new CompositionContainer(composableParts);
			//container.ComposeParts(this);
			// ***********方式一**************

			// ***********方式二**************
			DirectoryCatalog dcatalog = new DirectoryCatalog(@"./Modules");
			CompositionContainer parentContainer = new CompositionContainer(dcatalog);

			// 筛选指定目录下的导出部件
			FilteredCatalog fcatalog = new FilteredCatalog(dcatalog, p => p.Metadata.ContainsKey("IsSecure") && p.Metadata["IsSecure"].ToString() == "true");

			CompositionContainer childContainer = new CompositionContainer(fcatalog, parentContainer);
			Sender = childContainer.GetExport<IMessageSender>();
			//childContainer.Dispose();
			// ***********方式二**************
		}
	}
}
