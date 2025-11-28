using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Learning.PrismDemo.Controls
{
	public class MainMenuItemControl : Button
	{
		#region Dependency Properties

		/// <summary>
		/// 背景图片
		/// </summary>
		public ImageSource BackgroundImage
		{
			get { return (ImageSource)GetValue(BackgroundImageProperty); }
			set { SetValue(BackgroundImageProperty, value); }
		}

		/// <summary>
		/// 背景图片依赖项属性
		/// </summary>
		public static readonly DependencyProperty BackgroundImageProperty =
			DependencyProperty.Register(nameof(BackgroundImage), typeof(ImageSource), typeof(MainMenuItemControl), new PropertyMetadata(null));

		#endregion
	}
}
