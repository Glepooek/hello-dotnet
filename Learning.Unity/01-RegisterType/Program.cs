using _01_RegisterType.Media;
using _01_RegisterType.Player;
using System;
using Unity;
using Unity.Injection;

// http://www.tutorialsteacher.com/ioc/constructor-injection-using-unity-container
// https://stackoverflow.com/questions/4059991/microsoft-unity-how-to-specify-a-certain-parameter-in-constructor

namespace _01_RegisterType
{
	class Program
	{
		static void Main(string[] args)
		{
			// create UnityContainer instance
			IUnityContainer container = new UnityContainer();

			RegisterTypeDemo(container);

			Console.ReadLine();
		}

		public static void RegisterTypeDemo(IUnityContainer container)
		{
			// retister type-mapping
			container.RegisterType<IPlayer, QQPlayer>();
			// retister type-mapping with a name
			container.RegisterType<IPlayer, BaiduPlayer>("baidu");

			container.RegisterType<IMediaFile, MP4MediaFile>(new InjectionConstructor("C:\\document\\mp4"));
			container.RegisterType<IMediaFile, MP3MediaFile>("mp3", new InjectionConstructor("C:\\document\\mp3"));


			/********使用[InjectionConstructor]特性注入********/
			//User u = container.Resolve<User>();
			//u.PlayMedia();
			//User u1 = container.Resolve<User>();
			//u1.PlayMedia();
			/********使用[InjectionConstructor]特性注入********/

			/********使用InjectionConstructor传入参数，用于识别调用那个构造********/
			container.RegisterType<User>("use-baidu", new InjectionConstructor(container.Resolve<IMediaFile>(), container.Resolve<IPlayer>("baidu")));

			//container.RegisterType<User>(new InjectionConstructor("anyu", container.Resolve<IMediaFile>(), container.Resolve<IPlayer>()));
			/********使用InjectionConstructor传入参数，用于识别调用那个构造********/


			/********解析具有相同名称的类型时，获得单例的效果********/
			User u = container.Resolve<User>("use-baidu");
			u.PlayMedia();
			User u1 = container.Resolve<User>("use-baidu");
			u1.PlayMedia();
			/********解析具有相同名称的类型时，获得单例的效果********/

			//User u2 = container.Resolve<User>();
			//u2.PlayMedia();
			//User u3 = container.Resolve<User>();
			//u3.PlayMedia();
		}
	}
}
