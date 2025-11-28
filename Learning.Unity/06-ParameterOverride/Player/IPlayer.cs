using _06_ParameterOverride.Media;

namespace _06_ParameterOverride.Player
{
	public interface IPlayer
	{
		void Play(IMediaFile mediaFile);
		void Play(string userName, IMediaFile mediaFile);
	}
}
