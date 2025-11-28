using System.Windows;
using System.Windows.Media;

namespace Animation
{
	/// <summary>
	/// Interaction logic for CachingTest2.xaml
	/// </summary>
	public partial class CachingTest2 : Window
	{
		public CachingTest2()
		{
			InitializeComponent();
		}

		private void chkCache_Click(object sender, RoutedEventArgs e)
		{
			if (chkCache.IsChecked == true)
			{
				BitmapCache bitmapCache = new BitmapCache();
				bitmapCache.RenderAtScale = 5;
				cmd.CacheMode = bitmapCache;
				img.CacheMode = new BitmapCache();
			}
			else
			{
				cmd.CacheMode = null;
				img.CacheMode = null;
			}
		}
	}
}
