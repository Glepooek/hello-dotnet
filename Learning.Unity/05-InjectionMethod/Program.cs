using _05_InjectionMethod.Media;
using _05_InjectionMethod.Player;
using System;
using Unity;
using Unity.Injection;

namespace _05_InjectionMethod
{
	class Program
	{
		static void Main(string[] args)
		{
			// create UnityContainer instance
			IUnityContainer container = new UnityContainer();

			ExeInjectionMethodDemo(container);

			Console.ReadLine();
		}

		public static void ExeInjectionMethodDemo(IUnityContainer container)
		{
			// retister type-mapping
			container.RegisterType<IPlayer, QQPlayer>();
			// retister type-mapping with a name
			container.RegisterType<IPlayer, BaiduPlayer>("baidu");

			container.RegisterType<IMediaFile, MP3MediaFile>(new InjectionConstructor("C:\\document\\mp3"));
			container.RegisterType<User2>(new InjectionMethod("Injct", new object[] { container.Resolve<IMediaFile>(), container.Resolve<IPlayer>("baidu") }));

			User2 u = container.Resolve<User2>();
			u.PlayMedia();
			User2 u1 = container.Resolve<User2>();
			u1.PlayMedia();
		}
	}
}
