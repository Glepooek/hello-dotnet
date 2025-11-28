using _03_RegisterSingleton.Media;
using _03_RegisterSingleton.Player;
using System;
using Unity;
using Unity.Injection;

namespace _03_RegisterSingleton
{
	class Program
	{
		static void Main(string[] args)
		{
			// create UnityContainer instance
			IUnityContainer container = new UnityContainer();

			RegisterSingletonDemo(container);

			Console.ReadLine();
		}

		public static void RegisterSingletonDemo(IUnityContainer container)
		{
			// retister type-mapping
			container.RegisterSingleton<IPlayer, QQPlayer>();
			container.RegisterSingleton<IMediaFile, MP4MediaFile>(new InjectionConstructor("C:\\document\\mp4"));

			User u = container.Resolve<User>();
			u.PlayMedia();
			User u1 = container.Resolve<User>();
			u1.PlayMedia();
		}
	}
}
