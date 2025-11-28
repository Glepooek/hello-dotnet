using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace _09_Catalogs
{
	class Program
	{
		static void Main(string[] args)
		{

		}

		public void Run()
		{
			Compose();

		}

		private void Compose()
		{
			// 01在程序集中发现部件
			AssemblyCatalog acatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());

			// 02在指定目录中发现部件
			// 一次性扫描目录，目录内容更新后不会自动刷新，需要用Refresh()方法刷新
			DirectoryCatalog dcatalog = new DirectoryCatalog(@"./Modules");

			// 03类型目录
			// 发现特定类型的部件
			TypeCatalog tcatalog = new TypeCatalog();

			// 04聚合目录
			// 当程序集目录和文件目录不能独自地满足要求或者是需要聚合目录。
			// 一种常见的模式是不仅添加当前执行的程序集，而且添加三方扩展的文件目录。
			AggregateCatalog composableParts = new AggregateCatalog(acatalog, dcatalog);

			// 使用容器组装部件
			CompositionContainer container = new CompositionContainer(composableParts);
			container.ComposeParts(this);
		}
	}
}
