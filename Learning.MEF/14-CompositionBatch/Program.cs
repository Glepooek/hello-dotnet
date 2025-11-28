using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace _14_CompositionBatch
{
	class Program : IPartImportsSatisfiedNotification
	{
		CompositionContainer Container { get; set; }
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
			TCPSender.Send("Hello MEF");

			Console.ReadLine();
		}

		private void Compose()
		{
			// 批处理包含一系列添加或移除的部件。在执行更改之后，容器自动地触发一次更新重组导入导致的变化的组合。
			CompositionBatch batch = new CompositionBatch();
			batch.AddPart(this);
			batch.AddPart(new TCPSender());
			batch.AddPart(new EmailSender());

			Container = new CompositionContainer();
			Container.Compose(batch);
		}

		public void OnImportsSatisfied()
		{

		}
	}
}
