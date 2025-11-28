using _08_DependencyOverride.Media;
using _08_DependencyOverride.Player;
using Unity;

namespace _08_DependencyOverride
{
	class User2
	{
		IMediaFile _media;
		IPlayer _player;
		string _userName;

		//[InjectionMethod]
		public void Injct(IMediaFile _media, IPlayer _player)
		{
			this._media = _media;
			this._player = _player;
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
