using System.ComponentModel.Composition.Hosting;

namespace _04_ObjLifetime.PartCreation
{
	public class LifeTimeManager
	{
		public void ShowObjLifetime(CompositionContainer container)
		{
			for (int i = 1; i < 5; i++)
			{
				var tester = container.GetExportedValue<ObjectTester>();
				++tester.Counter;
				System.Console.WriteLine("execution {0} Counter Value is {1}", i, tester.Counter);
			}
			System.Console.ReadKey();
		}
	}
}
