using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace _05_ImportingConstructor
{
	class Program : IPartImportsSatisfiedNotification
	{
		#region 01属性导入

		//[Import("TCPSender")]
		//IMessageSender TCPSender { get; set; }

		//[Import("EmailSender")]
		//IMessageSender EmailSender { get; set; }

		[Import]
		Processor Processor { get; set; }

		#endregion

		#region 02字段导入

		//[Import("TCPSender")]
		//IMessageSender tcpSender;

		//[Import("EmailSender")]
		//IMessageSender emailSender;

		#endregion

		#region 03缺省导入

		//[Import("TCPSender", typeof(IMessageSender), AllowDefault =true)]
		//IMessageSender TCPSender { get; set; }

		#endregion

		#region 05导入集合

		// 当导入有契约时，ImportMany需要指定相同的契约才能导入
		[ImportMany()]
		IEnumerable<IMessageSender> Senders { get; set; }

		#endregion

		#region 06IPartImportsSatisfiedNotification

		// 实现该接口，以便导入完成时获取通知
		public void OnImportsSatisfied()
		{
			foreach (var item in Senders)
			{
				item.Send("消息已发送");
			}
		}

		#endregion

		static void Main(string[] args)
		{
			Program p = new Program();
			p.Run();
		}

		private void Run()
		{
			Compose();
			//Processor.Sender.Send("消息已发送");
			//TCPSender.Send("消息已发送");

			Console.ReadLine();
		}

		private void Compose()
		{
			//CompositionContainer container = new CompositionContainer();
			//container.ComposeParts(this, new Connection(), new Configuration());

			AssemblyCatalog catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
			CompositionContainer container = new CompositionContainer(catalog);
			container.ComposeParts(this);
		}
	}
}
