using _02_RegisterInstance.Media;

namespace _02_RegisterInstance.Player
{
	public interface IPlayer
	{
		void Play(IMediaFile mediaFile);
		void Play(string userName, IMediaFile mediaFile);
	}
}
