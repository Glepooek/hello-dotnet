using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace _08_CustomExportAttribute
{
	class HttpServerHealthMonitor
	{
		[ImportMany]
		Lazy<IMessageSender>[] Senders { get; set; }

		public void Run()
		{
			Compose();

			foreach (Lazy<IMessageSender> item in Senders)
			{
				item.Value.Send("this TransportType is secure");
			}
		}

		private void Compose()
		{
			//var container = new CompositionContainer();
			//container.ComposeParts(this, new EmailSender());

			AssemblyCatalog catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
			var container = new CompositionContainer(catalog);
			container.ComposeParts(this);
		}
	}
}
