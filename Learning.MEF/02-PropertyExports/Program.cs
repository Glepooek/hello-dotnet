using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

/*****************
 * 组合部件通过 [System.ComponentModel.Composition.ExportAttribute] 特性声明导出。
 * MEF 有几种不同的方式声明导出，包括在部件层面（Part Level，类、接口），通过属性（Properties）和方法（Method）声明导出，还有继承导出（MEF 支持基类/接口定义导出，由实现者自动继承的能力）。
 *****************/

namespace _02_PropertyExports
{
	class Program
	{
		[Import]
		public Connection Connection { get; set; }

		static void Main(string[] args)
		{
			Program p = new Program();
			p.Run();
		}

		private void Run()
		{
			Compose();
			Console.WriteLine(Connection.Timeout);
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
