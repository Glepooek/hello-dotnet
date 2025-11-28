using _01_MEFIIPreview.Classes;
using _01_MEFIIPreview.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Reflection;

/*
     // map the dependency
    // 注册一个特定类型并导出有三种方式：
    // ForType()\ForType<T>()
    // ForTypesMatching(Predicate<Type> typeFilter)\ForTypesMatching<>(Predicate<Type> typeFilter)
    // ForTypesDerivedFrom()\ForTypesDerivedFrom<T>() the same as the using InheritedExport  in MEF 1
    // convention.ForTypesDerivedFrom<IGreetings>().Export<IGreetings>(); 

    CreationPolicy枚举：指定何时以及如何实例化部件（用在一个导出填充多个导入的情况）
    Any:共享单个实例，默认
    Shared:共享单个实例
    NonShared:为每个导出创建实例

    CompositionOptions枚举：
     // Default: 默认实现和设置 
     // DisableSilentRejection: 当导入或导出失败时，显示更多异常详细
     // IsThreadSafe: 在安全线程中组合导出
     // ExportCompositionService: 设置导出组合服务 
*/

namespace _01_MEFIIPreview
{
	class Program
	{
		private CompositionContainer container;

		static void Main(string[] args)
		{
			Program P = new Program();
			P.Run();
		}

		private void Run()
		{
			RegisterExport();

			var test = container.GetExportedValue<ITest>();
			test.Run();

			IRepository<Person> repository = container.GetExportedValueOrDefault<IRepository<Person>>();
			Person person = repository.GetPerson(new Person { Name = "Anyu" });

			Console.WriteLine($"Hello, {person.Name}");

			Console.ReadKey();
		}

		private void RegisterExport()
		{
			RegistrationBuilder builder = new RegistrationBuilder();
			builder.ForType(typeof(PersonRepository<>))
				.Export(b => b.AsContractType(typeof(IRepository<>)))
				.SetCreationPolicy(CreationPolicy.NonShared);

			//builder.ForType(typeof(Test))
			//    .Export(b => b.AsContractType(typeof(ITest)))
			//    .SetCreationPolicy(CreationPolicy.NonShared);
			builder.ForTypesDerivedFrom<ITest>()
				.Export<ITest>()
				.SetCreationPolicy(CreationPolicy.NonShared);

			Compose(builder);
		}

		private void Compose(RegistrationBuilder builder)
		{
			AssemblyCatalog catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly(), builder);

			container = new CompositionContainer(catalog,
				CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe);

			container.ComposeParts(this);
		}
	}
}
