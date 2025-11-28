using _06_ParameterOverride.Media;
using System;

namespace _06_ParameterOverride.Player
{
	public class BaiduPlayer : IPlayer
	{
		/// <summary>
		/// 播放次数
		/// </summary>
		int count = 0;

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
