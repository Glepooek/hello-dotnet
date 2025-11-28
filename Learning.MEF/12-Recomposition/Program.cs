using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace _12_Recomposition
{
	class Program
	{
		/*********
		 * 有些应用程序被设计成在运行时动态地改变。例如，一个新的扩展可能被下载，或者其他原因变得不可用。MEF 依靠我们
		 * 称之为重组（Composition）的技术处理，在初始化组合以后改变导入值的场景。
		 * 导入可以通过 [System.ComponentModel.Composition.ImportAttribute] 使用 Allowrecompostion 属性通知 MEF 支持重组。

		* 重组的注意事项（Caveats of Recompostion）
		* 	1、当重组发生的时候，将会用新的实例替换集合中的实例 / 数组，不会更新已经存在的实例。
		* 	2、重组支持几乎所有类型的导入：字段、属性和集合，但是不支持构造器参数。
		* 	3、如果类型实现了 [System.ComponentModel.Compostion.IPartImportsSatisfiedNotification] 接口，每当发生重组 ImportCompleted 也将会被调用。
		 * **************/
		[ImportMany(AllowRecomposition = true)]
		IMessageSender[] Senders { get; set; }

		static void Main(string[] args)
		{
			Program p = new Program();
			p.Run();
		}

		public void Run()
		{
			Compose();

			if (Senders != null)
			{
				foreach (var item in Senders)
				{
					item.Send("Hello MEF");
				}
			}

			Console.ReadLine();
		}

		private void Compose()
		{
			AssemblyCatalog catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
			CompositionContainer container = new CompositionContainer(catalog);
			container.ComposeParts(this);
		}
	}
}
