using _07_PropertyOverride.Media;

namespace _07_PropertyOverride.Player
{
	public interface IPlayer
	{
		void Play(IMediaFile mediaFile);
		void Play(string userName, IMediaFile mediaFile);
	}
}
