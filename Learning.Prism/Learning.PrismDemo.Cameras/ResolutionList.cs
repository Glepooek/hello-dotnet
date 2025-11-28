using System.Collections.Generic;

namespace Learning.PrismDemo.Cameras
{
	/// <summary>
	/// 摄像头分辨率集合类
	/// </summary>
	public class ResolutionList : List<Resolution>
	{
		/// <summary>
		/// Adds resolution to collection if it doesn't already exist in it
		/// </summary>
		/// <param name="item">Resolution should be added if it's new.</param>
		/// <returns>True if was added, False otherwise</returns>
		public bool AddIfNew(Resolution item)
		{
			if (this.Contains(item))
				return false;

			this.Add(item);
			return true;
		}
	}
}
