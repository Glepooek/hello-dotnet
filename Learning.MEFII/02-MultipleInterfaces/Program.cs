using _02_MultipleInterfaces.Classes;
using _02_MultipleInterfaces.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Reflection;

namespace _02_MultipleInterfaces
{
	class Program
	{
		private CompositionContainer container;

		static void Main(string[] args)
		{
			Program p = new Program();
			p.Run();
		}

		private void Run()
		{
			RegisterExport();

			var human = container.GetExportedValue<IHuman>();
			Console.WriteLine(string.Format("IdentityNo:{0}  Name:{1}",
				human.IdentityNo, human.Name));

			var mammal = container.GetExportedValue<IMammal>();
			Console.WriteLine(string.Format("LegNum:{0} HairType:{1}",
				mammal.LegNum, mammal.HairType));

			//var person = container.GetExportedValue<Person>();
			//Console.WriteLine(person.ToString());

			Console.ReadLine();
		}

		private void RegisterExport()
		{
			RegistrationBuilder builder = new RegistrationBuilder();

			//builder.ForType<Person>().ExportInterfaces();

			builder.ForType<Person>().ExportInterfaces(t=> t.Name.Equals("IHuman"));
			builder.ForType<Person>().ExportInterfaces(t => t.Name.Equals("IMammal"));

			//builder.ForType<Person>().Export();

			Compose(builder);
		}

		private void Compose(RegistrationBuilder builder)
		{
			AssemblyCatalog catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly(), builder);

			this.container = new CompositionContainer(catalog,
				CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe);
			this.container.ComposeParts(this);
		}
	}
}
