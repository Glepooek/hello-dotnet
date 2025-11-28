using _04_InjectionProperty.Media;
using _04_InjectionProperty.Player;
using Unity;

namespace _04_InjectionProperty
{
	/// <summary>
	/// 演示属性注入
	/// </summary>
	class User1
	{
		//[Dependency]
		public IMediaFile Media { get; set; }
		//[Dependency]
		//[Dependency("baidu")]
		public IPlayer Player { get; set; }
		public string UserName { get; set; }

		public void PlayMedia()
		{
			if (Media == null || Player == null)
			{
				return;
			}

			if (!string.IsNullOrEmpty(UserName))
			{
				Player.Play(UserName, Media);
			}
			else
			{
				Player.Play(Media);
			}
		}
	}
}
