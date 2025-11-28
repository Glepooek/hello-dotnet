using _07_PropertyOverride.Media;
using _07_PropertyOverride.Player;
using System;
using Unity;
using Unity.Injection;
using Unity.Resolution;

namespace _07_PropertyOverride
{
	class Program
	{
		static void Main(string[] args)
		{
			// create UnityContainer instance
			IUnityContainer container = new UnityContainer();

			PropertyOverrideDemo(container);

			Console.ReadLine();
		}

		public static void PropertyOverrideDemo(IUnityContainer container)
		{
			// retister type-mapping
			container.RegisterType<IPlayer, QQPlayer>();
			// retister type-mapping with a name
			container.RegisterType<IPlayer, BaiduPlayer>("baidu");

			container.RegisterType<IMediaFile, MP3MediaFile>(new InjectionConstructor("C:\\document\\mp3"));
			container.RegisterType<IMediaFile, MP4MediaFile>("mp4", new InjectionConstructor("C:\\document\\mp4"));

			container.RegisterType<User1>(new InjectionProperty("Media", new MP3MediaFile("C:\\document\\mp3")), new InjectionProperty("Player", container.Resolve<IPlayer>()));

			User1 u = container.Resolve<User1>();
			u.PlayMedia();

			u = container.Resolve<User1>(new PropertyOverride("Media", new MP4MediaFile("C:\\document\\mp4")), new PropertyOverride("Player", container.Resolve<IPlayer>("baidu")));
			u.PlayMedia();

			//u = container.Resolve<User1>(new DependencyOverride<IPlayer>(container.Resolve<IPlayer>("baidu")), new DependencyOverride<IMediaFile>(container.Resolve<IMediaFile>("mp4")));
			//u.PlayMedia();
		}
	}
}
