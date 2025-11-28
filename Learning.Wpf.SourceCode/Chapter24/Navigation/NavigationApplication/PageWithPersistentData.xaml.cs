using System.Windows;

namespace NavigationApplication
{
	/// <summary>
	/// Interaction logic for PageWithPersistentData.xaml
	/// </summary>

	public partial class PageWithPersistentData : System.Windows.Controls.Page
	{
		public PageWithPersistentData()
		{
			InitializeComponent();
		}
		private static DependencyProperty MyPageDataProperty;
		static PageWithPersistentData()
		{
			FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata();
			metadata.Journal = true;

			MyPageDataProperty = DependencyProperty.Register(
				"MyPageDataProperty", typeof(string), typeof(PageWithPersistentData),
				metadata, null);
		}

		private string MyPageData
		{
			set { SetValue(MyPageDataProperty, value); }
			get { return (string)GetValue(MyPageDataProperty); }
		}
		public void SetText(object sender, RoutedEventArgs e)
		{
			MyPageData = txt.Text;
			txt.Text = "";
		}
		public void GetText(object sender, RoutedEventArgs e)
		{
			lbl.Content = "Retrieved: " + MyPageData;
		}
	}
}