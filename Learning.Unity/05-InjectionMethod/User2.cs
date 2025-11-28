using _05_InjectionMethod.Media;
using _05_InjectionMethod.Player;
using Unity;

namespace _05_InjectionMethod
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
