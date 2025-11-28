using _08_DependencyOverride.Media;

namespace _08_DependencyOverride.Player
{
	public interface IPlayer
	{
		void Play(IMediaFile mediaFile);
		void Play(string userName, IMediaFile mediaFile);
	}
}
