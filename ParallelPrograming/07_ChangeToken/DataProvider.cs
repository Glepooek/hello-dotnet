using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Threading;

namespace ChangeTokenExample
{
    public class DataProvider
    {
        #region Fields

        private ReloadToken _reloadToken;
        protected IDictionary<string, string> Data { get; set; }

        #endregion

        #region Constructor

        public DataProvider()
        {
            _reloadToken = new ReloadToken();
            Data = new Dictionary<string, string>();
        }

        #endregion

        #region Methods

        protected void Set(string key, string value)
        {
            Data[key] = value;
        }

        protected IChangeToken GetReloadToken()
        {
            return _reloadToken;
        }

        protected void OnReload()
        {
            // Interlocked.Exchange，用于交换指定变量的值，并返回交换之前的值。
            ReloadToken previousToken = Interlocked.Exchange(ref _reloadToken, new ReloadToken());
            previousToken.OnReload();
        }

        #endregion
    }
}