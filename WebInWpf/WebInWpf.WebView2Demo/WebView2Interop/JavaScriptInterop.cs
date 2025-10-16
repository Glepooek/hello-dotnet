using Microsoft.Web.WebView2.Wpf;
using System.Threading.Tasks;

namespace Unipus.Student.Client.WebView2Interop
{
    /// <summary>
    /// C#调用网页JS交互类
    /// </summary>
    public static class JavaScriptInterop
    {
        /// <summary>
        /// 调用JS方法
        /// </summary>
        /// <param name="control">WebView2</param>
        /// <param name="jsMethod">JS方法，带参数，如"app4.calculateAdd(12,13)"</param>
        /// <returns>JS方法返回的结果</returns>
        public static async Task<string> JSFuncAsync(this WebView2 control, string jsMethod)
        {
            return await control.CoreWebView2.ExecuteScriptAsync(jsMethod);
        }
    }
}
