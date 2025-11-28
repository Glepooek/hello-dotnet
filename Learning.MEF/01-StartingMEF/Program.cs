using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace _01StartingMEF
{
	class Program
	{
		[Import("EmailSender")]
		IMessageSender EmailSender { get; set; }

		[Import("TCPSender")]
		IMessageSender TCPSender { get; set; }

		static void Main(string[] args)
		{
			Program p = new Program();
			p.Run();
		}

		public void Run()
		{
			Compose();
			//TCPSender.Send("Hello MEF");
			EmailSender.Send("Hello MEF");
		}

		private void Compose()
		{
			// 1、将可组合部件显示添加到容器中
			//CompositionContainer container = new CompositionContainer();
			//container.ComposeParts(this, new EmailSender());

			// 2、通过使用目录，容器自动组合部件
			AssemblyCatalog catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
			CompositionContainer container = new CompositionContainer(catalog);
			container.ComposeParts(this);
		}
	}
}
