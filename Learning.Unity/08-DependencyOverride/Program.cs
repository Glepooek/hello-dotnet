using _08_DependencyOverride.Media;
using _08_DependencyOverride.Player;
using System;
using Unity;
using Unity.Injection;
using Unity.Resolution;

namespace _08_DependencyOverride
{
	class Program
	{
		static void Main(string[] args)
		{
			// create UnityContainer instance
			IUnityContainer container = new UnityContainer();

			DependencyOverrideDemo(container);

			Console.ReadLine();
		}

		public static void DependencyOverrideDemo(IUnityContainer container)
		{
			// retister type-mapping
			container.RegisterType<IPlayer, QQPlayer>();
			// retister type-mapping with a name
			container.RegisterType<IPlayer, BaiduPlayer>("baidu");

			container.RegisterType<IMediaFile, MP3MediaFile>(new InjectionConstructor("C:\\document\\mp3"));
			container.RegisterType<IMediaFile, MP4MediaFile>("mp4", new InjectionConstructor("C:\\document\\mp4"));

			container.RegisterType<User2>(new InjectionMethod("Injct", new object[] { container.Resolve<IMediaFile>(), container.Resolve<IPlayer>("baidu") }));

			User2 u = container.Resolve<User2>();
			u.PlayMedia();

			u = container.Resolve<User2>(new DependencyOverride<IMediaFile>(container.Resolve<IMediaFile>("mp4")), new DependencyOverride<IPlayer>(container.Resolve<IPlayer>()));
			u.PlayMedia();
		}
	}
}
