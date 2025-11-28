using _03_Imports.Generic;
using _03_Imports.Normal;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Reflection;

namespace _03_Imports
{
	class Program
	{
		CompositionContainer container;

		static void Main(string[] args)
		{
			Program p = new Program();
			p.Run();
		}

		private void Run()
		{
			RegisterNormalExport();
			RegisterGenericExport();

			var test = container.GetExportedValue<Test2>();
			test.Run();

			var test1 = container.GetExportedValue<ITest>();
			test1.Run();

			System.Console.ReadKey();
		}

		private void RegisterNormalExport()
		{
			RegistrationBuilder builder = new RegistrationBuilder();

			builder.ForTypesDerivedFrom<IGreeting>().Export<IGreeting>();
			builder.ForTypesDerivedFrom<IPerson>().Export<IPerson>();
			builder.ForType<Test2>()
			  .ImportProperties(pi => pi.Name == "Greeting" || pi.Name == "Person")
			  .Export();

			//builder.ForType<Test2>().ImportProperty<IGreeting>(x => x.Greeting);
			//builder.ForType<Test2>().ImportProperty<Normal.IPerson>(x => x.Person);
			//builder.ForType<Test2>().Export();

			Compose(builder);
		}

		private void RegisterGenericExport()
		{
			RegistrationBuilder builder = new RegistrationBuilder();
			builder.ForType(typeof(PersonRepository<>)).Export(x => x.AsContractType(typeof(IRepository<>)));

			builder.ForTypesDerivedFrom<ITest>().
			   // 选择执行的构造函数，执行必备导入
			   SelectConstructor((ConstructorInfo[] x) =>
			   {
				   foreach (var item in x)
				   {
					   if (item.GetParameters().Count() == 1)
						   return item;
				   }

				   return null;
			   }, (param, imp) =>
			   {
				   imp.AsContractType(param.ParameterType);
			   }).Export<ITest>();

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
