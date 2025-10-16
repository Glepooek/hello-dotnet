using System;

namespace PluginManager
{
    public interface ITeachingComponent : IDisposable
    {
        void Run(TeachingParams param, Action closeCallback);
    }
}
