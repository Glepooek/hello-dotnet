using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace _06_LazyExport
{
	class HttpServerHealthMonitor
	{
		[Import]
		Lazy<IMessageSender> Sender { get; set; }

		public void Run()
		{
			Compose();
			// 延迟导出，请求 Lazy<IMessageSender> 时，实例化 IMessageSender 导出
			Sender.Value.Send("Hello MEF!");
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
