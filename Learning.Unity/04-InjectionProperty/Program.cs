using _04_InjectionProperty.Media;
using _04_InjectionProperty.Player;
using System;
using Unity;
using Unity.Injection;

namespace _04_InjectionProperty
{
	class Program
	{
		static void Main(string[] args)
		{
			// create UnityContainer instance
			IUnityContainer container = new UnityContainer();

			InjectionPropertyDemo(container);

			Console.ReadLine();
		}

		public static void InjectionPropertyDemo(IUnityContainer container)
		{
			// retister type-mapping
			container.RegisterType<IPlayer, QQPlayer>();
			// retister type-mapping with a name
			container.RegisterType<IPlayer, BaiduPlayer>("baidu");

			container.RegisterType<IMediaFile, MP3MediaFile>(new InjectionConstructor("C:\\document\\mp3"));

			container.RegisterType<User1>(new InjectionProperty("Media", new MP3MediaFile("C:\\document\\mp3")), new InjectionProperty("Player", container.Resolve<IPlayer>()));

			User1 u = container.Resolve<User1>();
			u.PlayMedia();
			User1 u1 = container.Resolve<User1>();
			u1.PlayMedia();
		}
	}
}
