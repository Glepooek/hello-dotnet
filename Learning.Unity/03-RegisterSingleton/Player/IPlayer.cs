using _03_RegisterSingleton.Media;

namespace _03_RegisterSingleton.Player
{
	public interface IPlayer
	{
		void Play(IMediaFile mediaFile);
		void Play(string userName, IMediaFile mediaFile);
	}
}
