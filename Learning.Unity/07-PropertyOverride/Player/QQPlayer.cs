using _07_PropertyOverride.Media;
using System;

namespace _07_PropertyOverride.Player
{
	public class QQPlayer : IPlayer
	{
		/// <summary>
		/// 播放次数
		/// </summary>
		int count;

		public void Play(IMediaFile mediaFile)
		{
			count++;
			Console.WriteLine(string.Format("PlayerType:{0}, MeaiaFilePath:{1}, PlayTimes:{2}", this.GetType().Name, mediaFile.FilePath, count));
		}

		public void Play(string userName, IMediaFile mediaFile)
		{
			count++;
			Console.WriteLine(string.Format("userName:{0}, PlayerType:{1}, MeaiaFilePath:{2}, PlayTimes:{3}", userName, this.GetType().Name, mediaFile.FilePath, count));
		}
	}
}
