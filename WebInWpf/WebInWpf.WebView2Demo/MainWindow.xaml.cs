using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using Unipus.Student.Client.WebView2Interop;

namespace WebInWpf.WebView2Demo;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    #region Constructor

    public MainWindow()
    {
        InitializeComponent();

        this.Loaded += async (sender, args) =>
        {
            AttachControlEventHandlers(webView);
            await webView.EnsureCoreWebView2Async();
        };
    }

    #endregion

    #region EventHandlers

    private void OnWebViewNavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs e)
    {
        //mMainViewModel.IsNavigating = true;
    }

    private async void OnWebViewNavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
    {
        //mMainViewModel.IsNavigating = false;
        // 隐藏滚动条
        await webView.CoreWebView2.ExecuteScriptAsync(@"document.getElementsByTagName('body')[0].style.overflow='hidden';");
    }

    private void OnCoreWebView2InitializationCompleted(object? sender, CoreWebView2InitializationCompletedEventArgs e)
    {
        if (e.IsSuccess)
        {
            SetCoreWebView2Settings(webView);
            // 打开新链接时禁止创建新窗口处理
            //webView.CoreWebView2.NewWindowRequested += OnCoreWebView2NewWindowRequested;
            webView.CoreWebView2.ProcessFailed += OnCoreWebView2ProcessFailed;
            // JS调用C#方法。使用方法参考TestWeb/index.html
            webView.CoreWebView2.AddHostObjectToScript("UniStuWebView2HostObject", new DotnetInterop(this));
            webView.CoreWebView2.WebMessageReceived += OnCoreWebView2WebMessageReceived;
            //mLogger.Info("WebView2's CoreWebView2 creation succeed");

            Debug.WriteLine($"ChromeVersion: {webView.CoreWebView2.Environment.BrowserVersionString}");
            return;
        }

        //mLogger.Error("WebView2's CoreWebView2 creation failed with exception", e.InitializationException);
    }

    private void OnCoreWebView2ProcessFailed(object? sender, CoreWebView2ProcessFailedEventArgs e)
    {
        StringBuilder messageBuilder = new StringBuilder();
        messageBuilder.AppendLine($"Process kind: {e.ProcessFailedKind}");
        messageBuilder.AppendLine($"Reason: {e.Reason}");
        messageBuilder.AppendLine($"Exit code: {e.ExitCode}");
        messageBuilder.AppendLine($"Process description: {e.ProcessDescription}");
        //mLogger.Error(messageBuilder.ToString());
        System.Threading.SynchronizationContext.Current?.Post((_) =>
        {
            var result = MessageBox.Show("是否重新启动程序?", "程序子进程出现异常", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                //ProcessHelper.StartProcess(AppConstant.AppExeFilePath);
            }
            Application.Current.Shutdown();
        }, null);
    }

    private void OnCoreWebView2NewWindowRequested(object? sender, CoreWebView2NewWindowRequestedEventArgs e)
    {
        var deferral = e.GetDeferral();
        e.NewWindow = webView.CoreWebView2;
        deferral.Complete();
    }

    private void OnCoreWebView2WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
    {
        Debug.WriteLine(e.WebMessageAsJson);
    }

    private void OnWebViewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.IsRepeat) return;
        bool ctrl = e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || e.KeyboardDevice.IsKeyDown(Key.RightCtrl);
        bool alt = e.KeyboardDevice.IsKeyDown(Key.LeftAlt) || e.KeyboardDevice.IsKeyDown(Key.RightAlt);
        bool shift = e.KeyboardDevice.IsKeyDown(Key.LeftShift) || e.KeyboardDevice.IsKeyDown(Key.RightShift);
        if (e.Key == Key.N && ctrl && !alt && !shift)
        {
            new MainWindow().Show();
            e.Handled = true;
        }
        else if (e.Key == Key.W && ctrl && !alt && !shift)
        {
            Close();
            e.Handled = true;
        }
    }

    protected override void OnStateChanged(EventArgs e)
    {
        if (this.WindowState == WindowState.Normal || this.WindowState == WindowState.Maximized)
        {
            if (this.webView.CoreWebView2.IsSuspended)
            {
                //mLogger.Info($"WebView is suspended: {this.webView.CoreWebView2.IsSuspended}");
                this.webView.CoreWebView2.Resume();
                //mLogger.Info($"WebView is suspended: {this.webView.CoreWebView2.IsSuspended}");
            }
        }
    }

    private async void OnCsharpCallJS(object sender, RoutedEventArgs e)
    {
        var result = await webView.JSFuncAsync("app4.calculateAdd1(1,2)");
        Debug.WriteLine(result);
    }

    private void OnSendMessageToJS(object sender, RoutedEventArgs e)
    {
        if (webView.CoreWebView2 != null)
        {
            // 发送字符串消息
            webView.CoreWebView2.PostWebMessageAsString("Hello from WPF!");

            // 发送 JSON 格式消息
            var jsonData = new { Action = "update", Value = 42 };
            string json = JsonSerializer.Serialize(jsonData);
            webView.CoreWebView2.PostWebMessageAsJson(json);
        }
    }
    #endregion

    #region PrivateMethods

    private void SetCoreWebView2Settings(WebView2 control)
    {
        control.CoreWebView2.Settings.IsPasswordAutosaveEnabled = false;
        control.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
        control.CoreWebView2.Settings.AreDevToolsEnabled = true;
        control.CoreWebView2.Settings.IsZoomControlEnabled = false;
    }

    /// <summary>
    /// 注册WebView2事件
    /// </summary>
    /// <param name="control"></param>
    private void AttachControlEventHandlers(WebView2 control)
    {
        control.NavigationStarting += OnWebViewNavigationStarting;
        control.NavigationCompleted += OnWebViewNavigationCompleted;
        control.CoreWebView2InitializationCompleted += OnCoreWebView2InitializationCompleted;
        control.KeyDown += OnWebViewKeyDown;
    }

    /// <summary>
    /// 判断WebView2是否有效
    /// </summary>
    /// <returns></returns>
    private bool IsWebViewValid()
    {
        try
        {
            return webView != null && webView.CoreWebView2 != null;
        }
        catch (Exception ex) when (ex is ObjectDisposedException || ex is InvalidOperationException)
        {
            return false;
        }
    }

    /// <summary>
    ///  强制触发命令
    /// </summary>
    private void RequeryCommands()
    {
        CommandManager.InvalidateRequerySuggested();
    }

    #endregion
}
