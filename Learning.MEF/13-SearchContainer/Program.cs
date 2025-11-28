using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace _13_SearchContainer
{
	class Program
	{
		CompositionContainer Container { get; set; }

		IMessageSender Sender { get; set; }
		Lazy<IMessageSender> LazySender { get; set; }
		IEnumerable<IMessageSender> Senders { get; set; }
		IEnumerable<Lazy<IMessageSender>> LazySenders { get; set; }

		static void Main(string[] args)
		{
			Program p = new Program();
			p.Run();
		}

		public void Run()
		{
			Compose();

			// 相同类型的多个导出对象时，查找时指定契约名称
			//Sender = Container.GetExportedValue<IMessageSender>();
			Sender = Container.GetExportedValueOrDefault<IMessageSender>();

			if (Sender != null)
			{
				Sender.Send("Hello MEF! by GetExportedValue");
			}

			// 相同类型的多个导出时，查找时指定契约名称
			LazySender = Container.GetExport<IMessageSender>();

			if (LazySender != null && LazySender.Value != null)
			{
				LazySender.Value.Send("Hello MEF! by GetExport");
			}

			Senders = Container.GetExportedValues<IMessageSender>();

			foreach (var sender in Senders)
			{
				sender.Send("Hello MEF! by GetExportedValues");
			}

			LazySenders = Container.GetExports<IMessageSender>();

			foreach (var lazysender in LazySenders)
			{
				lazysender.Value.Send("Hello MEF! by GetExports");
			}

			Console.ReadLine();
		}

		private void Compose()
		{
			AssemblyCatalog catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
			Container = new CompositionContainer(catalog);
			Container.ComposeParts(this);
		}
	}
}
