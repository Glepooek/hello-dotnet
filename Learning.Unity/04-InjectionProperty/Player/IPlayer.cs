using _04_InjectionProperty.Media;

namespace _04_InjectionProperty.Player
{
	public interface IPlayer
	{
		void Play(IMediaFile mediaFile);
		void Play(string userName, IMediaFile mediaFile);
	}
}
