using Autofac;
using Autofac.Core;
using Shared.Impls;
using Shared.Interfaces;
using System;

//注册组件
//通过创建 ContainerBuilder 来注册组件并且告诉容器哪些组件暴露了哪些服务。

//通过反射 (注册指定的.NET类或开放结构的泛型)创建组件;
//通过提供现成的实例(你已创建的一个对象实例)创建组件;
//或者通过lambda 表达式(一个执行实例化对象的匿名方法)来创建组件。

namespace _02_Registration
{
	class Program
	{
		private IContainer container = null;
		private ContainerBuilder builder = null;

		static void Main(string[] args)
		{
			Program p = new Program();
			p.Run();

			Console.ReadLine();
		}

		private void Run()
		{
			//TypeRegister();
			//InstanceRegister();
			//LambdaRegister();
			ConditionRegister();
		}

		/// <summary>
		/// 通过具体类型注册组件
		/// </summary>
		private void TypeRegister()
		{
			builder = new ContainerBuilder();
			// 当使用基于反射的组件时, 未指定使用的构造函数时，Autofac 自动为类从容器中寻找匹配拥有最多参数的构造方法
			// 发现并使用DateWriter第一个构造
			// 反射组件时，注册的组件必须是具体类型，无法注册抽象类或接口
			builder.RegisterType<DateWriter>();
			// 指定构造函数，两个参数必须都要注册
			//builder.RegisterType<DateWriter>().UsingConstructor(typeof(ILogger), typeof(IConfigReader));
			builder.RegisterType<ConsoleLogger>().As<ILogger>();
			//builder.RegisterType<XmlConfigReader>().As<IConfigReader>();

			container = builder.Build();

			using var scope = container.BeginLifetimeScope();
			DateWriter dateWriter = scope.Resolve<DateWriter>();
			dateWriter.Write();
		}

		/// <summary>
		/// 通过实例注册组件
		/// </summary>
		private void InstanceRegister()
		{
			builder = new ContainerBuilder();
			TextWriter textWriter = new TextWriter();
			builder.RegisterInstance(textWriter)
				.As<TextWriter>()
				// 自己控制生命周期，不用autofac释放对象
				.ExternallyOwned();

			container = builder.Build();
			using var scope = container.BeginLifetimeScope();
			textWriter = scope.Resolve<TextWriter>();
			textWriter.Write();
		}

		/// <summary>
		/// Lambda表达式注册组件
		/// </summary>
		private void LambdaRegister()
		{
			builder = new ContainerBuilder();
			builder.RegisterType<ConsoleLogger>()
				// 露为默认服务，解析时用ConsoleLogger类型
				.AsSelf()
				// 暴露为特定服务，解析时用ILogger类型
				.As<ILogger>();
			builder.Register<IWriter>((c, p) =>
			{
				// 通过参数值选择具体的实现
				string type = p.Named<string>("type");
				if (type.Equals("date"))
				{
					return new DateWriter(c.Resolve<ConsoleLogger>());
					//return new DateWriter(c.Resolve<ILogger>());
				}
				else
				{
					return new TextWriter();
				}
			});

			container = builder.Build();
			using var scope = container.BeginLifetimeScope();
			IWriter writer = scope.Resolve<IWriter>(new NamedParameter("type", "date"));
			writer.Write();
		}

		/// <summary>
		/// 有条件注册
		/// </summary>
		private void ConditionRegister()
		{
			builder = new ContainerBuilder();
			builder.RegisterType<ConsoleLogger>()
				.As<ILogger>();
			builder.RegisterType<DateWriter>()
				.AsSelf()
				.As<IWriter>()
				// 只有ILogger注册后，DateWriter才能注册
				.OnlyIf(reg=> reg.IsRegistered(new TypedService(typeof(ILogger))));
			builder.RegisterType<TextWriter>()
				.AsSelf()
				.As<IWriter>()
				// 在DateWriter没注册时，TextWriter才能注册
				.IfNotRegistered(typeof(DateWriter));

			container = builder.Build();
			using var scope = container.BeginLifetimeScope();
			IWriter writer = scope.Resolve<IWriter>();
			writer.Write();
		}
	}
}
