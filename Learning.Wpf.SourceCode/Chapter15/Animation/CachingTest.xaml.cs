using System;
using System.Windows;
using System.Windows.Media;

namespace Animation
{
	/// <summary>
	/// Interaction logic for CachingTest.xaml
	/// </summary>
	public partial class CachingTest : Window
	{
		public CachingTest()
		{
			InitializeComponent();

			PathGeometry pathGeometry = new PathGeometry();
			PathFigure pathFigure = new PathFigure();

			pathFigure.StartPoint = new Point(0, 0);

			PathSegmentCollection pathSegmentCollection = new PathSegmentCollection();

			int maxHeight = (int)this.Height;
			int maxWidth = (int)this.Width;
			Random rand = new Random();
			for (int i = 0; i < 500; i++)
			{
				LineSegment newSegment = new LineSegment();
				newSegment.Point = new Point(rand.Next(0, maxWidth), rand.Next(0, maxHeight));
				pathSegmentCollection.Add(newSegment);
			}

			pathFigure.Segments = pathSegmentCollection;
			pathGeometry.Figures.Add(pathFigure);

			pathBackground.Data = pathGeometry;
		}

		private void chkCache_Click(object sender, RoutedEventArgs e)
		{
			if (chkCache.IsChecked == true)
			{
				pathBackground.CacheMode = new BitmapCache();
			}
			else
			{
				pathBackground.CacheMode = null;
			}
		}
	}
}
