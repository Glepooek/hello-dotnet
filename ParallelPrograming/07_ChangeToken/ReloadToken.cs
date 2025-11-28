using Microsoft.Extensions.Primitives;
using System;
using System.Threading;

namespace ChangeTokenExample
{
    public class ReloadToken : IChangeToken
    {
        #region Fields

        private CancellationTokenSource _cts = new CancellationTokenSource();

        #endregion

        #region Methods

        public void OnReload()
        {
            _cts.Cancel();
            _cts.Dispose();
        }

        #endregion

        #region IChangeToken
        public bool HasChanged => _cts.IsCancellationRequested;
        /// <summary>
        /// 是否自动触发回调
        /// </summary>
        public bool ActiveChangeCallbacks => true;
        public IDisposable RegisterChangeCallback(Action<object> callback, object state)
        {
            return _cts.Token.Register(callback, state);
        }
        #endregion
    }
}
