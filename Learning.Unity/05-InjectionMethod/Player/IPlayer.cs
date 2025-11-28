using _05_InjectionMethod.Media;

namespace _05_InjectionMethod.Player
{
	public interface IPlayer
	{
		void Play(IMediaFile mediaFile);
		void Play(string userName, IMediaFile mediaFile);
	}
}
