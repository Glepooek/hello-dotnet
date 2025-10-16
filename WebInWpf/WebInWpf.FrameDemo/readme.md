### 在WPF中显示HTML
- 使用CEF，可显示本地或在线HTML。
- 使用WebView2，可显示本地或在线HTML。
- 使用BlazorWebView，可显示本地或在线HTML。
- 使用Frame，可显示本地或在线HTML，不支持互操作。
- 使用WebBrowser，可显示本地或在线HTML，不支持互操作。```（不推荐使用）```
```xml,C#
<WebBrowser x:Name="webbrower" Source="file:///E:/ProjectxPlex/WPFCodePlex/WebInWpf/WebInWpf/WebInWpf.FrameDemo/bin/Debug/net8.0-windows/TestWeb/index.html" />

this.Loaded += (s,e) =>
{
    webbrower.Navigate(new Uri("file:///E:/ProjectxPlex/WPFCodePlex/WebInWpf/WebInWpf/WebInWpf.FrameDemo/bin/Debug/net8.0-windows/TestWeb/index.html"));
};
```

### Frame的主要用途：
- 导航到Page;
- 导航到UserControl或其他Control;
- 导航到在线网址