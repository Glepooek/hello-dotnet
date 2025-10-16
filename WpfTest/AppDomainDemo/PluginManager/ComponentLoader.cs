using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;

namespace PluginManager
{
    public class ComponentLoader : MarshalByRefObject
    {
        private CompositionContainer mContainer;

        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        private ITeachingComponent mTeachingComponent;

        public override object InitializeLifetimeService() => null;

        /// <summary>
        /// 运行组件
        /// </summary>
        /// <param name="teachingParams">启动参数</param>
        /// <param name="dllName">程序集名</param>
        public void RunComponent(TeachingParams teachingParams, string dllName)
        {
            DisposeContainer();

            Assembly assembly = Assembly.LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dllName));
            AssemblyCatalog catalog = new AssemblyCatalog(assembly);

            mContainer = new CompositionContainer(catalog);
            mContainer.ComposeParts(this);

            mTeachingComponent.Run(teachingParams, DisposeContainer);
        }

        public void Unload()
        {
            DisposeContainer();
        }

        /// <summary>
        /// 关闭并释放组件
        /// </summary>
        private void DisposeContainer()
        {
            if (mContainer == null)
            {
                return;
            }

            // 释放组件
            mContainer.ReleaseExports(mContainer.GetExports<ITeachingComponent>());
            mContainer.Dispose();
            mContainer = null;
            mTeachingComponent = null;
        }
    }
}
