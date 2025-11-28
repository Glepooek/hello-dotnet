using _02_RegisterInstance.Media;
using _02_RegisterInstance.Player;
using Unity;

// *********no-runtime*********
// [InjectionConstructor] 构造
// [Dependency] 属性
// [InjectionMethod] 方法
// *********no-runtime*********

// *********runtime*********
// ParameterOverride 覆盖构造传入的参数值，ResolverOverride覆盖构造传入的多个参数值
// PropertyOverride 覆盖属性传入的参数值
// DependencyOverride 覆盖构造、方法、属性传入的参数值
// *********runtime*********
namespace _02_RegisterInstance
{
	public class User
	{
		IMediaFile _media;
		IPlayer _player;
		string _userName;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="media"></param>
		/// <param name="player"></param>
		/// <remarks>
		/// 由多个构造时，用[InjectionConstructor]特性指明使用哪个构造实例化对象
		/// 或container.RegisterType<User>(new InjectionConstructor(container.Resolve<IMediaFile>(), container.Resolve<IPlayer>()));
		/// </remarks>
		[InjectionConstructor]
		public User(IMediaFile media, IPlayer player)
		{
			this._media = media;
			this._player = player;
		}

		public User(string userName, IMediaFile media, IPlayer player)
		{
			this._userName = userName;
			this._media = media;
			this._player = player;
		}

		public void PlayMedia()
		{
			if (_media == null || _player == null)
			{
				return;
			}

			if (!string.IsNullOrEmpty(_userName))
			{
				_player.Play(_userName, _media);
			}
			else
			{
				_player.Play(_media);
			}
		}
	}
}
