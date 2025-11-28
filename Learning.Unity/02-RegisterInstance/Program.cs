using _02_RegisterInstance.Media;
using _02_RegisterInstance.Player;
using System;
using Unity;

namespace _02_RegisterInstance
{
	class Program
	{
		static void Main(string[] args)
		{
			// create UnityContainer instance
			IUnityContainer container = new UnityContainer();

			RegisterInstanceDemo(container);

			Console.ReadLine();
		}

		public static void RegisterInstanceDemo(IUnityContainer container)
		{
			IPlayer player = new QQPlayer();
			IMediaFile media = new MP4MediaFile("C:\\document\\mp4");
			// register instance, container will use the same instance every time for the same registered type.
			container.RegisterInstance<IPlayer>(player);
			container.RegisterInstance<IMediaFile>(media);

			/********用RegisterInstance注册类型后，解析类型时，获得单例的效果********/
			User u = container.Resolve<User>();
			u.PlayMedia();
			User u1 = container.Resolve<User>();
			u1.PlayMedia();
			/********用RegisterInstance注册类型后，解析类型时，获得单例的效果********/
		}
	}
}
