using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace _07_ExportMetadata
{
	class HttpServerHealthMonitor
	{
		/***
		 * 01 使用用弱类型元数据
		 * 使用 System.Lazy<T, TMetadata> 类型传递 IDictionary<string, object> 元数据给导入。
		 * 然后，可以通过 Dictionary 的元数据属性访问元数据。
		 * ****/
		//[ImportMany]
		//Lazy<IMessageSender, IDictionary<string, object>>[] Senders { get; set; }

		/**
		 * 02 使用强类型元数据
		 * 为了访问强类型的元数据，通过定义匹配只读属性（名称和类型）的接口创建元数据视图。
		 * **/
		[ImportMany]
		Lazy<IMessageSender, ITransport>[] Senders { get; set; }

		public void Run()
		{
			Compose();

			//foreach (var item in Senders)
			//{
			//	if (item.Metadata.ContainsKey("IsSecure")
			//		&& (bool)item.Metadata["IsSecure"] == true)
			//	{
			//		item.Value.Send("this TransportType is secure");
			//		break;
			//	}
			//}

			foreach (Lazy<IMessageSender, ITransport> item in Senders)
			{
				if (item.Metadata.TransportType == TransportType.SMTP
					&& item.Metadata.IsSecure == true)
				{
					item.Value.Send("this TransportType is secure");
				}
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
