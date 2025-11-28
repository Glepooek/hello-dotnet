using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Learning.PrismDemo.Setting.Views
{
	/// <summary>
	/// About.xaml 的交互逻辑
	/// </summary>
	public partial class AboutView : UserControl
	{
		public AboutView()
		{
			InitializeComponent();

			this.Loaded += AboutView_Loaded;
			this.webBrowser.Navigating += WebBrowser_Navigating;
		}

		private void AboutView_Loaded(object sender, RoutedEventArgs e)
		{
			// 导航到指定地址
			// 也可以导航到指定HTML页面
			this.webBrowser.Navigate("https://www.baidu.com");
		}

		private void WebBrowser_Navigating(object sender, NavigatingCancelEventArgs e)
		{
			SetWebBrowserSilent(sender as WebBrowser, true);
		}

		/// <summary>
		/// 设置浏览器静默，不弹错误提示框
		/// </summary>
		/// <param name="webBrowser">要设置的WebBrowser控件浏览器</param>
		/// <param name="isSilent">是否静默</param>
		private void SetWebBrowserSilent(WebBrowser webBrowser, bool isSilent)
		{
			FieldInfo fieldInfo = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
			if (fieldInfo != null)
			{
				object browser = fieldInfo.GetValue(webBrowser);
				if (browser != null)
				{
					browser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, browser, new object[] { isSilent });
				}
			}
		}
	}
}