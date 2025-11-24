using System;
using System.Reflection;

namespace SimpleAOPSamples
{
    public class DynamicProxy<T> : DispatchProxy
    {
        private T? _decorated;
        private Predicate<MethodInfo> _filter = m => true;

        public Predicate<MethodInfo> Filter
        {
            get => _filter;
            set => _filter = value ?? (m => true);
        }

        // Initialize the proxy with the real instance
        public void SetParameters(T decorated)
        {
            _decorated = decorated ?? throw new ArgumentNullException(nameof(decorated));
        }

        // Create helper to simplify proxy construction
        public static T Create(T decorated)
        {
            var proxy = DispatchProxy.Create<T, DynamicProxy<T>>();
            ((DynamicProxy<T>)(object)proxy).SetParameters(decorated);
            return proxy;
        }

        private void Log(string msg, params object[] args)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg, args);
            Console.ForegroundColor = prev;
        }

        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
        {
            ArgumentNullException.ThrowIfNull(targetMethod);

            if (_decorated == null)
                throw new InvalidOperationException("Decorated instance was not set. Use DynamicProxy<T>.Create to construct the proxy.");

            if (_filter(targetMethod))
                Log("In Dynamic Proxy - Before executing '{0}'", targetMethod.Name);

            try
            {
                object? result = targetMethod.Invoke(_decorated, args);

                if (_filter(targetMethod))
                    Log("In Dynamic Proxy - After executing '{0}'", targetMethod.Name);

                return result;
            }
            catch (TargetInvocationException tie)
            {
                // unwrap
                Exception? ex = tie.InnerException ?? tie;
                if (_filter(targetMethod))
                    Log("In Dynamic Proxy - Exception {0} executing '{1}'", ex, targetMethod.Name);
                throw ex;
            }
        }
    }
}
