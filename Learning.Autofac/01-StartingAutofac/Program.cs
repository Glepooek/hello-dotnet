using Autofac;
using Shared.Impls;
using Shared.Interfaces;
using System;

namespace _01_StartingAutofac
{
	class Program
	{
		private IContainer container = null;
		private ContainerBuilder builder = null;

		static void Main(string[] args)
		{
			Program p = new Program();
			p.Run();
		}

		private void Run()
		{
			Register();

			// 这种做法不被推荐
			//IDateWriter writer = container.Resolve<IDateWriter>();
			//writer.WriteDate();

			// 从实例作用域中解析依赖
			using (var scope = container.BeginLifetimeScope())
			{
				IWriter writer = scope.Resolve<IWriter>();
				writer.Write();
			}

			Console.ReadLine();
		}

		private void Register()
		{
			builder = new ContainerBuilder();
			builder.RegisterType<DateWriter>().As<IWriter>();
			builder.RegisterType<ConsoleLogger>().As<ILogger>();

			container = builder.Build();
		}
	}
}
