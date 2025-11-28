using _06_ParameterOverride.Media;
using _06_ParameterOverride.Player;
using System;
using Unity;
using Unity.Injection;
using Unity.Resolution;

namespace _06_ParameterOverride
{
	class Program
	{
		static void Main(string[] args)
		{
			// create UnityContainer instance
			IUnityContainer container = new UnityContainer();

			ParameterOverrideDemo(container);

			Console.ReadLine();
		}

		public static void ParameterOverrideDemo(IUnityContainer container)
		{
			// retister type-mapping
			container.RegisterType<IPlayer, QQPlayer>();
			// retister type-mapping with a name
			container.RegisterType<IPlayer, BaiduPlayer>("baidu");

			container.RegisterType<IMediaFile, MP3MediaFile>(new InjectionConstructor("C:\\document\\mp3"));

			container.RegisterType<IMediaFile, MP4MediaFile>("mp4", new InjectionConstructor("C:\\document\\mp4"));

			User u = container.Resolve<User>();
			u.PlayMedia();

			u = container.Resolve<User>(new ParameterOverride("media", container.Resolve<IMediaFile>("mp4")), new ParameterOverride("player", container.Resolve<IPlayer>("baidu")));
			u.PlayMedia();

			//u = container.Resolve<User>(new DependencyOverride<IPlayer>(container.Resolve<IPlayer>("baidu")), new DependencyOverride<IMediaFile>(container.Resolve<IMediaFile>("mp4")));
			//u.PlayMedia();
		}
	}
}
