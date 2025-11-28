namespace _05_InjectionMethod.Media
{
	/// <summary>
	/// mp4媒体文件类
	/// </summary>
	public class MP4MediaFile : IMediaFile
	{
		public string FilePath { get; set; }
		public string Copyright { get; set; }

		public MP4MediaFile(string filePath)
		{
			this.FilePath = filePath;
		}
	}
}
