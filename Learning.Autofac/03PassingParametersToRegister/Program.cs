using System;
using Autofac;
using Shared.Impls;
using Shared.Interfaces;

/**
 * 注册组件时，可用提供一组参数，在解析该组件时使用。
 * Autofac提供多种参数匹配机制：
 *		NamedParameter-通过名字匹配目标参数
 *		TypedParameter-通过具体类型匹配目标参数
 *		ResolvedParameter-复杂参数匹配
 *		
 *		NamedParameter和TypedParameter仅支持常量
 *		ResolvedParameter 可以用于提供不同的值来从容器中动态获取对象, 例如, 通过名字解析服务
 * **/

namespace _03PassingParametersToRegister
{
	class Program
	{
		private IContainer container = null;
		private ContainerBuilder builder = null;

		static void Main(string[] args)
		{
			Program p = new Program();
			p.Run();

			Console.Read();
		}


		private void Run()
		{
			Register();


			XmlConfigReader configReader = container.Resolve<XmlConfigReader>();
			configReader.Read();

			IConfigReader configReader1 = container.Resolve<IConfigReader>();
			configReader1.Read("");
		}

		private void Register()
		{
			builder = new ContainerBuilder();
			builder.Register(c => new XmlConfigReader("LoggerSection"))
				.AsSelf()
				.As<IConfigReader>();

			//builder.RegisterType<XmlConfigReader>()
			//	.As<IConfigReader>()
			//	.WithParameter("sectionName", "LoggerSection");

			builder.RegisterType<XmlConfigReader>()
				.As<IConfigReader>()
				.WithParameter(new TypedParameter(typeof(string), "LoggerSection"));

			container = builder.Build();
		}
	}
}
