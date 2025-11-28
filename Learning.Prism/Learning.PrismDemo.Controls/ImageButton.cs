using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Learning.PrismDemo.Controls
{
	public class ImageButton : Button
	{
		#region Dependency Properties

		/// <summary>
		/// 正常态图片
		/// </summary>
		public ImageSource NormalImage
		{
			get { return (ImageSource)GetValue(NormalImageProperty); }
			set { SetValue(NormalImageProperty, value); }
		}
		/// <summary>
		/// 正常态图片依赖项属性
		/// </summary>
		public static readonly DependencyProperty NormalImageProperty =
			DependencyProperty.Register(nameof(NormalImage), typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));

		/// <summary>
		/// 悬浮态图片
		/// </summary>
		public ImageSource HoverImage
		{
			get { return (ImageSource)GetValue(HoverImageProperty); }
			set { SetValue(HoverImageProperty, value); }
		}
		/// <summary>
		/// 悬浮态图片依赖项属性
		/// </summary>
		public static readonly DependencyProperty HoverImageProperty =
			DependencyProperty.Register(nameof(HoverImage), typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));

		/// <summary>
		/// 按下态图片
		/// </summary>
		public ImageSource DownImage
		{
			get { return (ImageSource)GetValue(DownImageProperty); }
			set { SetValue(DownImageProperty, value); }
		}
		/// <summary>
		/// 按下态图片依赖项属性
		/// </summary>
		public static readonly DependencyProperty DownImageProperty =
			DependencyProperty.Register(nameof(DownImage), typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));

		/// <summary>
		/// 图片高度
		/// </summary>
		public double ImageHeight
		{
			get { return (double)GetValue(ImageHeightProperty); }
			set { SetValue(ImageHeightProperty, value); }
		}
		/// <summary>
		/// 图片高度依赖项属性
		/// </summary>
		public static readonly DependencyProperty ImageHeightProperty =
			DependencyProperty.Register(nameof(ImageHeight), typeof(double), typeof(ImageButton), new PropertyMetadata(40.0));

		/// <summary>
		/// 图片宽度
		/// </summary>
		public double ImageWidth
		{
			get { return (double)GetValue(ImageWidthProperty); }
			set { SetValue(ImageWidthProperty, value); }
		}
		/// <summary>
		/// 图片宽度依赖项属性
		/// </summary>
		public static readonly DependencyProperty ImageWidthProperty =
			DependencyProperty.Register(nameof(ImageWidth), typeof(double), typeof(ImageButton), new PropertyMetadata(40.0));

		/// <summary>
		/// 图片填充状态
		/// </summary>
		public Stretch ImageStretch
		{
			get { return (Stretch)GetValue(ImageStretchProperty); }
			set { SetValue(ImageStretchProperty, value); }
		}
		/// <summary>
		/// 图片填充状态依赖项属性
		/// </summary>
		public static readonly DependencyProperty ImageStretchProperty =
			DependencyProperty.Register(nameof(ImageStretch), typeof(Stretch), typeof(ImageButton), new PropertyMetadata(Stretch.None));

		/// <summary>
		/// 图片水平对齐方式
		/// </summary>
		public HorizontalAlignment ImageHorizontalAlignment
		{
			get { return (HorizontalAlignment)GetValue(ImageHorizontalAlignmentProperty); }
			set { SetValue(ImageHorizontalAlignmentProperty, value); }
		}
		/// <summary>
		/// 图片水平对齐方式依赖项属性
		/// </summary>
		public static readonly DependencyProperty ImageHorizontalAlignmentProperty =
			DependencyProperty.Register(nameof(ImageHorizontalAlignment), typeof(HorizontalAlignment), typeof(ImageButton), new PropertyMetadata(HorizontalAlignment.Stretch));

		/// <summary>
		/// 图片垂直对齐方式
		/// </summary>
		public VerticalAlignment ImageVerticalAlignment
		{
			get { return (VerticalAlignment)GetValue(ImageVerticalAlignmentProperty); }
			set { SetValue(ImageVerticalAlignmentProperty, value); }
		}
		/// <summary>
		/// 图片垂直对齐方式依赖项属性
		/// </summary>
		public static readonly DependencyProperty ImageVerticalAlignmentProperty =
			DependencyProperty.Register(nameof(ImageVerticalAlignment), typeof(VerticalAlignment), typeof(ImageButton), new PropertyMetadata(VerticalAlignment.Stretch));

		#endregion
	}
}
