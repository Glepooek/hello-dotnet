using CefSharp;
using CefSharp.Wpf;
using CefSharp.Wpf.Experimental;
using System;
using System.Linq;
using System.Reflection;
using WebInWpf.Cefsharp.Handlers;

namespace WebInWpf.Cefsharp.Controls;

internal class UniStuChromiumBrowser : ChromiumWebBrowser
{
    #region Constructor

    public UniStuChromiumBrowser() : base()
    {
        this.LifeSpanHandler = new CefLifeSpanHandler();
        this.DownloadHandler = new CefDownloadHandler();
        this.MenuHandler = new CefMenuHandler();
        this.KeyboardHandler = new KeyboardHandler();
        this.WpfKeyboardHandler = new WpfImeKeyboardHandler(this);

        this.Address = $"{AppDomain.CurrentDomain.BaseDirectory}TestWeb//index.html";

        RegisterBoundObject("webView", new BoundObject());
    }

    #endregion

    #region EventHandles

    private void OnBrowserJavascriptMessageReceived(object sender, JavascriptMessageReceivedEventArgs e)
    {
        Console.WriteLine(e.Message);
    }

    private void OnBrowserFrameLoadEnd(object sender, FrameLoadEndEventArgs e)
    {
        if (e.Frame != null)
        {
            MethodInfo[] methodInfos = typeof(BoundObject).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            string methodName = methodInfos.Select(m => m.Name).ToArray()[1];
            // 执行JS，将BoundObject中的异步代码包装为同步实现
            string js = $@"";
            e.Frame.ExecuteJavaScriptAsync(js);
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// 注册回调对象，用于JS调用C#方法
    /// </summary>
    /// <param name="name"></param>
    /// <param name="objectToBind"></param>
    private void RegisterBoundObject(string name, object objectToBind)
    {
        this.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
        this.JavascriptObjectRepository.Register(name, objectToBind, BindingOptions.DefaultBinder);
        this.JavascriptMessageReceived -= OnBrowserJavascriptMessageReceived;
        this.JavascriptMessageReceived += OnBrowserJavascriptMessageReceived;
        this.FrameLoadEnd += OnBrowserFrameLoadEnd;
    }

    /// <summary>
    /// C#调用JS方法
    /// </summary>
    public void CallJavaScriptAsync(string code, string scriptUrl = "about:blank", int startLine = 1)
    {
        if (!this.IsBrowserInitialized)
        {
            return;
        }

        try
        {
            this?.GetBrowser()?.MainFrame.ExecuteJavaScriptAsync(code, scriptUrl, startLine);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion
}