using Microsoft.Extensions.Primitives;
using System;

// +++++++++++++++++++++++
// CancellationChangeToken
// CompositeChangeToken
// +++++++++++++++++++++++

namespace ChangeTokenExample
{
    public class Bookstore : DataProvider, IDisposable
    {
        #region Fields

        private readonly IDisposable _changeTokenRegistration;
        private ParamInfo _paramInfo;

        #endregion

        #region Constructor

        public Bookstore()
        {
            _paramInfo = new ParamInfo();

            //_changeTokenRegistration = ChangeToken.OnChange(GetReloadToken, PrintBooks);
            _changeTokenRegistration = ChangeToken.OnChange<ParamInfo>(GetReloadToken, PrintBooks, _paramInfo);
        }

        #endregion

        #region IDisposable

        public void Dispose() => Dispose(true);

        #endregion

        #region Methods

        public void AddBook(string sn, string bookName)
        {
            _paramInfo.Key = sn;

            Set(sn, bookName);
            OnReload();
        }

        private void PrintBooks()
        {
            Console.WriteLine("=======book list=========");
            foreach ((string key, string value) in Data)
            {
                Console.WriteLine($"sn:{key} book:{value}");
            }
            Console.WriteLine("=========================");
        }

        private void PrintBooks(ParamInfo s)
        {
            Console.WriteLine("=======book list=========");
            Console.WriteLine($"sn:{s.Key} book:{Data[s.Key]}");
            Console.WriteLine("=========================");
        }

        protected virtual void Dispose(bool disposing)
        {
            _changeTokenRegistration?.Dispose();
        }

        #endregion
    }
}