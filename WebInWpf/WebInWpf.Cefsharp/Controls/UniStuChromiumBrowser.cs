using CefSharp;
using CefSharp.Wpf;
using CefSharp.Wpf.Experimental;
using System;
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