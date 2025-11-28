namespace _08_DependencyOverride.Media
{
	/// <summary>
	/// mp3媒体文件类
	/// </summary>
	class MP3MediaFile : IMediaFile
	{
		public string FilePath { get; set; }
		public string Copyright { get; set; }

		public MP3MediaFile(string filePath)
		{
			this.FilePath = filePath;
		}
	}
}
